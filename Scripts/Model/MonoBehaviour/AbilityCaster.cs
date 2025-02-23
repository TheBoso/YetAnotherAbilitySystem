using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;
using UnityEngine;

namespace YAAS
{
    public class AbilityCaster : NetworkBehaviour
    {
        private Dictionary<string, RuntimeAbility> _learnedAbilities = new Dictionary<string, RuntimeAbility>();
        
        [SerializeField] private AbilityDef[] _startingAbilities;

        public IEnumerable<RuntimeAbility> GetAllAbilities() => _learnedAbilities.Values;
        
        public bool IsPerformingAbility { get; private set; }
        
        public event Action<RuntimeAbility> OnAbilityLearned;
        public event Action<RuntimeAbility> OnAbilityUnLearned;

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
            if(_startingAbilities != null && _startingAbilities.Length > 0)
            {
                foreach (var ability in _startingAbilities)
                {
                    LearnAbility(ability);
                }
            }  
        }

        public void LearnAbility(AbilityDef def)
        {
            if (def.IsConsumable && _learnedAbilities.TryGetValue(def.AbilityID, out RuntimeAbility info))
            {
                info.Quantity++;
            }
            else
            {
                if (def.OnAbilityEquipEffects != null && def.OnAbilityEquipEffects.Length > 0)
                {
                    foreach (var effect in def.OnAbilityEquipEffects)
                    {
                        effect.PerformEffect(this);
                    }
                }
            _learnedAbilities.Add(def.AbilityID, new RuntimeAbility(def));
            OnAbilityLearned?.Invoke(_learnedAbilities[def.AbilityID]);
            }
        }
        public void UnLearnAbility(AbilityDef def)
        {

            if (def.OnAbilityUnequipEffects != null && def.OnAbilityUnequipEffects.Length > 0)
            {
                foreach (var effect in def.OnAbilityUnequipEffects)
                {
                    effect.PerformEffect(this);
                }
            }
            _learnedAbilities.Remove(def.AbilityID);
            OnAbilityUnLearned?.Invoke(_learnedAbilities[def.AbilityID]);


        }
        
        public bool TryUseAbility(AbilityDef def)
        {
            return TryUseAbility(def.AbilityID);
        }
        public bool TryUseAbility(string abilityID)
        {
            if (IsPerformingAbility) return false;
            RuntimeAbility info = null;
            if (_learnedAbilities.TryGetValue(abilityID, out info) == false)
            {
                Debug.LogWarning($"Character {gameObject.name} does not have ability {abilityID}");
                return false;
            }

            if (info.IsOnCooldown())
            {
                return false;
            }

            AbilityActivationRequirement[] requirements = info.Ability.AbilityActivationRequirements;
            
            //  iterate through all our useage requirements
            if (requirements != null &&
                requirements.Length > 0)
            {
                foreach (AbilityActivationRequirement requirement in requirements)
                {
                    if (requirement.MeetsRequirement(this) == false)
                    {
                        return false;
                    }
                }
            }


        

            info.AbilityLastUseTime = Time.time;
            
            if (info.Ability.IsConsumable && info.Quantity > 0)
            {
                info.Quantity--;
                if (info.Quantity < 1)
                {
                    UnLearnAbility(info.Ability);
                }
            }

            int index = -1;

            for (int i = 0; i < _learnedAbilities.Count; i++)
            {
                if (_learnedAbilities.ElementAt(i).Key == abilityID)
                {
                    index = i;
                    break;
                }
            }

            UseAbilityServerRpc(index);
            return true;

        }

        [ServerRpc]
        public void UseAbilityServerRpc(int abilityIndex)
        {
            UseAbilityClientRpc(abilityIndex);
        }

        [ClientRpc]
        public void UseAbilityClientRpc(int abilityIndex)
        {
            AbilityEffect[] effects = this._learnedAbilities.ElementAt(abilityIndex).Value.Ability.AbilityEffects;
            StartCoroutine(AbilityEffectRoutine(effects));

        }
        
        private IEnumerator AbilityEffectRoutine(AbilityEffect[] effects)
        {
            IsPerformingAbility = true;
            foreach (AbilityEffect effect in effects)
            {
                yield return effect.PerformEffect(this);
            }
            IsPerformingAbility = false;
        }
    }
    
    


    public class RuntimeAbility
    {
        public int Quantity { get; set; } = 1;
        public AbilityDef Ability { get; private set; }

        public float AbilityLastUseTime { get; set; }
        public float AbilityCooldownSeconds { get; private set; }

        public RuntimeAbility(AbilityDef ability)
        {
            Ability = ability;
            AbilityLastUseTime = 0;
            AbilityCooldownSeconds = ability.AbilityCooldownSeconds;
        }
        
        public bool IsOnCooldown()
        {
            return Time.time < AbilityLastUseTime + AbilityCooldownSeconds;
        }
    }
}
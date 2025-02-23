using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace YAAS
{
    public class AbilityUIController : MonoBehaviour
    {
        private AbilityCaster _caster;

        //  Map abilities to key index
        [SerializeField] private KeyCode[] _abilityKey;
        [SerializeField] private AbilityUI _abilityUIPrefab;
        private List<AbilityUI> _spawnedAbilityUIs;
        [SerializeField] private RectTransform _parent;

        private void Awake()
        {
            _spawnedAbilityUIs = new List<AbilityUI>();
        }

        private void Update()
        {
            for (int i = 0; i < _abilityKey.Length; i++)
            {
                if (i < _spawnedAbilityUIs.Count && Input.GetKeyDown(_abilityKey[i]))
                {
                    _spawnedAbilityUIs[i].SimulateTap();
                }
            }
        }

        public void SpawnAbilitiesFromCaster(AbilityCaster caster)
        {
            _caster = caster;
            CleanUp();

            var abs = caster.GetAllAbilities();

            int i = 0;
            foreach (var ability in abs)
            {
                AddAbilityUI(ability);
                if (i < _abilityKey.Length)
                {
                    _spawnedAbilityUIs[i].SetKeyBind(_abilityKey[i].ToString());
                }

                i++;
            }

            caster.OnAbilityLearned += AbilityLearnt;
            caster.OnAbilityUnLearned += AbilityUnLearnt;
        }

        public void AbilityLearnt(RuntimeAbility ab)
        {
            AddAbilityUI(ab);
        }

        public void AbilityUnLearnt(RuntimeAbility ab)
        {
            RemoveAbilityUI(ab);
        }

        private void RemoveAbilityUI(RuntimeAbility ab)
        {
            var abUI = _spawnedAbilityUIs.FirstOrDefault(x => ab.Ability.AbilityID == x.AbilityID);
            if (abUI != null)
            {
                _spawnedAbilityUIs.Remove(abUI);
                Destroy(abUI.gameObject);
            }
        }

        private void AddAbilityUI(RuntimeAbility ab)
        {
            AbilityUI spawnedAbility = Instantiate(_abilityUIPrefab, _parent);
            spawnedAbility.Setup(ab, () =>
            {
                if (_caster.TryUseAbility(ab.Ability.AbilityID))
                {
                    spawnedAbility.PutOnCooldown();
                }
            });

            _spawnedAbilityUIs.Add(spawnedAbility);
        }


        private void OnDestroy()
        {
            CleanUp();
        }

        public void CleanUp()
        {
            if (_spawnedAbilityUIs != null && _spawnedAbilityUIs.Count > 0)
            {
                for (int i = _spawnedAbilityUIs.Count - 1; i >= 0; i--)
                {
                    Destroy(_spawnedAbilityUIs[i].gameObject);
                }

                _spawnedAbilityUIs.Clear();
            }

            if (_caster != null)
            {
                _caster.OnAbilityLearned -= AbilityLearnt;
                _caster.OnAbilityUnLearned -= AbilityUnLearnt;
            }
        }
    }
}
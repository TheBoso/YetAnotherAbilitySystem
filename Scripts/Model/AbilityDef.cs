using UnityEngine;

namespace YAAS
{
    [CreateAssetMenu(menuName = "YAAS/AbilityDef")]
    public class AbilityDef : ScriptableObject
    {
        public string AbilityID => this.name;

        
        [Header("Visuals")]
        [field: SerializeField] public string AbilityName { get; private set; }
        [field: SerializeField] public string AbilityDescription { get; private set; }
        [field: SerializeField] public Sprite AbilityIcon { get; private set; }

        [Header("Settings")]
        [field: SerializeField] public AbilityActivationRequirement[] AbilityActivationRequirements { get; private set; }
        [field: SerializeField] public AbilityEffect[] AbilityEffects { get; private set; }
        [field: SerializeField] public AbilityEffect[] OnAbilityEquipEffects { get; private set; }
        [field: SerializeField] public AbilityEffect[] OnAbilityUnequipEffects { get; private set; }
        [field: SerializeField] public float AbilityCooldownSeconds { get; private set; } = 2.0f;
        [field: SerializeField] public bool IsConsumable { get; private set; }
        [field: SerializeField] public bool DisallowActivation { get; private set; }
        
    }
}

using UnityEngine;
using YAAS;

namespace YAAS
{
    
public abstract class AbilityActivationRequirement : ScriptableObject
{
    public abstract bool MeetsRequirement(AbilityCaster caster);
}
}

using System.Collections;
using UnityEngine;

namespace YAAS
{
    
public class IsLocalPlayerEffect : AbilityEffect
{
    [SerializeField] private AbilityEffect[] _trueEffects;
    public override IEnumerator PerformEffect(AbilityCaster caller)
    {
        if (IsLocalPlayer(caller))
        {
            foreach (var effect in _trueEffects)
            {
                yield return effect.PerformEffect(caller);
            }
        }
    }

    protected virtual bool IsLocalPlayer(AbilityCaster caster)
    {
        return caster.CompareTag("Player");
    }
    
    
}
}

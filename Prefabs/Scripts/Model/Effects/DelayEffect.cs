using System.Collections;
using UnityEngine;

namespace YAAS
{
    
    [CreateAssetMenu(fileName = "DelayEffect", menuName = "YAAS/Effects/DelayEffect", order = 1)]
public class DelayEffect : AbilityEffect
{
    [SerializeField] private float _delaySeconds = 1.0f;
    public override IEnumerator PerformEffect(AbilityCaster caller)
    {
        yield return new WaitForSeconds(_delaySeconds);
    }


}

}



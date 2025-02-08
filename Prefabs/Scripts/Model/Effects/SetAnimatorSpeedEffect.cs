using System.Collections;
using UnityEngine;

namespace YAAS
{
    
    [CreateAssetMenu(fileName = "SetAnimatorSpeedEffect", menuName = "YAAS/Effects/SetAnimatorSpeedEffect", order = 1)]
public class SetAnimatorSpeedEffect : AbilityEffect
{
    [SerializeField] private float _speed = 1.0f;
    public override IEnumerator PerformEffect(AbilityCaster caller)
    {
        if (caller.TryGetComponent(out Animator animator))
        {
            animator.speed = _speed;
        }
        
        yield return null;
    }
}
}

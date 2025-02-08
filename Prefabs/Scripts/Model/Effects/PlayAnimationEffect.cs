using System.Collections;
using UnityEngine;

namespace YAAS
{
    
 [CreateAssetMenu(menuName = "YAAS/Effects/Play Animation")]
public class PlayAnimationEffect : AbilityEffect
{
 [SerializeField] private string _animationToPlay;
 [SerializeField] private float _fixedTime = 0.2f;
 public override IEnumerator PerformEffect(AbilityCaster caller)
 {
  if (caller.TryGetComponent(out Animator animator))
  {
   animator.CrossFadeInFixedTime(_animationToPlay, _fixedTime);
   yield return null;
  }
  
 }
}
}

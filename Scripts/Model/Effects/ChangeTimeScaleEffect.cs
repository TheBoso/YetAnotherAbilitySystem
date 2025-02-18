using System.Collections;
using UnityEngine;

namespace YAAS
{
    
public  class ChangeTimeScaleEffect : AbilityEffect
{
    [SerializeField] private float _scale = 1.0f;
    [SerializeField] private float _secondsRealtime = 0.05f;
    public override IEnumerator PerformEffect(AbilityCaster caller)
    {
        Time.timeScale = _scale;
        yield return new WaitForSecondsRealtime(_secondsRealtime);
    }
}
}

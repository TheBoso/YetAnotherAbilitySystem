using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YAAS
{
    
    [CreateAssetMenu(fileName = "ChangeColourEffect", menuName = "YAAS/Effects/ChangeColourEffect", order = 1)]
public class ChangeColourEffect : AbilityEffect
{
    [SerializeField] private Color _colour;
    public override IEnumerator PerformEffect(AbilityCaster caller)
    {
        Debug.Log("Change Color");
        IEnumerable<Renderer> rends = caller.GetComponentsInChildren<Renderer>();
        foreach (Renderer rend in rends)
        {
            rend.material.color = _colour;
        }
        
        yield return null;
    }
}
}

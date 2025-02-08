using System.Collections;
using UnityEngine;
using YAAS;

public abstract class AbilityEffect : ScriptableObject
{
 public abstract IEnumerator PerformEffect(AbilityCaster caller);
}

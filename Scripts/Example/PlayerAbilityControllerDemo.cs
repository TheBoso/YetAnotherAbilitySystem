using System;
using UnityEngine;
using YAAS;

[RequireComponent(typeof(AbilityCaster))]
public class PlayerAbilityControllerDemo : MonoBehaviour
{
   private AbilityUIController _uiController;
   private AbilityCaster _caster;

   private void Awake()
   {
      _caster = GetComponent<AbilityCaster>();
      _uiController = FindFirstObjectByType<AbilityUIController>();
   }

   private void Start()
   {
      _uiController.SpawnAbilitiesFromCaster(_caster);
   }
}

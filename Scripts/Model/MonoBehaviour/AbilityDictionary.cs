using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace YAAS
{
public class AbilityDictionary : MonoBehaviour
{
    public static AbilityDictionary Instance;

    private Dictionary<string, AbilityDef> _abilities;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        _abilities = Resources.LoadAll<AbilityDef>("Abilitys")
            .ToDictionary(x => x.AbilityID, y => y);
    }

    public bool GetAbilityDef(string id, out AbilityDef abilityDef)
    {
        return Instance._abilities.TryGetValue(id, out abilityDef);
    }


}
}

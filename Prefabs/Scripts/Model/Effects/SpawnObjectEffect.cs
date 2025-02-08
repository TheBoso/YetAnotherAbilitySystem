using System.Collections;
using UnityEngine;

namespace YAAS
{
    
    [CreateAssetMenu(fileName = "SpawnObjectEffect", menuName = "YAAS/Effects/SpawnObjectEffect", order = 1)]
public class SpawnObjectEffect : AbilityEffect
{
    [SerializeField] private float _destroyAfterSeconds = -1.0f;
    [SerializeField] private GameObject _objectToSpawn;
    [SerializeField] private Vector3 _offsetFromCaller;
    [SerializeField] private Vector3 _scale = Vector3.one;
    [SerializeField] private Vector3 _eulerAngles = Vector3.zero;
    public override IEnumerator PerformEffect(AbilityCaster caller)
    {
        var obj = Instantiate(_objectToSpawn, caller.transform.position + _offsetFromCaller, 
            Quaternion.Euler(_eulerAngles));
        
        obj.transform.localScale = _scale;

        if (_destroyAfterSeconds > -1.0f)
        {
            Destroy(obj, _destroyAfterSeconds);
        }
        
        yield return null;
    }
}
}

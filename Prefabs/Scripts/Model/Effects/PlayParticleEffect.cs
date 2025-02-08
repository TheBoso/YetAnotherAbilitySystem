using System.Collections;
using UnityEngine;

namespace YAAS
{
    [CreateAssetMenu(fileName = "PlayParticleEffect", menuName = "YAAS/Effects/PlayParticleEffect", order = 1)]
    public class PlayParticleEffect : AbilityEffect
    {
        [SerializeField] private ParticleSystem _particleSystem;
        [SerializeField] private Vector3 _scale;
        [SerializeField] private Vector3 _position;
        [SerializeField] private Vector3 _eulerAngles;
        public override IEnumerator PerformEffect(AbilityCaster caller)
        {
            if (_particleSystem != null)
            {
                var particle = Instantiate(_particleSystem, _position + caller.transform.position, Quaternion.identity);
                particle.transform.localScale = _scale;
                particle.transform.eulerAngles = _eulerAngles;
                particle.Play();
                Destroy(particle.gameObject, particle.main.duration + 3.0f);
                
            }
            
            yield return null;
        }
    }
}

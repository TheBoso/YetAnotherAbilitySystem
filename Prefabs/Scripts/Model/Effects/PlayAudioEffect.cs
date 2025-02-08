using System.Collections;
using UnityEngine;

namespace YAAS
{
    [CreateAssetMenu(menuName = "YAAS/Effects/Play Audio")]
    public class PlayAudioEffect : AbilityEffect
    {
        [SerializeField] private AudioClip _audioClip;
        [SerializeField] private float _volume = 1.0f;
        [SerializeField] private float _pitch = 0.5f;
        public override IEnumerator PerformEffect(AbilityCaster caller)
        {
            if (_audioClip != null)
            {
               AudioSource.PlayClipAtPoint(_audioClip, caller.transform.position, _volume);
            }

            yield return null;
        }
    }
}
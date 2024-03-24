using UnityEngine;
using UnityEngine.Serialization;

namespace LethalConfig.MonoBehaviours.Managers
{
    internal class ConfigMenuAudioManager : MonoBehaviour
    {
        [FormerlySerializedAs("confirmSFX")] public AudioClip confirmSfx;
        [FormerlySerializedAs("cancelSFX")] public AudioClip cancelSfx;
        [FormerlySerializedAs("selectSFX")] public AudioClip selectSfx;
        [FormerlySerializedAs("hoverSFX")] public AudioClip hoverSfx;

        [FormerlySerializedAs("changeValueSFX")]
        public AudioClip changeValueSfx;

        public AudioSource audioSource;

        public void PlayConfirmSfx()
        {
            audioSource.PlayOneShot(confirmSfx);
        }

        public void PlayCancelSfx()
        {
            audioSource.PlayOneShot(cancelSfx);
        }

        public void PlayHoverSfx()
        {
            audioSource.PlayOneShot(hoverSfx);
        }

        public void PlaySelectSfx()
        {
            audioSource.PlayOneShot(selectSfx);
        }

        public void PlayChangeValueSfx()
        {
            audioSource.PlayOneShot(changeValueSfx);
        }
    }
}
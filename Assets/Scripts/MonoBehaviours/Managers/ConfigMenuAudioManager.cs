using UnityEngine;

namespace LethalConfig.MonoBehaviours.Managers
{
    internal class ConfigMenuAudioManager : MonoBehaviour
    {
        public AudioClip confirmSFX;
        public AudioClip cancelSFX;
        public AudioClip selectSFX;
        public AudioClip hoverSFX;
        public AudioClip changeValueSFX;

        public AudioSource audioSource;

        public void PlayConfirmSFX()
        {
            audioSource.PlayOneShot(confirmSFX);
        }

        public void PlayCancelSFX()
        {
            audioSource.PlayOneShot(cancelSFX);
        }

        public void PlayHoverSFX()
        {
            audioSource.PlayOneShot(hoverSFX);
        }

        public void PlaySelectSFX()
        {
            audioSource.PlayOneShot(selectSFX);
        }

        public void PlayChangeValueSFX()
        {
            audioSource.PlayOneShot(changeValueSFX);
        }
    }
}

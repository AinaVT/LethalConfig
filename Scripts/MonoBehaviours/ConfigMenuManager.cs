using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LethalConfig.MonoBehaviours
{
    internal class ConfigMenuAudioManager : MonoBehaviour
    {
        public AudioClip confirmSFX;
        public AudioClip cancelSFX;
        public AudioClip selectSFX;
        public AudioClip hoverSFX;
        public AudioClip changeValueSFX;

        private MenuManager menuManager;

        private void Awake()
        {
            menuManager = GameObject.Find("MenuManager").GetComponent<MenuManager>();
        }

        public void PlayConfirmSFX()
        {
            menuManager.MenuAudio.PlayOneShot(confirmSFX);
        }

        public void PlayCancelSFX()
        {
            menuManager.MenuAudio.PlayOneShot(cancelSFX);
        }

        public void PlayHoverSFX()
        {
            menuManager.MenuAudio.PlayOneShot(hoverSFX);
        }

        public void PlaySelectSFX()
        {
            menuManager.MenuAudio.PlayOneShot(selectSFX);
        }

        public void PlayChangeValueSFX()
        {
            menuManager.MenuAudio.PlayOneShot(changeValueSFX);
        }
    }
}

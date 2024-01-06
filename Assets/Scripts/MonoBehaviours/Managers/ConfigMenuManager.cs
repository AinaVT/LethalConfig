using LethalConfig.MonoBehaviours;
using LethalConfig.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LethalConfig
{
    internal class ConfigMenuManager : MonoBehaviour
    {
        public ConfigMenuAudioManager menuAudio;

        public static ConfigMenuManager Instance { get; private set; }

        private void Awake()
        {
            LogUtils.LogInfo("Create Config Menu Manager!");
            Instance = this;
        }

        private void OnDestroy()
        {
            LogUtils.LogInfo("Destroy Config Menu Manager!");
            Instance = null;
        }

        public void ShowConfigMenu()
        {
            FindObjectOfType<ConfigMenu>(true)?.gameObject.SetActive(true);
        }

        public void HideConfigMenu() 
        {
            FindObjectOfType<ConfigMenu>()?.gameObject.SetActive(false);
        }

        public void DisplayNotification(string message)
        {
            // Temporary
            LogUtils.LogInfo(message);
        }
    }
}

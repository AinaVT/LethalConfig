using LethalConfig.Mods;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace LethalConfig.MonoBehaviours
{
    internal class DescriptionBox : MonoBehaviour
    {
        public ModInfoBox modInfoBox;
        public ConfigInfoBox configInfoBox;

        private void Awake()
        {
            modInfoBox.gameObject.SetActive(false);
            configInfoBox.gameObject.SetActive(false);
        }

        public void ShowConfigInfo(string configInfo)
        {
            configInfoBox.gameObject.SetActive(true);
            configInfoBox.SetConfigInfo(configInfo);
            HideModInfo();
        }

        public void HideConfigInfo()
        {
            configInfoBox.gameObject.SetActive(false);
        }

        public void ShowModInfo(Mod mod)
        {
            modInfoBox.gameObject.SetActive(true);
            modInfoBox.SetModInfo(mod);
            HideConfigInfo();
        }

        public void HideModInfo()
        {
            modInfoBox.gameObject.SetActive(false);
        }
    }
}

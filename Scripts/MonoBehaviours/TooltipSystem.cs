using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LethalConfig
{
    internal class TooltipSystem : MonoBehaviour
    {
        private static TooltipSystem instance;

        public Tooltip tooltip;

        private void Awake()
        {
            instance = this;
        }

        public static void Show(string content)
        {
            if (instance == null) return;

            instance.tooltip.gameObject.SetActive(true);
            instance.tooltip.SetText(content);
        }

        public static void Hide()
        {
            if (instance == null) return;

            instance.tooltip.gameObject.SetActive(false);
        }
    }
}

using UnityEngine;

namespace LethalConfig.MonoBehaviours
{
    internal class TooltipSystem : MonoBehaviour
    {
        private static TooltipSystem _instance;

        public Tooltip tooltip;

        private void Awake()
        {
            _instance = this;
        }

        public static void Show(string content, GameObject target)
        {
            if (_instance == null) return;

            _instance.tooltip.gameObject.SetActive(true);
            _instance.tooltip.SetText(content);
            _instance.tooltip.SetTarget(target);
        }

        public static void Hide()
        {
            if (_instance == null) return;

            _instance.tooltip.gameObject.SetActive(false);
        }
    }
}
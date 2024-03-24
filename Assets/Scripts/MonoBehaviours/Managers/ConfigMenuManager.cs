using LethalConfig.Utils;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace LethalConfig.MonoBehaviours.Managers
{
    internal class ConfigMenuManager : MonoBehaviour
    {
        public ConfigMenuAudioManager menuAudio;

        public static ConfigMenuManager Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        private void OnDestroy()
        {
            Instance = null;
        }

        public static void ShowConfigMenu()
        {
            var menu = FindObjectOfType<ConfigMenu>(true);
            if (!menu)
            {
                LogUtils.LogWarning("ConfigMenu object not found");
                return;
            }

            menu.Open();
        }

        public static void DisplayNotification(string message, string button)
        {
            var menuNotification = FindObjectOfType<ConfigMenuNotification>(true);
            if (!menuNotification)
            {
                LogUtils.LogWarning("Notification object not found");
                return;
            }

            menuNotification.SetNotificationContent(message, button);
            menuNotification.Open();
            EventSystem.current.SetSelectedGameObject(menuNotification.GetComponentInChildren<Button>().gameObject);
        }
    }
}
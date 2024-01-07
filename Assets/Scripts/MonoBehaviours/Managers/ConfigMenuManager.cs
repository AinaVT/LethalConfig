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

        public void ShowConfigMenu()
        {
            FindObjectOfType<ConfigMenu>(true)?.Open();
        }

        public void DisplayNotification(string message, string button)
        {
            var menuNotification = FindObjectOfType<ConfigMenuNotification>(true);
            if (menuNotification == null)
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

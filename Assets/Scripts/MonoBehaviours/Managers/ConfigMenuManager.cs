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
            FindObjectOfType<ConfigMenu>(true)?.gameObject.SetActive(true);
        }

        public void HideConfigMenu() 
        {
            FindObjectOfType<ConfigMenu>()?.gameObject.SetActive(false);
        }

        public void DisplayNotification(string message, string button)
        {
            var menuNotification = FindObjectOfType<ConfigMenuNotification>();
            if (menuNotification == null) return;

            menuNotification.SetNotificationContent(message, button);
            menuNotification.gameObject.SetActive(true);
            EventSystem.current.SetSelectedGameObject(menuNotification.GetComponentInChildren<Button>().gameObject);
        }
    }
}

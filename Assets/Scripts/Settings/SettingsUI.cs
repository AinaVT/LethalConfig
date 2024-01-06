using LethalConfig.MonoBehaviours.Managers;
using LethalConfig.Utils;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace LethalConfig.Settings
{
    internal static class SettingsUI
    {
        private static bool hasInitialized = false;

        internal static void Init()
        {
            if (hasInitialized) return;

            SceneManager.sceneLoaded += OnSceneLoaded;
            hasInitialized = true;
        }

        private static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name == "MainMenu")
            {
                LogUtils.LogInfo("Injecting mod config menu into main menu...");

                var menuManager = GameObject.Find("MenuManager").GetComponent<MenuManager>();
                var menuContainer = GameObject.Find("MenuContainer");
                var mainButtons = menuContainer.transform.Find("MainButtons").gameObject;
                var creditsButton = mainButtons.transform.Find("Credits").gameObject;
                var menuNotification = menuContainer.transform.Find("MenuNotification").gameObject;

                // Adding manager to scene
                var manager = Object.Instantiate(Assets.ConfigMenuManagerPrefab);
                manager.transform.SetParent(menuManager.transform.parent); // Same level as menu manager.
                manager.transform.localPosition = Vector3.zero;

                // Adding menu to scene
                var configMenu = Object.Instantiate(Assets.ConfigMenuPrefab);
                configMenu.transform.SetParent(menuContainer.transform, false);
                configMenu.transform.localPosition = Vector3.zero;
                configMenu.transform.localScale = Vector3.one;
                configMenu.transform.localRotation = Quaternion.identity;
                configMenu.SetActive(false);

                // Adding notification ui to scene
                var notification = Object.Instantiate(Assets.ConfigMenuNotificationPrefab);
                notification.transform.SetParent(menuContainer.transform.parent, false);
                notification.transform.localPosition = Vector3.zero;
                notification.transform.localScale = Vector3.one;
                notification.transform.localRotation = Quaternion.identity;
                notification.SetActive(false);

                // Cloning main menu button for opening our menu
                var clonedButton = Object.Instantiate(creditsButton, mainButtons.transform);
                clonedButton.GetComponent<Button>().onClick.RemoveAllListeners();
                clonedButton.GetComponent<Button>().onClick = new Button.ButtonClickedEvent();
                clonedButton.GetComponent<Button>().onClick.AddListener(delegate { ConfigMenuManager.Instance.ShowConfigMenu(); });
                clonedButton.GetComponent<Button>().onClick.AddListener(delegate { ConfigMenuManager.Instance.menuAudio.PlayConfirmSFX(); });

                clonedButton.GetComponentInChildren<TextMeshProUGUI>().text = "> LethalConfig";

                var buttonsList = mainButtons.GetComponentsInChildren<Button>(true).Select(b => b.gameObject);
                foreach (var button in buttonsList.Where(g => g.name != "QuitButton" && g.name != "Credits"))
                {
                    button.GetComponent<RectTransform>().anchoredPosition += new Vector2(0, 38.5f);
                }

                clonedButton.GetComponent<RectTransform>().anchoredPosition = buttonsList.First(g => g.name == "Credits").GetComponent<RectTransform>().anchoredPosition + new Vector2(0, 38.5f);
            }
        }
    }
}

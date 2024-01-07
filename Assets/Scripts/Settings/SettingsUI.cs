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
            switch (scene.name)
            {
                case "MainMenu":
                    LoadUIForMainMenu();
                    break;
                case "SampleSceneRelay":
                    LoadUIForQuickMenu(scene.GetRootGameObjects().First(g => g.name == "Systems"));
                    break;
            }
        }

        private static void LoadUIForMainMenu()
        {
            LogUtils.LogInfo("Injecting mod config menu into main menu...");

            var menuContainer = GameObject.Find("MenuContainer");
            var mainButtonsTransform = menuContainer.transform.Find("MainButtons");
            var quitButton = mainButtonsTransform.Find("QuitButton").gameObject;

            InjectMenu(menuContainer.transform, mainButtonsTransform, quitButton);
        }

        private static void LoadUIForQuickMenu(GameObject systems)
        {
            LogUtils.LogInfo("Injecting mod config menu into in-game quick menu...");

            var quickMenu = systems.transform
                .Find("UI")
                .Find("Canvas")
                .Find("QuickMenu");
            var mainButtonsTransform = quickMenu.transform.Find("MainButtons");
            var quitButton = mainButtonsTransform.Find("Quit").gameObject;

            InjectMenu(quickMenu.transform, mainButtonsTransform, quitButton);
        }

        private static void InjectMenu(Transform parentTransform, Transform mainButtonsTransform, GameObject quitButton)
        {
            // Adding manager to scene
            var manager = Object.Instantiate(Assets.ConfigMenuManagerPrefab);
            manager.transform.SetParent(parentTransform); // Same level as menu manager.
            manager.transform.localPosition = Vector3.zero;

            // Adding menu to scene
            var configMenu = Object.Instantiate(Assets.ConfigMenuPrefab);
            configMenu.transform.SetParent(parentTransform, false);
            configMenu.transform.localPosition = Vector3.zero;
            configMenu.transform.localScale = Vector3.one;
            configMenu.transform.localRotation = Quaternion.identity;
            configMenu.SetActive(false);

            // Adding notification ui to scene
            var notification = Object.Instantiate(Assets.ConfigMenuNotificationPrefab);
            notification.transform.SetParent(parentTransform, false);
            notification.transform.localPosition = Vector3.zero;
            notification.transform.localScale = Vector3.one;
            notification.transform.localRotation = Quaternion.identity;
            notification.SetActive(false);

            // Cloning main menu button for opening our menu
            var clonedButton = Object.Instantiate(quitButton, mainButtonsTransform);
            clonedButton.GetComponent<Button>().onClick.RemoveAllListeners();
            clonedButton.GetComponent<Button>().onClick = new Button.ButtonClickedEvent();
            clonedButton.GetComponent<Button>().onClick.AddListener(delegate { ConfigMenuManager.Instance.ShowConfigMenu(); });
            clonedButton.GetComponent<Button>().onClick.AddListener(delegate { ConfigMenuManager.Instance.menuAudio.PlayConfirmSFX(); });

            clonedButton.GetComponentInChildren<TextMeshProUGUI>().text = "> LethalConfig";

            // Offsets all buttons inside the main buttons.
            // This is what makes me wish the game used a vertical layout group :T
            var buttonsList = mainButtonsTransform.GetComponentsInChildren<Button>(true)
                .Select(b => b.gameObject);

            // Gets the smallest distance between two buttons. Needs this since it turns out different
            // menus have different offsets. Cool.
            // This is awful and would also be unnecessary if the game used a vertial layout group
            // Oh well..
            var positions = buttonsList
                .Select(b => b.transform as RectTransform)
                .Select(t => t.anchoredPosition.y);
            var offset = positions
                .Zip(positions.Skip(1), (y1, y2) => Mathf.Abs(y2 - y1))
                .Max();

            foreach (var button in buttonsList.Where(g => g != quitButton))
            {
                button.GetComponent<RectTransform>().anchoredPosition += new Vector2(0, offset);
            }

            clonedButton.GetComponent<RectTransform>().anchoredPosition = quitButton.GetComponent<RectTransform>().anchoredPosition + new Vector2(0, offset);
        }
    }
}

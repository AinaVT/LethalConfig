using LethalConfig.Utils;
using System.Collections;
using System.Collections.Generic;
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

                var configMenu = Object.Instantiate(Prefabs.ConfigMenuPrefab);
                configMenu.transform.SetParent(menuContainer.transform, false);
                configMenu.transform.localPosition = Vector3.zero;
                configMenu.transform.localScale = Vector3.one;
                configMenu.transform.localRotation = Quaternion.identity;
                configMenu.transform.SetSiblingIndex(menuNotification.transform.GetSiblingIndex());
                configMenu.SetActive(false);

                var clonedButton = Object.Instantiate(creditsButton, mainButtons.transform);
                clonedButton.GetComponent<Button>().onClick.RemoveAllListeners();
                clonedButton.GetComponent<Button>().onClick = new Button.ButtonClickedEvent();
                clonedButton.GetComponent<Button>().onClick.AddListener(delegate { menuManager.EnableUIPanel(configMenu); });
                clonedButton.GetComponent<Button>().onClick.AddListener(delegate { menuManager.PlayConfirmSFX(); });

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

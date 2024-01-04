using LethalConfig;
using LethalConfig.MonoBehaviours;
using LethalConfig.MonoBehaviours.Components;
using LethalConfig.Utils;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LethalConfig.MonoBehaviours
{
    internal class ConfigMenu : MonoBehaviour
    {
        public ConfigList configList;
        public ConfigMenuAudioManager audioManager;
        private MenuManager menuManager;

        private void Awake()
        {
            LethalConfigManager.AutoGenerateMissingConfigsIfNeeded();
        }

        public void OnCancelButtonClicked()
        {
            if (!EnsureMenuManagerInstance()) return;

            var mods = LethalConfigManager.Mods;
            foreach (var item in mods.SelectMany(m => m.Value.configItems))
            {
                item.CancelChanges();
            }

            UpdateAppearanceOfCurrentComponents();

            menuManager.DisableUIPanel(gameObject);
            menuManager.PlayCancelSFX();
        }

        public void OnApplyButtonClicked()
        {
            if (!EnsureMenuManagerInstance()) return;

            var mods = LethalConfigManager.Mods;
            var itemsToSave = mods
                .SelectMany(m => m.Value.configItems)
                .Where(c => c.HasValueChanged).ToList();
            var restartRequiredItems = itemsToSave.Where(c => c.RequiresRestart).ToList();

            foreach (var item in itemsToSave)
            {
                item.ApplyChanges();
            }

            UpdateAppearanceOfCurrentComponents();

            menuManager.PlayConfirmSFX();

            LogUtils.LogInfo($"Saved config values for {itemsToSave.Count} items.");
            LogUtils.LogInfo($"Modified {restartRequiredItems.Count} item(s) that requires a restart.");
            if (restartRequiredItems.Count > 0)
            {
                // Show alert
                menuManager.DisplayMenuNotification($"Some of the modified settings may require a restart to take effect.", "[OK]");
            }
        }

        private bool EnsureMenuManagerInstance()
        {
            if (menuManager == null)
            {
                var menuManagerObject = GameObject.Find("MenuManager");
                if (menuManagerObject == null) return false;

                menuManager = menuManagerObject.GetComponent<MenuManager>();
                if (menuManager == null) return false;
            }

            return true;
        }

        private void UpdateAppearanceOfCurrentComponents()
        {
            foreach (var controller in configList.GetComponentsInChildren<ModConfigController>())
            {
                controller.UpdateAppearance();
            }
        }
    }

}
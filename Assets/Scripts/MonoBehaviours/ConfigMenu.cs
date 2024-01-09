using LethalConfig.MonoBehaviours.Components;
using LethalConfig.MonoBehaviours.Managers;
using LethalConfig.Utils;
using System.Linq;
using UnityEngine;

namespace LethalConfig.MonoBehaviours
{
    internal class ConfigMenu : MonoBehaviour
    {
        public ConfigList configList;
        public Animator menuAnimator;

        private void Awake()
        {
            LethalConfigManager.AutoGenerateMissingConfigsIfNeeded();
        }

        public void Open()
        {
            var animatorState = menuAnimator.GetCurrentAnimatorStateInfo(0);
            if (animatorState.IsName("ConfigMenuNormal") || animatorState.IsName("ConfigMenuAppear")) return;

            gameObject.SetActive(true);
            menuAnimator.SetTrigger("Open");
            transform.SetAsLastSibling();
        }

        public void Close(bool animated = true)
        {
            var animatorState = menuAnimator.GetCurrentAnimatorStateInfo(0);
            if (animatorState.IsName("ConfigMenuClosed") || animatorState.IsName("ConfigMenuDisappear")) return;

            var mods = LethalConfigManager.Mods;
            foreach (var item in mods.SelectMany(m => m.Value.configItems))
            {
                item.CancelChanges();
            }

            UpdateAppearanceOfCurrentComponents();

            if (!animated)
            {
                menuAnimator.SetTrigger("ForceClose");
                gameObject.SetActive(false);
                return;
            }

            menuAnimator.SetTrigger("Close");
        }

        public void OnCloseAnimationEnd()
        {
            gameObject.SetActive(false);
        }

        public void OnCancelButtonClicked()
        {
            Close();
            ConfigMenuManager.Instance.menuAudio.PlayCancelSFX();
        }

        public void OnApplyButtonClicked()
        {
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

            ConfigMenuManager.Instance.menuAudio.PlayConfirmSFX();

            LogUtils.LogInfo($"Saved config values for {itemsToSave.Count} items.");
            LogUtils.LogInfo($"Modified {restartRequiredItems.Count} item(s) that requires a restart.");
            if (restartRequiredItems.Count > 0)
            {
                // Show alert
                ConfigMenuManager.Instance.DisplayNotification($"Some of the modified settings may require a restart to take effect.", "OK");
            }
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
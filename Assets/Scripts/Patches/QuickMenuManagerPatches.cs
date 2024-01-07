using HarmonyLib;
using LethalConfig.MonoBehaviours;
using LethalConfig.Settings;
using UnityEngine;

namespace LethalConfig.Patches
{
    [HarmonyPatch(typeof(QuickMenuManager))]
    internal class QuickMenuManagerPatches
    {
        [HarmonyPatch("CloseQuickMenuPanels")]
        [HarmonyPostfix]
        public static void CloseQuickMenuPanelsPostFix(QuickMenuManager __instance)
        {
            var configMenu = __instance.menuContainer.transform.GetComponentInChildren<ConfigMenu>(true);
            var notification = __instance.menuContainer.transform.GetComponentInChildren<ConfigMenuNotification>(true);
            configMenu.Close(false);
            notification.Close(false);
        }

        [HarmonyPatch("Start")]
        [HarmonyPostfix]
        public static void StartPostFix(QuickMenuManager __instance)
        {
            var quickMenu = __instance.GetComponentInParent<Canvas>()?.transform
                .Find("QuickMenu");
            var mainButtonsTransform = quickMenu.transform.Find("MainButtons");
            var quitButton = mainButtonsTransform.Find("Quit").gameObject;

            MenusUtils.InjectMenu(quickMenu.transform, mainButtonsTransform, quitButton);
        }
    }
}

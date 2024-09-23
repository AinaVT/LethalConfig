using HarmonyLib;
using LethalConfig.MonoBehaviours;
using LethalConfig.Utils;

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
            if (configMenu) configMenu.Close(false);

            var notification = __instance.menuContainer.transform.GetComponentInChildren<ConfigMenuNotification>(true);
            if (notification) notification.Close(false);
        }

        [HarmonyPatch("Start")]
        [HarmonyPostfix]
        public static void StartPostFix(QuickMenuManager __instance)
        {
            LogUtils.LogDebug("Injecting mod config menu into quick menu...");
            var quickMenu = __instance.menuContainer;
            if (!quickMenu) return;

            var mainButtonsTransform = quickMenu.transform.Find("MainButtons");
            if (!mainButtonsTransform) return;

            var quitButton = mainButtonsTransform.Find("Quit");
            if (!quitButton) return;

            MenusUtils.InjectMenu(quickMenu.transform, mainButtonsTransform, quitButton.gameObject);
        }
    }
}
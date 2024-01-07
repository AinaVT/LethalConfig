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
            var notification = __instance.menuContainer.transform.GetComponentInChildren<ConfigMenuNotification>(true);
            configMenu.Close(false);
            notification.Close(false);
        }
    }
}

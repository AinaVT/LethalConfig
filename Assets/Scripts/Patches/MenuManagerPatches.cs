using System.Collections;
using HarmonyLib;
using LethalConfig.Utils;
using UnityEngine;

namespace LethalConfig.Patches
{
    [HarmonyPatch(typeof(MenuManager))]
    internal class MenuManagerPatches
    {
        [HarmonyPatch("Start")]
        [HarmonyPostfix]
        public static void StartPostFix(MenuManager __instance)
        {
            // Workaround to deal with other mods that do things to the main menu as well.
            // Runs the injection code a frame later, that way it runs *after* everyone else
            // is done doing their stuff.
            // Very hacky, probably needs an alternative in the future.
            __instance.StartCoroutine(DelayedMainMenuInjection());
        }

        private static IEnumerator DelayedMainMenuInjection()
        {
            yield return new WaitForSeconds(0);
            InjectToMainMenu();
        }

        private static void InjectToMainMenu()
        {
            LogUtils.LogInfo("Injecting mod config menu into main menu...");

            var menuContainer = GameObject.Find("MenuContainer");
            if (!menuContainer) return;

            var mainButtonsTransform = menuContainer.transform.Find("MainButtons");
            if (!mainButtonsTransform) return;

            var quitButton = mainButtonsTransform.Find("QuitButton");
            if (!quitButton) return;

            MenusUtils.InjectMenu(menuContainer.transform, mainButtonsTransform, quitButton.gameObject);
        }
    }
}
using HarmonyLib;
using LethalConfig.MonoBehaviours;
using LethalConfig.Settings;
using LethalConfig.Utils;
using System.Collections;
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
            var mainButtonsTransform = menuContainer.transform.Find("MainButtons");
            var quitButton = mainButtonsTransform.Find("QuitButton").gameObject;

            MenusUtils.InjectMenu(menuContainer.transform, mainButtonsTransform, quitButton);
        }
    }
}

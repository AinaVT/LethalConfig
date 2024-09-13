using HarmonyLib;

namespace LethalConfig.Patches
{
    [HarmonyPatch(typeof(StartOfRound))]
    internal class StartOfRoundPatches
    {
        [HarmonyPatch("Start")]
        [HarmonyPostfix]
        public static void StartPostFix()
        {
            // Call LateRegistration Config Files
            // Adds any final config items that were not already added
            // Very hacky, probably needs an alternative in the future.
            LethalConfigManager.RunLateAutoGeneration();
        }
    }
}
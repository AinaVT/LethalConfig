using BepInEx;
using HarmonyLib;
using LethalConfig.Utils;

namespace LethalConfig
{
    internal static class PluginInfo
    {
        public const string Guid = "ainavt.lc.lethalconfig";
        public const string Name = "LethalConfig";
        public const string Version = "1.4.1";
    }

    [BepInPlugin(PluginInfo.Guid, PluginInfo.Name, PluginInfo.Version)]
    internal class LethalConfigPlugin : BaseUnityPlugin
    {
        private static LethalConfigPlugin _instance;
        private static Harmony _harmony;

        private void Awake()
        {
            if (_instance == null) _instance = this;

            LogUtils.Init(PluginInfo.Guid);
            Configs.Initialize(Config);

            if (Configs.IsLethalConfigHidden.Value)
            {
                LogUtils.LogInfo("LethalConfig is hidden and will not load.");
                return;
            }

            Assets.Init();

            _harmony = new Harmony(PluginInfo.Guid);
            _harmony.PatchAll();

            LogUtils.LogInfo("LethalConfig loaded!");
        }
    }
}
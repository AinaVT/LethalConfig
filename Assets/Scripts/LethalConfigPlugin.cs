using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using LethalConfig.ConfigItems;
using LethalConfig.ConfigItems.Options;
using LethalConfig.MonoBehaviours.Managers;
using LethalConfig.Utils;
using System.IO;
using System.Linq;
using UnityEngine;

namespace LethalConfig
{
    internal static class PluginInfo
    {
        public const string Guid = "ainavt.lc.lethalconfig";
        public const string Name = "LethalConfig";
        public const string Version = "1.4.0";
    }

    [BepInPlugin(PluginInfo.Guid, PluginInfo.Name, PluginInfo.Version)]
    internal class LethalConfigPlugin : BaseUnityPlugin
    {
        private static LethalConfigPlugin instance;
        private static Harmony harmony;

        private void Awake()
        {
            if (instance == null) instance = this;

            LogUtils.Init(PluginInfo.Guid);
            Configs.Initialize(Config);

            if (Configs.IsLethalConfigHidden.Value) 
            {
                LogUtils.LogInfo("LethalConfig is hidden and will not load.");
                return;
            }

            Assets.Init();

            harmony = new Harmony(PluginInfo.Guid);
            harmony.PatchAll();

            LogUtils.LogInfo("LethalConfig loaded!");
        }
    }

}
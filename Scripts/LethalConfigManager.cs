using BepInEx;
using LethalConfig.ConfigItems;
using LethalConfig.Mods;
using LethalConfig.Utils;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace LethalConfig
{
    public static class LethalConfigManager
    {
        internal static Dictionary<string, Mod> Mods { get; private set; } = new Dictionary<string, Mod>();

        public static void AddConfigItem(BaseConfigItem configItem)
        {
            var mod = ModForAssembly(Assembly.GetCallingAssembly());
            if (mod == null)
            {
                LogUtils.LogWarning("Mod for calling assembly not found.");
                return;
            }
            configItem.Owner = mod;
            mod.AddConfigItem(configItem);
            LogUtils.LogInfo($"Registered config \"{configItem}\"");
        }

        private static Mod ModForAssembly(Assembly assembly)
        {
            if (assembly.TryGetModInfo(out var modInfo))
            {
                if (Mods.TryGetValue(modInfo.GUID, out var mod)) return mod;

                var newMod = new Mod(modInfo);
                Mods.Add(modInfo.GUID, newMod);
                return newMod;
            }

            return null;
        }
    } 
}

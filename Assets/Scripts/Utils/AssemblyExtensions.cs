using BepInEx;
using LethalConfig.Mods;
using System;
using System.Linq;
using System.Reflection;

namespace LethalConfig.Utils
{
    internal static class AssemblyExtensions
    {
        internal static bool TryGetModInfo(this Assembly assembly, out ModInfo modInfo)
        {
            modInfo = new ModInfo();

            var plugin = assembly.FindPluginAttribute();
            if (plugin == null) return false;

            modInfo.Name = plugin.Name;
            modInfo.GUID = plugin.GUID;
            modInfo.Version = plugin.Version.ToString();

            return true;
        }

        internal static BepInPlugin FindPluginAttribute(this Assembly assembly)
        {
            Type[] types;

            try
            {
                types = assembly.GetTypes();
            } catch (ReflectionTypeLoadException e)
            {
                types = e.Types.Where(t => t != null).ToArray();
            }

            foreach (var type in types)
            {
                var plugin = type.GetCustomAttribute<BepInPlugin>();
                if (plugin == null) continue;
                return plugin;
            }

            return null;
        }
    } 
}

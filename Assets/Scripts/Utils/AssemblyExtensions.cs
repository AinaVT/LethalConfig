using System;
using System.Linq;
using System.Reflection;
using BepInEx;
using LethalConfig.Mods;

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
            modInfo.Guid = plugin.GUID;
            modInfo.Version = plugin.Version.ToString();

            return true;
        }

        private static BepInPlugin FindPluginAttribute(this Assembly assembly)
        {
            Type[] types;

            try
            {
                types = assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException e)
            {
                types = e.Types.Where(t => t != null).ToArray();
            }

            return types.Select(type => type.GetCustomAttribute<BepInPlugin>())
                .FirstOrDefault(plugin => plugin != null);
        }
    }
}
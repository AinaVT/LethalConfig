using BepInEx;
using LethalConfig.Mods;
using System.Reflection;

namespace LethalConfig.Utils
{
    internal static class AssemblyExtensions
    {
        internal static bool TryGetModInfo(this Assembly assembly, out ModInfo modInfo)
        {
            modInfo = new ModInfo();

            var types = assembly.GetTypes();

            foreach (var type in types)
            {
                var plugin = type.GetCustomAttribute<BepInPlugin>();
                if (plugin == null) continue;

                modInfo.Name = plugin.Name;
                modInfo.GUID = plugin.GUID;
                modInfo.Version = plugin.Version.ToString();

                return true;
            }

            return false;
        }
    } 
}

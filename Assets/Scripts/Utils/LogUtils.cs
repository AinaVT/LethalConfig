using BepInEx;
using BepInEx.Logging;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace LethalConfig.Utils
{
    internal static class LogUtils
    {
        private static Dictionary<Assembly, ManualLogSource> logSources = new Dictionary<Assembly, ManualLogSource>();

        public static void Init(string pluginGuid)
        {
            var assembly = Assembly.GetCallingAssembly();
            if (logSources.ContainsKey(assembly)) return;
            logSources.Add(assembly, BepInEx.Logging.Logger.CreateLogSource(pluginGuid));
        }

        public static void LogInfo(string message) { logSources[Assembly.GetCallingAssembly()].LogInfo(message); }
        public static void LogWarning(string message) { logSources[Assembly.GetCallingAssembly()].LogWarning(message); }
        public static void LogError(string message) { logSources[Assembly.GetCallingAssembly()].LogError(message); }
        public static void LogFatal(string message) { logSources[Assembly.GetCallingAssembly()].LogFatal(message); }

    }  
}

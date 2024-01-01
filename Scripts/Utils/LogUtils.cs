using BepInEx;
using BepInEx.Logging;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LethalConfig.Utils
{
    internal static class LogUtils
    {
        private static ManualLogSource logSource;

        internal static void Init()
        {
            logSource = BepInEx.Logging.Logger.CreateLogSource(PluginInfo.Guid);
        }

        internal static void LogInfo(string message) { logSource.LogInfo(message); }
        internal static void LogWarning(string message) { logSource.LogWarning(message); }
        internal static void LogError(string message) { logSource.LogError(message); }
        internal static void LogFatal(string message) { logSource.LogFatal(message); }

    } 
}

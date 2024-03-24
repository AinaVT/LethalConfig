using BepInEx.Logging;

namespace LethalConfig.Utils
{
    internal static class LogUtils
    {
        private static ManualLogSource _logSource;

        public static void Init(string pluginGuid)
        {
            _logSource = Logger.CreateLogSource(pluginGuid);
        }

        public static void LogDebug(string message)
        {
            _logSource?.LogDebug(message);
        }

        public static void LogInfo(string message)
        {
            _logSource?.LogInfo(message);
        }

        public static void LogWarning(string message)
        {
            _logSource?.LogWarning(message);
        }

        public static void LogError(string message)
        {
            _logSource?.LogError(message);
        }

        public static void LogFatal(string message)
        {
            _logSource?.LogFatal(message);
        }
    }
}
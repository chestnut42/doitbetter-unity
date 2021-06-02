using UnityEngine;

namespace Plugins.GameBoost.Core
{
    public static class GBLog
    {
        public static bool LoggingEnabled = false;


        public static void SetLoggingEnabled(bool loggingEnabled)
        {
            GBLog.LoggingEnabled = loggingEnabled;
        }

        public static void LogError(object message)
        {
            Debug.LogWarning($"Game Boost SDK [Err]: {message}");
        }

        public static void LogDebug(object message)
        {
            if (GBLog.LoggingEnabled)
            {
                Debug.Log($"Game Boost SDK [Inf]: {message}");
            }
        }
    }
}

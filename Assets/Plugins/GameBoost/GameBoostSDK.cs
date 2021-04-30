using System;
using System.Collections.Generic;
using UnityEngine;

namespace Plugins.GameBoost
{
    public static partial class GameBoostSDK
    {
        private static GBImplementation gbImplementation;
        private static bool isInitialized => gbImplementation != null;


        /// <summary>
        /// Call this method to initialize SDK.
        /// This method MUST be called only once.
        /// </summary>
        /// <param name="apiKey">The API key to use</param>
        public static void Initialize(string apiKey)
        {
            if (isInitialized)
            {
                GBLog.LogError("SDK is already initialized. Remove a second call to Initialize");
                return;
            }
            if (string.IsNullOrEmpty(apiKey))
            {
                GBLog.LogError("API Key is null or empty. Pass a valid API key");
            }

            gbImplementation = new GBImplementation(apiKey);
        }


        /// <summary>
        /// Set's verbose logging mode.
        /// Capture logs sending true with this method if
        /// you plan to contact support.
        /// </summary>
        /// <param name="isLoggingEnabled">whether to do verbose logging</param>
        public static void SetLoggingEnabled(bool isLoggingEnabled)
        {
            GBLog.LoggingEnabled = isLoggingEnabled;
            gbImplementation?.SetLoggingEnabled(isLoggingEnabled);
        }
    }
}

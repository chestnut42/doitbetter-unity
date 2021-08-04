using System;
using System.Collections.Generic;
using Plugins.GameBoost.Core;

namespace Plugins.GameBoost
{
    public static partial class GameBoostSDK
    {
        internal static GameBoostEvents events { get; } = new GameBoostEvents();
        private static ISDKImplementation sdkImplementation;
        private static bool isInitialized => sdkImplementation != null;
        
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
                return;
            }

            try
            {
                var internalImplementation = SDKImplementationFactory.CreateImplementation(apiKey, events);
                sdkImplementation = new CatchingSDKImplementation(internalImplementation);
                sdkImplementation.SetLoggingEnabled(GBLog.LoggingEnabled);
            }
            catch (Exception exception)
            {
                GBLog.LogError($"Exception during Initialize: {exception}");
                sdkImplementation = null;
            }
        }

        public static IGameBoostEvents Events
        {
            get { return events; }
        }
        
        /// <summary>
        /// Set's verbose logging mode.
        /// Capture logs sending true to this method if
        /// you plan to contact GameBoost SDK support.
        /// </summary>
        /// <param name="isLoggingEnabled">whether to do verbose logging</param>
        public static void SetLoggingEnabled(bool isLoggingEnabled)
        {
            GBLog.LoggingEnabled = isLoggingEnabled;
            sdkImplementation?.SetLoggingEnabled(isLoggingEnabled);
        }


        /// <summary>
        /// Call this method whenever you detected that this application is
        /// a beta or development build. I.e. it's run buy developer or QA
        /// engineer instead of real user.
        ///
        /// If you use #defines, flags or settings to detect development build
        /// call this method as soon as you have detected that this
        /// flag/setting/#define is on.
        ///
        /// If you use development menu (e.g. 10 taps on left top corner)
        /// call this method as soon as this menu is open.
        ///
        /// It's safe to call it as many times as you need. It remembers the state
        /// inside.
        /// </summary>
        public static void MarkAsDevelopment()
        {
            sdkImplementation?.MarkAsDevelopment();
        }


        /// <summary>
        /// Start with calling this function to create a game object.
        /// Game object encapsulates current balance and provides
        /// methods to create game specific event trackers.
        /// </summary>
        /// <param name="balance"></param>
        /// <returns></returns>
        public static IGame CreateGame(Dictionary<string, object> balance)
        {
            if (!isInitialized)
            {
                GBLog.LogError("SDK is not initialized. Initialize SDK prior to creating an IGame");
            }

            return sdkImplementation?.CreateGame(balance);
        }
    }
}

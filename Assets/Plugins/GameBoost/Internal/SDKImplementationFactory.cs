using System.Collections.Generic;
using Plugins.GameBoost.Core;
#if UNITY_EDITOR
using Plugins.GameBoost.UnityRequest;
using UnityEditor;
#endif

namespace Plugins.GameBoost
{
    internal static class SDKImplementationFactory
    {
        public static ISDKImplementation CreateImplementation(string apiKey)
        {
            var pluginMethods = CreatePluginMethods();
            pluginMethods.InitializeWith(apiKey);

            var jsonSerializer = new JsonSerializer();
            var pluginEventTracker = new PluginEventTracker(jsonSerializer, pluginMethods);
            var revenueTracker = new RevenueTracker(pluginEventTracker);

#if UNITY_EDITOR && (UNITY_IPHONE || UNITY_ANDROID)
            var unityEventTracker = new UnityRequestEventTrackerWrapper(
                UnityRequestEventTrackerFactory.CreateTracker(
                    PlayerSettings.applicationIdentifier,
                    apiKey,
                    PlayerSettings.bundleVersion,
                    jsonSerializer
                )
            );
            var gameTracker = new GameTracker(unityEventTracker);
#else
            var gameTracker = new GameTracker(pluginEventTracker);
#endif

            return new CombinedSDK(revenueTracker, pluginMethods, gameTracker);
        }

        private static IPluginMethods CreatePluginMethods()
        {
#if UNITY_IPHONE && !UNITY_EDITOR
            return new PluginMethodsIos();
#elif UNITY_ANDROID && !UNITY_EDITOR
            return new PluginMethodsAndroid();
#else
            return new PluginMethodsLogging();
#endif
        }

#if UNITY_EDITOR
        // This translation is necessary as UnityRequest assembly can't depend
        // on this assembly. Because this assembly already references
        // UnityRequest assembly.
        // We can move IEventTracker to Core or some other assembly and make it public,
        // but why bother.
        private class UnityRequestEventTrackerWrapper : IEventTracker
        {
            private readonly IUnityRequestEventTracker unityRequestEventTracker;

            public UnityRequestEventTrackerWrapper(
                IUnityRequestEventTracker unityRequestEventTracker
            )
            {
                this.unityRequestEventTracker = unityRequestEventTracker;
            }

            public void SendEvent(string eventName, Dictionary<string, object> eventData, string deduplicateId)
            {
                unityRequestEventTracker.SendEvent(eventName, eventData, deduplicateId);
            }
        }
#endif
    }
}

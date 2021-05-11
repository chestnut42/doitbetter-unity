using System.Collections.Generic;
using Plugins.GameBoost.Core;

namespace Plugins.GameBoost
{
    internal partial class GBImplementation : IEventTracker
    {
        private readonly IJsonSerializer jsonSerializer = new JsonSerializer();
        private readonly IKeyGenerator keyGenerator = new JsonKeyGenerator(
            new JsonSerializer(),
            new SHA2Function(),
            new Base64DataEncoder()
        );

        private readonly IPluginMethods pluginMethods;

        public GBImplementation(string apiKey)
        {
            pluginMethods = CreatePluginMethods();
            pluginMethods.InitializeWith(apiKey);
        }


        public void SetLoggingEnabled(bool isLoggingEnabled)
        {
            pluginMethods.SetLoggingEnabled(isLoggingEnabled);
        }


        public void SendEvent(
            string eventName,
            Dictionary<string, object> eventData,
            string deduplicateId = null
        )
        {
            var jsonString = jsonSerializer.Serialize(eventData, false);
            pluginMethods.SendEvent(eventName, jsonString, deduplicateId);
        }


        public IGame CreateGame(
            Dictionary<string, object> balance
        )
        {
            return new GameInstance(
                keyGenerator,
                this,
                keyGenerator.GenerateKey(balance));
        }


        private static IPluginMethods CreatePluginMethods()
        {
#if UNITY_IPHONE && !UNITY_EDITOR
            return new PluginMethodsIos();
# elif UNITY_ANDROID && !UNITY_EDITOR
            return new PluginMethodsAndroid();
#else
            return new PluginMethodsEditor();
#endif
        }
    }
}

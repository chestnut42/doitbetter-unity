using System.Collections.Generic;
using Plugins.GameBoost.Core;

namespace Plugins.GameBoost
{
    internal partial class GBImplementation
    {
        private readonly IJsonSerializer jsonSerializer = new JsonSerializer();
        private readonly IHashFunction hashFunction = new SHA2Function();
        private readonly IDataEncoder dataEncoder = new Base64DataEncoder();

        private readonly IPluginMethods pluginMethods;

        public GBImplementation(string apiKey)
        {
            pluginMethods = CreatePluginMethods();
            pluginMethods.InitializeWith(apiKey);
        }


        public void SetLoggingEnabled(bool isLoggingEnabled)
        {
            GBLog.LoggingEnabled = isLoggingEnabled;
            pluginMethods.SetLoggingEnabled(isLoggingEnabled);
        }


        private void SendEvent(
            string eventName,
            Dictionary<string, object> eventData,
            string deduplicateId = null
        )
        {
            var jsonString = jsonSerializer.Serialize(eventData, false);
            pluginMethods.SendEvent(eventName, jsonString, deduplicateId);
        }


        private static IPluginMethods CreatePluginMethods()
        {
#if UNITY_IPHONE && !UNITY_EDITOR
            return new PluginMethodsIos();
#else
            return new PluginMethodsEditor();
#endif
        }
    }
}

using System.Collections.Generic;
using Plugins.GameBoost.Core;

namespace Plugins.GameBoost
{
    internal class PluginMethodsEditor : IPluginMethods
    {
        public void InitializeWith(string apiKey)
        {
            GBLog.LogDebug($"Initialized with API key: {apiKey}");
        }

        public void SendEvent(string eventName, string jsonString)
        {
            GBLog.LogDebug($"Sending event with name <{eventName}>, params: {jsonString}");
        }

        public void SetLoggingEnabled(bool isLoggingEnabled)
        {
            GBLog.LogDebug($"Setting logging enabled to: {isLoggingEnabled}");
        }
    }
}

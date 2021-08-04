using Plugins.GameBoost.Core;

namespace Plugins.GameBoost
{
    internal class PluginMethodsLogging : IPluginMethods
    {

        public PluginMethodsLogging(GameBoostEvents events)
        { }

        public void InitializeWith(string apiKey)
        {
            GBLog.LogDebug($"Initialized with API key: {apiKey}");
        }

        public void SendEvent(string eventName, string jsonString, string deduplicateId)
        {
            GBLog.LogDebug($"Sending event with name <{eventName}>" +
                           $", params: {jsonString}" +
                           $", deduplicate ID: {deduplicateId}");
        }

        public void SetLoggingEnabled(bool isLoggingEnabled)
        {
            GBLog.LogDebug($"Setting logging enabled to: {isLoggingEnabled}");
        }

        public void MarkAsDevelopment()
        {
            GBLog.LogDebug($"Mark as development called");
        }
    }
}

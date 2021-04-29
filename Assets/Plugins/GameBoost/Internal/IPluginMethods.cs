using System.Collections.Generic;

namespace Plugins.GameBoost
{
    internal interface IPluginMethods
    {
        void InitializeWith(string apiKey);
        void SendEvent(string eventName, string jsonString, string deduplicateId);
        void SetLoggingEnabled(bool isLoggingEnabled);
    }
}

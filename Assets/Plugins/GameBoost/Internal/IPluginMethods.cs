namespace Plugins.GameBoost
{
    interface IPluginMethods : ILoggingSettings
    {
        void InitializeWith(string apiKey);
        void SendEvent(string eventName, string jsonString, string deduplicateId);
    }
}

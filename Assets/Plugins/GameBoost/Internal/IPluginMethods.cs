namespace Plugins.GameBoost
{
    interface IPluginMethods : ISettings
    {
        void InitializeWith(string apiKey);
        void SendEvent(string eventName, string jsonString, string deduplicateId);
    }
}

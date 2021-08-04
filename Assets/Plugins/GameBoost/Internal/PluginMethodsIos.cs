#if UNITY_IPHONE && !UNITY_EDITOR
using System.Runtime.InteropServices;

namespace Plugins.GameBoost
{
    internal class PluginMethodsIos : IPluginMethods
    {
        private GameBoostEventsiOs iOsEvents;
s
        public PluginMethodsIos(GameBoostEvents events)
        {
            iOsEvents = new GameBoostEventsiOs(events);
        }

        public void InitializeWith(string apiKey)
        {
            _initializeWith(apiKey);
        }

        public void SendEvent(string eventName, string jsonString, string deduplicateId)
        {
            _sendEvent(eventName, jsonString, deduplicateId);
        }

        public void SetLoggingEnabled(bool isLoggingEnabled)
        {
            _enableLogging(isLoggingEnabled);
        }

        public void MarkAsDevelopment()
        {
            // nothing to do -> iOS SDK can detect sandbox
        }


        [DllImport("__Internal")]
        private static extern void _initializeWith(string apiKey);

        [DllImport("__Internal")]
        private static extern void _sendEvent(string eventName, string jsonString, string deduplicateId);

        [DllImport("__Internal")]
        private static extern void _enableLogging(bool loggingEnabled);
    }

    class GameBoostEventsiOs: IGameBoostEvents
    {
        GameBoostEvents events;
        public GameBoostEventsiOs(GameBoostEvents events) {
            this.events = events;
        }

        void busMessage(String type, String content) {
            
        }
    }
}
#endif

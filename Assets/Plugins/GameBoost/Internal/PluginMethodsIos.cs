// #if UNITY_IPHONE && !UNITY_EDITOR
#if UNITY_IPHONE

using System;
using System.Runtime.InteropServices;
using Plugins.GameBoost.Core;

namespace Plugins.GameBoost
{
    internal class PluginMethodsIos : IPluginMethods
    {
        private GameBoostEventsiOs iOsEvents;

        public PluginMethodsIos(GameBoostEvents events)
        {
            iOsEvents = new GameBoostEventsiOs(events);
        }

        public void InitializeWith(string apiKey)
        {
            _initializeWith(apiKey, GameBoostEventsiOs.busMessage);
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
        private static extern void _initializeWith(string apiKey, GameBoostEventsiOs.BusMessageType busMethod);

        [DllImport("__Internal")]
        private static extern void _sendEvent(string eventName, string jsonString, string deduplicateId);

        [DllImport("__Internal")]
        private static extern void _enableLogging(bool loggingEnabled);
    }

    class GameBoostEventsiOs
    {
        private static IGameBoostEventsBus eventsBus;
        
        public GameBoostEventsiOs(IGameBoostEventsBus eventsBus) {
            GameBoostEventsiOs.eventsBus = eventsBus;
        }

        public delegate void BusMessageType(string type, string content);

        [AOT.MonoPInvokeCallback(typeof(BusMessageType))]
        public static void busMessage(string type, string content)
        {
             try
             {
                 eventsBus.receiveBusMessage(type, content);
             }
             catch (Exception ex) 
             {
                 GBLog.LogError($"receiveBusMessage  ex: {ex.Message} for type: {type}");
             }        
        }
    }
}
#endif
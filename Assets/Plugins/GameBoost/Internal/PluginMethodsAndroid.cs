// #if UNITY_ANDROID && !UNITY_EDITOR
#if UNITY_ANDROID
using System;
using UnityEngine;
using Plugins.GameBoost.Core;

namespace Plugins.GameBoost
{
    internal class PluginMethodsAndroid : IPluginMethods
    {
        private AndroidJavaClass androidJavaClass;
        private GameBoostEventsAndroid gbEvents;

        public PluginMethodsAndroid(IGameBoostEventsBus events)
        {
            gbEvents = new GameBoostEventsAndroid(events);
            androidJavaClass = new AndroidJavaClass("com.doitbetter.sdk.u3d.U3DGameBoostSDK");
        }

        public void InitializeWith(string apiKey)
        {
            androidJavaClass.CallStatic("initialize", apiKey, gbEvents);
        }

        public void SendEvent(string eventName, string jsonString, string deduplicateId)
        {
            androidJavaClass.CallStatic("sendEvent", eventName, jsonString, deduplicateId);
        }

        public void SetLoggingEnabled(bool isLoggingEnabled)
        {
            androidJavaClass.CallStatic("enableLogging", isLoggingEnabled);
        }

        public void MarkAsDevelopment()
        {
            androidJavaClass.CallStatic("markAsDevelopment");
        }
    }

    class GameBoostEventsAndroid : AndroidJavaProxy
    {
        private IGameBoostEventsBus eventsBus;

        public GameBoostEventsAndroid(IGameBoostEventsBus eventsBus) : base("com.doitbetter.sdk.outbus.OutMessageBus") {
            this.eventsBus = eventsBus;
        }

        void busMessage(string type, string content) {
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

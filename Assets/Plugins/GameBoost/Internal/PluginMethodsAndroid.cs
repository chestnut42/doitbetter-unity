// #if UNITY_ANDROID && !UNITY_EDITOR
#if UNITY_ANDROID
using System.Collections.Generic;
using UnityEngine;
using Plugins.GameBoost.Core;

namespace Plugins.GameBoost
{
    internal class PluginMethodsAndroid : IPluginMethods
    {
        private AndroidJavaClass androidJavaClass;
        private GameBoostEventsAndroid gbEvents;

        public PluginMethodsAndroid(GameBoostEvents events)
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
        private const string SANDBOX_STATUS = "SANDBOX_STATUS";
        private GameBoostEvents events;

        public GameBoostEventsAndroid(GameBoostEvents events) : base("com.doitbetter.sdk.u3d.UnityInputCallback") {
            this.events = events;
        }

        void busMessage(string type, string content) {
            switch (type)
            {
                case SANDBOX_STATUS:
                    ReceiveStatus(content);
                    break;
                default:
                    GBLog.LogError($"busMessage recieve unsupported type == {type}");
                    break;
            }
        }

        private void ReceiveStatus(string value)
        {
            var mapping = new Dictionary<string, SandboxStatus>()
            {
                {"ukwn", SandboxStatus.Unknown},
                {"sdbx", SandboxStatus.Sandbox},
                {"prod", SandboxStatus.Production}
            };

            if (mapping.ContainsKey(value))
            {
                events.Post(mapping[value]);    
            }
            else
            {
                GBLog.LogError($"SandboxStatus receive unsupported type == {value}");                
            }
        }
    }

}
#endif

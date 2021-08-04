#if UNITY_ANDROID && !UNITY_EDITOR
using UnityEngine;

namespace Plugins.GameBoost
{
    delegate void GBNativeCallback(String type, String content);

    internal class PluginMethodsAndroid : IPluginMethods
    {
        private AndroidJavaClass androidJavaClass;
        private GameBoostEventsAndroid gbEvents;

        public IGameBoostEvents GameBoostEvents { return gbEvents; }

        public PluginMethodsAndroid()
        {
            gbEvents = new GameBoostEventsAndroid();
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

        [AOT.MonoPInvokeCallback(typeof(MyFuncType))]
        static void nativeCallback(String type, String content) { }
        
        static extern void RegisterCallback(GBNativeCallback func);
    }

    class GameBoostEventsAndroid : AndroidJavaProxy
    {
        private const string SANDBOX_STATUS = "SANDBOX_STATUS";

        public event IGameBoostEvents.SandboxStatusHandler sandboxStatus;
        
        GameBoostEvents events;

        public GameBoostEventsAndroid(GameBoostEvents events) : base("com.doitbetter.sdk.u3d.UnityInputCallback") {
            this.events = events;
        }

        void busMessage(String type, String content) {
            switch (type)
            {
                case SANDBOX_STATUS:
                    recieveStatus(content);
                    break;
                default:
                    GBLog.LogError($"busMessage recieve unsupported type == {type}");
                    break;
            }
        }

        private void recieveStatus(String value) {
            var status = SandboxStatus.Unknown;
            if (SandboxStatus.TryParse(value, out status))
            {
                events.post(status);
            }
            else
            {
                GBLog.LogError($"SandboxStatus recieve unsupported type == {value}");
            }
        }
    }

}
#endif

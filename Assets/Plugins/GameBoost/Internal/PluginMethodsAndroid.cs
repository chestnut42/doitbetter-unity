#if UNITY_ANDROID && !UNITY_EDITOR
using UnityEngine;

namespace Plugins.GameBoost
{
    internal class PluginMethodsAndroid : IPluginMethods
    {
        private AndroidJavaClass androidJavaClass;

        public PluginMethodsAndroid()
        {
            androidJavaClass = new AndroidJavaClass("com.doitbetter.sdk.u3d.U3DGameBoostSDK");
        }

        public void InitializeWith(string apiKey)
        {
            androidJavaClass.CallStatic("initialize", apiKey);
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
}
#endif

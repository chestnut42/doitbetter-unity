#if UNITY_ANDROID && !UNITY_EDITOR
using UnityEngine;
using System.Collections;

namespace Plugins.GameBoost
{
    internal class PluginMethodsAndroid : IPluginMethods
    {
        private AndroidJavaClass _ajc;
 
        private AndroidJavaClass plugin() {
            if (_ajc == null)
            {
                _ajc = new AndroidJavaClass("com.doitbetter.sdk.U3DGameBoosterSDK");
            }
            return _ajc;
        }   

        public void InitializeWith(string apiKey)
        {
            if (plugin() != null)
            {
                plugin().CallStatic("initialize", apiKey, false);
            }
        }

        public void SendEvent(string eventName, string jsonString, string deduplicateId)
        {
            if (plugin() != null)
            {
                plugin().CallStatic("sendEvent", eventName, jsonString, deduplicateId);
            }            
        }

        public void SetLoggingEnabled(bool isLoggingEnabled)
        {
            if (plugin() != null)
            {
                plugin().CallStatic("enableLogging", isLoggingEnabled);
            }
        }
    }
}
#endif

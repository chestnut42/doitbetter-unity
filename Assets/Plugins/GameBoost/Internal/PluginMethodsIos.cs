using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Plugins.GameBoost
{
#if UNITY_IPHONE && !UNITY_EDITOR
    internal class PluginMethodsIos : IPluginMethods
    {
        public void InitializeWith(string apiKey)
        {
            _initializeWith(apiKey);
        }

        public void SendEvent(string eventName, string jsonString)
        {
            _sendEvent(eventName, jsonString);
        }

        public void SetLoggingEnabled(bool isLoggingEnabled)
        {
            _enableLogging(isLoggingEnabled);
        }


        [DllImport("__Internal")]
        private static extern void _initializeWith(string apiKey);

        [DllImport("__Internal")]
        private static extern void _sendEvent(string eventName, string jsonString);

        [DllImport("__Internal")]
        private static extern void _enableLogging(bool loggingEnabled);
    }
#endif
}

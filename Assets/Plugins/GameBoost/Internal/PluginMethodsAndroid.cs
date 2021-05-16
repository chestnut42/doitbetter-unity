#if UNITY_ANDROID && !UNITY_EDITOR
using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace Plugins.GameBoost
{
    internal class PluginMethodsAndroid : IPluginMethods
    {
        private AndroidJavaClass _ajc;
 
        private AndroidJavaClass plugin() {
            if (_ajc != null) 
            {
                return _ajc;
            }

            try
            {
                _ajc = new AndroidJavaClass("com.doitbetter.sdk.u3d.U3DGameBoostSDK");
            }
            catch (Exception exception)
            {
                GBLog.LogError($"Exception during Initialize PluginMethodsAndroid: {exception}");
            }
            return _ajc;
        }   

        public void InitializeWith(string apiKey)
        {
            if (plugin() != null)
            {
                try
                {
                    plugin().CallStatic("initialize", apiKey);
                }
                catch (Exception exception)
                {
                    GBLog.LogError($"Exception during CallStatic(initialize): {exception}");
                }
            }
        }

        public void SendEvent(string eventName, string jsonString, string deduplicateId)
        {
            if (plugin() != null)
            {
                try
                {
                    plugin().CallStatic("sendEvent", eventName, jsonString, deduplicateId);
                }
                catch (Exception exception)
                {
                    GBLog.LogError($"Exception during CallStatic(sendEvent): {exception}");
                }
            }            
        }

        public void SetLoggingEnabled(bool isLoggingEnabled)
        {
            if (plugin() != null)
            {
               try
                {
                    plugin().CallStatic("enableLogging", isLoggingEnabled);
                }
                catch (Exception exception)
                {
                    GBLog.LogError($"Exception during CallStatic(enableLogging): {exception}");
                }
            }
        }
    }
}
#endif

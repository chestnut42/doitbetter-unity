// #if UNITY_ANDROID && !UNITY_EDITOR
#if UNITY_ANDROID
using System;
using System.Collections;
using System.Collections.Generic;
using Plugins.GameBoost.BusData;
using UnityEngine;
using Plugins.GameBoost.Core;

namespace Plugins.GameBoost.Android
{
    internal class PluginMethods : IPluginMethods
    {
        private AndroidJavaClass androidJavaClass;
        private GBNativeEvents nativeEvents;
        private GBNativeCall nativeCall;

        public PluginMethods(IGameBoostEventsBus events)
        {
            nativeEvents = new GBNativeEvents(events);
            nativeCall = new GBNativeCall();
            androidJavaClass = new AndroidJavaClass("com.doitbetter.sdk.u3d.U3DGameBoostSDK");
        }

        public void InitializeWith(string apiKey)
        {
            androidJavaClass.CallStatic("initialize", apiKey, nativeEvents);
        }

        public void SendEvent(string eventName, string jsonString, string deduplicateId)
        {
            androidJavaClass.CallStatic("sendEvent", eventName, jsonString, deduplicateId);
        }

        public void SetLoggingEnabled(bool isLoggingEnabled)
        {
            androidJavaClass.CallStatic("enableLogging", isLoggingEnabled);
        }

        public IEnumerator Level(string room_number, Action<LevelData> callMethod)
        {
            var done = false;
            
            rawLevel(room_number, json =>
            {
                callMethod.Invoke(LevelData.ParseOrNull(json)); 
                done = true;                    
            });
            while (!done)
            {
                yield return null;
            }
        }
        
        public IEnumerator Abilities(string reason, string room_number, Action<AbilitiesData> callMethod)
        {
            var done = false;
            
            rawAbilities(room_number, reason, json =>
            {
                callMethod(AbilitiesData.ParseOrNull(json));
                done = true;                    
            });
            while (!done)
            {
                yield return null;
            }
        }        
        
        public void MarkAsDevelopment()
        {
            androidJavaClass.CallStatic("markAsDevelopment");
        }
        
        private void rawLevel(string room_number, Action<string> callback)
        {
            nativeCall.call(
                callID => androidJavaClass.CallStatic("level", room_number, callID, nativeCall),
                callback
            );            
        }

        private void rawAbilities(string room_number, string reason, Action<string> callback)
        {
            nativeCall.call(
                callID => androidJavaClass.CallStatic("abilities", room_number, reason, callID, nativeCall), 
                callback
            );   
        }        
    }

    class GBNativeEvents : AndroidJavaProxy
    {
        private IGameBoostEventsBus eventsBus;

        public GBNativeEvents(IGameBoostEventsBus eventsBus)
            : base("com.doitbetter.sdk.outbus.OutMessageBus")
        {
            this.eventsBus = eventsBus;
        }

        void busMessage(string type, string content)
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
    
    class GBNativeCall: AndroidJavaProxy
    {
        private Dictionary<string, Action<string>> callbacks = new Dictionary<string, Action<string>>();
        public delegate void BusCallbackType(string callID, string content);

        public GBNativeCall()
            : base("com.doitbetter.sdk.outbus.OutNativeCallback")
        {}
        
        public void call(Action<string> nativeCall, Action<string> callback)
        {
            try
            {
                var callID = Guid.NewGuid().ToString();
                lock (callbacks)
                {
                    callbacks[callID] = callback;
                }
                nativeCall(callID);
            }
            catch (Exception ex)
            {
                GBLog.LogError($"cannot call native callNativeWithCallback  ex: {ex.Message}");                
            }
        }        
        
        [AOT.MonoPInvokeCallback(typeof(BusCallbackType))]
        public void nativeCallback(string callID, string content)
        {
            try
            {
                Action<string> callback = null;
                lock (callbacks)
                {
                    if (callbacks.ContainsKey(callID))
                    {
                        callback = callbacks[callID];
                        callbacks.Remove(callID);                        
                    }
                }

                if (callback != null)
                {
                    callback( string.IsNullOrEmpty(content) ? null : content);
                }
                else
                {
                    GBLog.LogError($"nativeCallback no lambda for callID: {callID}");
                }
            }
            catch (Exception ex)
            {
                GBLog.LogError($"nativeCallback calling exception");
            }
        }
    }        
}
#endif

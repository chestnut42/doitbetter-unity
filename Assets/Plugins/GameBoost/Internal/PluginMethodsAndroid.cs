#if UNITY_ANDROID && !UNITY_EDITOR
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

        public void Level(string roomNumber, Action<LevelData> callMethod)
        {
            rawLevel(roomNumber, json =>
            {
                callMethod.Invoke(LevelData.ParseOrNull(json)); 
            });
        }
        
        public void Abilities(string reason, string roomNumber, Action<AbilitiesData> callMethod)
        {
            rawAbilities(roomNumber, reason, json =>
            {
                callMethod(AbilitiesData.ParseOrNull(json));
            });
        }        
        
        public bool IsNeedToProcess(string hashValue)
        {
            bool result = androidJavaClass.CallStatic<bool>("isNeedToProcess", hashValue);
            return result;
        }

        public void AddKeyHash(string keyValue, string hashValue)
        {
            androidJavaClass.CallStatic("add", keyValue, hashValue);
        }        
        
        public void MarkAsDevelopment()
        {
            androidJavaClass.CallStatic("markAsDevelopment");
        }
        
        private void rawLevel(string roomNumber, Action<string> callback)
        {
            nativeCall.call(
                callID => androidJavaClass.CallStatic("level", roomNumber, callID, nativeCall),
                callback
            );            
        }

        private void rawAbilities(string roomNumber, string reason, Action<string> callback)
        {
            nativeCall.call(
                callID => androidJavaClass.CallStatic("abilities", roomNumber, reason, callID, nativeCall), 
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
                GBLog.LogError($"nativeCallback calling exception {ex}");
            }
        }
    }        
}
#endif

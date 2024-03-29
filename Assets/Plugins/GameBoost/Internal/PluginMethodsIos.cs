#if UNITY_IPHONE && !UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Plugins.GameBoost.BusData;
using Plugins.GameBoost.Core;

namespace Plugins.GameBoost.iOs
{
    internal class PluginMethods : IPluginMethods
    {
        private GBNativeEvents nativeEvents;

        public PluginMethods(IGameBoostEventsBus events)
        {
            nativeEvents = new GBNativeEvents(events);
        }

        public void InitializeWith(string apiKey)
        {
            _initializeWith(apiKey, GBNativeEvents.busMessage);
        }

        public void SendEvent(string eventName, string jsonString, string deduplicateId)
        {
            _sendEvent(eventName, jsonString, deduplicateId);
        }

        public void SetLoggingEnabled(bool isLoggingEnabled)
        {
            _enableLogging(isLoggingEnabled);
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
            return _isNeedToProcess(hashValue);
        }

        public void AddKeyHash(string keyValue, string hashValue)
        {
            _addKeyHash(keyValue, hashValue);
        }
        
        public void MarkAsDevelopment()
        {
            // nothing to do -> iOS SDK can detect sandbox
        }

        private void rawLevel(string roomNumber, Action<string> callback)
        {
            GBNativeCallbacks.call(
                callID => _level(roomNumber, callID, GBNativeCallbacks.nativeCallback ),
                callback
            );            
        }

        private void rawAbilities(string roomNumber, string reason, Action<string> callback)
        {
            GBNativeCallbacks.call(
                callID => _abilities(roomNumber, reason, callID, GBNativeCallbacks.nativeCallback), 
                callback
            );   
        }
        
        [DllImport("__Internal")]
        private static extern void _initializeWith(string apiKey, GBNativeEvents.BusMessageType busMethod);

        [DllImport("__Internal")]
        private static extern void _sendEvent(string eventName, string jsonString, string deduplicateId);

        [DllImport("__Internal")]
        private static extern void _level(string roomNumber, string callbackID, GBNativeCallbacks.BusCallbackType callMethod);

        [DllImport("__Internal")]
        private static extern void _abilities(string roomNumber, string reason, string callbackID, GBNativeCallbacks.BusCallbackType callMethod);

        [DllImport("__Internal")]
        private static extern bool _isNeedToProcess(string hashValue);
        
        [DllImport("__Internal")]
        private static extern void _addKeyHash(string keyValue, string hashValue);
        
        [DllImport("__Internal")]
        private static extern void _enableLogging(bool loggingEnabled);
    }

    class GBNativeEvents
    {
        private static IGameBoostEventsBus eventsBus;

        public GBNativeEvents(IGameBoostEventsBus eventsBus)
        {
            GBNativeEvents.eventsBus = eventsBus;
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
    
    class GBNativeCallbacks
    {
        private static Dictionary<string, Action<string>> callbacks = new Dictionary<string, Action<string>>();
        public delegate void BusCallbackType(string callID, string content);

        public static void call(Action<string> nativeCall, Action<string> callback)
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
        public static void nativeCallback(string callID, string content)
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
                    callback(content);
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

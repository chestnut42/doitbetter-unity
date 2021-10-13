using System;
using System.Collections;
using System.Collections.Generic;
using Plugins.GameBoost.Core;

namespace Plugins.GameBoost
{
    internal class PluginEventTracker : IEventTracker, IGameParamsRequest, IKeyHashStorage
    {
        private readonly IJsonSerializer jsonSerializer;
        private readonly IPluginMethods pluginMethods;

        public PluginEventTracker(
            IJsonSerializer jsonSerializer,
            IPluginMethods pluginMethods
        )
        {
            this.jsonSerializer = jsonSerializer;
            this.pluginMethods = pluginMethods;
        }

        public void SendEvent(
            string eventName,
            Dictionary<string, object> eventData,
            string deduplicateId = null
        )
        {
            var jsonString = jsonSerializer.Serialize(eventData, false);
            pluginMethods.SendEvent(eventName, jsonString, deduplicateId);
        }
        
        public AsyncResult<string, BusData.LevelData> LevelRequest(string roomNumber)
        {
            return new AsyncResult<string, BusData.LevelData>(roomNumber, this.Level);
        }            
        
        public AsyncResult<Tuple<string, string>, BusData.AbilitiesData> AbilitiesRequest(string reason, string roomNumber)
        {
            var parameters = new Tuple<string, string>(reason, roomNumber);
            return new AsyncResult<Tuple<string, string>, BusData.AbilitiesData>(parameters, this.Abilities);
        }                    
        
        public void AddKeyHash(string keyValue, string hashValue)
        {
            var adoptedHash = hashValue.Replace('+', '-').Replace('/', '_');
            if (pluginMethods.IsNeedToProcess(adoptedHash))
            {
                pluginMethods.AddKeyHash(keyValue, adoptedHash);
            }
        }
        public void Level(string roomNumber, Action<BusData.LevelData> callMethod)
        {
            pluginMethods.Level(roomNumber, callMethod);
        }

        public void Abilities(Tuple<string, string> parameters, Action<BusData.AbilitiesData> callMethod)
        {
            pluginMethods.Abilities(parameters.Item1, parameters.Item2, callMethod);
        }
    }
}

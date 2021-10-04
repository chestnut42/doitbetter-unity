using System;
using System.Collections;
using System.Collections.Generic;
using Plugins.GameBoost.Core;

namespace Plugins.GameBoost
{
    internal class PluginEventTracker : IEventTracker, IKeyHashStorage
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
        
        public AsyncResult<string, BusData.LevelData> LevelRequest(string room_number)
        {
            return new AsyncResult<string, BusData.LevelData>(room_number, this.Level);
        }            
        
        public AsyncResult<Tuple<string, string>, BusData.AbilitiesData> AbilitiesRequest(string reason, string room_number)
        {
            var parameters = new Tuple<string, string>(reason, room_number);
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
        
        private IEnumerator Level(string room_number, Action<BusData.LevelData> callMethod)
        {
            return pluginMethods.Level(room_number, callMethod);
        }

        private IEnumerator Abilities(Tuple<string, string> parameters, Action<BusData.AbilitiesData> callMethod)
        {
            return pluginMethods.Abilities(parameters.Item1, parameters.Item2, callMethod);
        }

    }
    
    class AsyncResult<Params, Result>
    {
        public delegate IEnumerator AsyncCommand(Params parameters, Action<Result> callMethod);
        
        private Params parameters;
        private Result commandResult;
        private Boolean isDone;
        private AsyncCommand command;
        
        public Result CommandResult => commandResult;
        public Boolean IsDone => isDone;
        
        public AsyncResult(Params parameters, AsyncCommand command)
        {
            this.parameters = parameters;
            this.command = command;
        }
        public IEnumerator Run()
        {
            if (!isDone) {
                yield return null;
                command(parameters, result => commandResult = result);
                isDone = true;                 
            }
        }

    }
}

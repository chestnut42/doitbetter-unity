using System;
using System.Collections;
using System.Collections.Generic;
using Plugins.GameBoost.Core;

namespace Plugins.GameBoost
{
    internal class PluginEventTracker : IEventTracker
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

        public IEnumerator Level(string room_number, Action<BusData.LevelData> callMethod)
        {
            return pluginMethods.Level(room_number, callMethod);
        }

        public IEnumerator Abilities(string reason, string room_number, Action<BusData.AbilitiesData> callMethod)
        {
            return pluginMethods.Abilities(reason, room_number, callMethod);
        }
    }
}

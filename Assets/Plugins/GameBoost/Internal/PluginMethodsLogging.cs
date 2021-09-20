using System;
using System.Collections;
using Plugins.GameBoost.BusData;
using Plugins.GameBoost.Core;

namespace Plugins.GameBoost
{
    internal class PluginMethodsLogging : IPluginMethods
    {
        private readonly IGameBoostEventsBus eventsBus;

        public PluginMethodsLogging(IGameBoostEventsBus eventsBus)
        {
            this.eventsBus = eventsBus;
        }

        public void InitializeWith(string apiKey)
        {
            GBLog.LogDebug($"Initialized with API key: {apiKey}");
        }

        public void SendEvent(string eventName, string jsonString, string deduplicateId)
        {
            GBLog.LogDebug($"Sending event with name <{eventName}>" +
                           $", params: {jsonString}" +
                           $", deduplicate ID: {deduplicateId}");
        }

        public IEnumerator Level(string room_number, Action<LevelData> callMethod)
        {
            GBLog.LogDebug($"RawLevel room_number <{room_number}>");
            yield return null;
        }

        public IEnumerator Abilities(string reason, string room_number, Action<AbilitiesData> callMethod)
        {
            GBLog.LogDebug($"RawAbilities room_number <{room_number}>, reason <{reason}>");
            yield return null;
        }
        
        public void SetLoggingEnabled(bool isLoggingEnabled)
        {
            GBLog.LogDebug($"Setting logging enabled to: {isLoggingEnabled}");
        }

        public void MarkAsDevelopment()
        {
            GBLog.LogDebug($"Mark as development called");
            eventsBus.receiveBusMessage("SANDBOX_STATUS", "sdbx");
        }
    }
}

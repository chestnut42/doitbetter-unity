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

        public void Level(string roomNumber, Action<LevelData> callMethod)
        {
            callMethod(null);
            GBLog.LogDebug($"RawLevel roomNumber <{roomNumber}>");
        }

        public void Abilities(string reason, string roomNumber, Action<AbilitiesData> callMethod)
        {
            callMethod(null);
            GBLog.LogDebug($"RawAbilities roomNumber <{roomNumber}>, reason <{reason}>");
        }

        public bool IsNeedToProcess(string hashValue)
        {
            GBLog.LogDebug($"isNeedToProcess(string hashValue <{hashValue}>)");
            return false;
        }

        public void AddKeyHash(string keyValue, string hashValue)
        {
            GBLog.LogDebug($"addKeyHash(keyValue <{keyValue}>, hashValue <{hashValue}>)");
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

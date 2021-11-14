using System;
using System.Collections;
using System.Collections.Generic;
using Plugins.GameBoost.Core;
using Plugins.GameBoost.JSON;
using UnityEngine;

namespace Plugins.GameBoost
{
    interface IPluginMethods : ISettings
    {
        void InitializeWith(string apiKey);
        void SendEvent(string eventName, string jsonString, string deduplicateId);
        void Level(string roomNumber, Action<BusData.LevelData> callMethod);
        void Abilities(string reason, string roomNumber, Action<BusData.AbilitiesData> callMethod);
        bool IsNeedToProcess(string hashValue);
        void AddKeyHash(string keyValue, string hashValue);
    }
}
namespace Plugins.GameBoost.BusData
{
    [Serializable]
    public class AbilitiesData
    {
        public List<string> abilities {  get; private set; }

        public static AbilitiesData ParseOrNull(string json)
        {
            if (json != null)
            {
                try
                {
                    var rootNode = JSONNode.Parse(json);
                    var result = new AbilitiesData();
                    if (rootNode["abilities"].IsArray)
                    {
                        result.abilities = rootNode["abilities"].AsStringList;
                    }
                    return result;
                }
                catch (Exception e)
                {
                    GBLog.LogError($"AbilitiesData JSONNode.Parse ${e}");
                }
            }
            else
            {
                GBLog.LogError($"Abilities jsonString == null");
            }
            return null;
        }
    }

    public class LevelData
    {
        public string RoomName {  get; private set; }
        public JSONNode DynBalance {  get; private set; }
        
        public static LevelData ParseOrNull(string json)
        {
            if (json != null)
            {
                try
                {
                    var rootNode = JSONNode.Parse(json);
                    var result = new LevelData();
                    if (rootNode["roomName"].IsString)
                    {
                        result.RoomName = rootNode["roomName"];
                    }

                    if (rootNode["dynamicBalance"].IsObject)
                    {
                        result.DynBalance = rootNode["dynamicBalance"].AsObject;
                    }
                    return result;
                }
                catch (Exception e)
                {
                    GBLog.LogError($"LevelData ParseOrNull exceptin ${e}");
                }
            }
            else
            {
                GBLog.LogError($"LevelData jsonString == null");
            }
            return null;
        }
    }    
}
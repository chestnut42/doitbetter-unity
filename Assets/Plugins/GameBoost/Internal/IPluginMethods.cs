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
        IEnumerator Level(string room_number, Action<BusData.LevelData> callMethod);
        IEnumerator Abilities(string reason, string room_number, Action<BusData.AbilitiesData> callMethod);
    }
}
namespace Plugins.GameBoost.BusData
{
    [Serializable]
    class AbilitiesData
    {
        public List<int> abilities {  get; private set; }

        public static AbilitiesData ParseOrNull(string json)
        {
            if (json != null)
            {
                try
                {
                    return JsonUtility.FromJson<AbilitiesData>(json);
                }
                catch (Exception e)
                {
                    GBLog.LogError($"AbilitiesResponse ParseOrNull exceptin ${e}");
                }
            }
            else
            {
                GBLog.LogError($"Abilities jsonString == null");
            }

            return null;
        }
    }
    
    class LevelData
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
                    if (rootNode["room_name"].IsString)
                    {
                        result.RoomName = rootNode["room_name"];
                    }

                    if (rootNode["dyn_balance"].IsObject)
                    {
                        result.DynBalance = rootNode["dyn_balance"].AsObject;
                    }
                    return result;
                }
                catch (Exception e)
                {
                    GBLog.LogError($"AbilitiesResponse ParseOrNull exceptin ${e}");
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
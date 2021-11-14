using System;
using System.Collections;

namespace Plugins.GameBoost
{
    internal interface IGameParamsRequest
    {
        AsyncResult<string, BusData.LevelData> LevelRequest(string roomNumber);
        AsyncResult<Tuple<string, string>, BusData.AbilitiesData> AbilitiesRequest(string reason, string roomNumber);
        void Level(string roomNumber, Action<BusData.LevelData> callMethod);
        void Abilities(Tuple<string, string> parameters, Action<BusData.AbilitiesData> callMethod);
    }
}

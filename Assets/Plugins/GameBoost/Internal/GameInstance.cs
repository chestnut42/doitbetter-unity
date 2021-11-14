using System;
using System.Collections.Generic;

namespace Plugins.GameBoost
{
    internal class GameInstance : IGame
    {
        private readonly IKeyGenerator keyGenerator;
        private readonly IEventTracker eventTracker;
        private readonly IGameParamsRequest gameParamsRequest;
        private readonly string balanceKey;

        public GameInstance(
            IKeyGenerator keyGenerator,
            IEventTracker eventTracker,
            IGameParamsRequest gameParamsRequest,
            string balanceKey
        )
        {
            this.keyGenerator = keyGenerator;
            this.eventTracker = eventTracker;
            this.gameParamsRequest = gameParamsRequest;
            this.balanceKey = balanceKey;
        }


        public IArcheroRoom CreateArcheroRoom(
            string roomNumber,
            string roomName,
            Dictionary<string, object> roomDescription
        )
        {
            var roomKey = keyGenerator.GenerateKey(roomDescription);
            return new ArcheroRoom(
                eventTracker,
                roomNumber,
                roomName,
                roomKey,
                balanceKey);
        }


        public IAsyncResult<BusData.LevelData> LevelRequest(string roomNumber)
        {
            return gameParamsRequest.LevelRequest(roomNumber);
        }

        public IAsyncResult<BusData.AbilitiesData> AbilitiesRequest(string roomNumber, string reason)
        {
            return gameParamsRequest.AbilitiesRequest(reason, roomNumber);
        }

        public void Level(string roomNumber, Action<BusData.LevelData> callMethod)
        {
            gameParamsRequest.Level(roomNumber, callMethod);
        }

        public void Abilities(string roomNumber, string reason, Action<BusData.AbilitiesData> callMethod)
        {
            var parameters = new Tuple<string, string>(reason, roomNumber);
            gameParamsRequest.Abilities(parameters, callMethod);
        }
    }
}

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
                gameParamsRequest,
                roomNumber,
                roomName,
                roomKey,
                balanceKey);
        }
    }
}

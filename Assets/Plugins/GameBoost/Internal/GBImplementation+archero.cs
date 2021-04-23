using System.Collections.Generic;

namespace Plugins.GameBoost
{
    internal partial class GBImplementation
    {
        public IArcheroRoom CreateArcheroRoom(
            string roomNumber,
            string roomName,
            Dictionary<string, object> roomDescription,
            Dictionary<string, object> balance)
        {
            return new ArcheroRoom(
                this,
                roomNumber,
                roomName,
                GenerateKey(roomDescription),
                GenerateKey(balance));
        }


        private string GenerateKey(Dictionary<string, object> dataObject)
        {
            var jsonString = jsonSerializer.Serialize(dataObject, true);
            var hashBytes = hashFunction.Calculate(jsonString);
            return dataEncoder.Encode(hashBytes);
        }


        private class ArcheroRoom : IArcheroRoom
        {
            private static readonly string EventKeyRoomNumber = "r_num";
            private static readonly string EventKeyRoomName = "r_name";
            private static readonly string EventKeyRoomKey = "r_key";
            private static readonly string EventKeyBalanceKey = "bal";
            private static readonly string EventKeyPlayerState = "pl_st";
            private static readonly string EventKeyDynamicBalance = "d_bal";
            private static readonly string EventKeyRoomPlayData = "pl_data";

            private static readonly string EventNameStarted = "room_started";
            private static readonly string EventNameFinished = "room_finished";
            private static readonly string EventNamePlayerDied = "pl_died";
            private static readonly string EventNamePlayerResurrected = "pl_re";
            private static readonly string EventNamePlayerChoseAbility = "pl_abl";

            private readonly GBImplementation gbImplementation;
            private readonly string roomNumber;
            private readonly string roomName;
            private readonly string roomKey;
            private readonly string balanceKey;

            public ArcheroRoom(
                GBImplementation gbImplementation,
                string roomNumber,
                string roomName,
                string roomKey,
                string balanceKey)
            {
                this.gbImplementation = gbImplementation;
                this.roomNumber = roomNumber;
                this.roomName = roomName;
                this.roomKey = roomKey;
                this.balanceKey = balanceKey;
            }

            private Dictionary<string, object> CreateNewEvent()
            {
                var result = new Dictionary<string, object>();
                result[EventKeyRoomNumber] = roomNumber;
                result[EventKeyRoomName] = roomName;
                result[EventKeyRoomKey] = roomKey;
                result[EventKeyBalanceKey] = balanceKey;
                return result;
            }

            public void Started(
                Dictionary<string, object> playerState,
                Dictionary<string, object> dynamicBalance)
            {
                var eventData = CreateNewEvent();
                eventData[EventKeyPlayerState] = playerState;
                eventData[EventKeyDynamicBalance] = dynamicBalance;
                gbImplementation.SendEvent(EventNameStarted, eventData);
            }

            public void Finished(
                Dictionary<string, object> playerState,
                Dictionary<string, object> dynamicBalance,
                Dictionary<string, object> roomPlayData)
            {
                var eventData = CreateNewEvent();
                eventData[EventKeyPlayerState] = playerState;
                eventData[EventKeyDynamicBalance] = dynamicBalance;
                eventData[EventKeyRoomPlayData] = roomPlayData;
                gbImplementation.SendEvent(EventNameFinished, eventData);
            }

            public void PlayerDied(
                Dictionary<string, object> playerState,
                Dictionary<string, object> dynamicBalance,
                Dictionary<string, object> roomPlayData)
            {
                var eventData = CreateNewEvent();
                eventData[EventKeyPlayerState] = playerState;
                eventData[EventKeyDynamicBalance] = dynamicBalance;
                eventData[EventKeyRoomPlayData] = roomPlayData;
                gbImplementation.SendEvent(EventNamePlayerDied, eventData);
            }

            public void PlayerResurrected(
                Dictionary<string, object> playerState,
                Dictionary<string, object> dynamicBalance,
                Dictionary<string, object> roomPlayData)
            {
                var eventData = CreateNewEvent();
                eventData[EventKeyPlayerState] = playerState;
                eventData[EventKeyDynamicBalance] = dynamicBalance;
                eventData[EventKeyRoomPlayData] = roomPlayData;
                gbImplementation.SendEvent(EventNamePlayerResurrected, eventData);
            }

            public void PlayerChoseAbility(
                Dictionary<string, object> playerState,
                Dictionary<string, object> dynamicBalance,
                Dictionary<string, object> roomPlayData)
            {
                var eventData = CreateNewEvent();
                eventData[EventKeyPlayerState] = playerState;
                eventData[EventKeyDynamicBalance] = dynamicBalance;
                eventData[EventKeyRoomPlayData] = roomPlayData;
                gbImplementation.SendEvent(EventNamePlayerChoseAbility, eventData);
            }
        }
    }
}

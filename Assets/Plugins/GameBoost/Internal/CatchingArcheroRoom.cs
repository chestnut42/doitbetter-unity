using System;
using System.Collections;
using System.Collections.Generic;
using Plugins.GameBoost.Core;

namespace Plugins.GameBoost
{
    internal class CatchingArcheroRoom : IArcheroRoom
    {
        private readonly IArcheroRoom wrappedRoom;

        public CatchingArcheroRoom(IArcheroRoom wrappedRoom)
        {
            this.wrappedRoom = wrappedRoom;
        }

        public void Started(
            Dictionary<string, object> playerState,
            Dictionary<string, object> dynamicBalance)
        {
            try
            {
                wrappedRoom.Started(playerState, dynamicBalance);
            }
            catch (Exception exception)
            {
                GBLog.LogError($"Started: {exception}");
            }
        }

        public void EnemiesKilled(
            Dictionary<string, object> playerState,
            Dictionary<string, object> dynamicBalance,
            Dictionary<string, object> roomPlayData)
        {
            try
            {
                wrappedRoom.EnemiesKilled(playerState, dynamicBalance, roomPlayData);
            }
            catch (Exception exception)
            {
                GBLog.LogError($"EnemiesKilled: {exception}");
            }
        }

        public void Finished(
            Dictionary<string, object> playerState,
            Dictionary<string, object> dynamicBalance,
            Dictionary<string, object> roomPlayData)
        {
            try
            {
                wrappedRoom.Finished(playerState, dynamicBalance, roomPlayData);
            }
            catch (Exception exception)
            {
                GBLog.LogError($"Finished: {exception}");
            }
        }

        public void PlayerDied(
            Dictionary<string, object> playerState,
            Dictionary<string, object> dynamicBalance,
            Dictionary<string, object> roomPlayData)
        {
            try
            {
                wrappedRoom.PlayerDied(playerState, dynamicBalance, roomPlayData);
            }
            catch (Exception exception)
            {
                GBLog.LogError($"PlayerDied: {exception}");
            }
        }

        public void PlayerResurrected(
            Dictionary<string, object> playerState,
            Dictionary<string, object> dynamicBalance,
            Dictionary<string, object> roomPlayData)
        {
            try
            {
                wrappedRoom.PlayerResurrected(playerState, dynamicBalance, roomPlayData);
            }
            catch (Exception exception)
            {
                GBLog.LogError($"PlayerResurrected: {exception}");
            }
        }

        public void PlayerChoseAbility(
            Dictionary<string, object> playerState,
            Dictionary<string, object> dynamicBalance,
            Dictionary<string, object> roomPlayData)
        {
            try
            {
                wrappedRoom.PlayerChoseAbility(playerState, dynamicBalance, roomPlayData);
            }
            catch (Exception exception)
            {
                GBLog.LogError($"PlayerChoseAbility: {exception}");
            }
        }

        public AsyncResult<string, BusData.LevelData> LevelRequest()
        {
            try
            {
                return wrappedRoom.LevelRequest();
            }
            catch (Exception exception)
            {
                GBLog.LogError($"LevelRequest: {exception}");
            }
            return null;
        }

        public AsyncResult<Tuple<string, string>, BusData.AbilitiesData> AbilitiesRequest(string reason)
        {
            try
            {
                return wrappedRoom.AbilitiesRequest(reason);
            }
            catch (Exception exception)
            {
                GBLog.LogError($"AbilitiesRequest: {exception}");
            }
            return null;            
        }

        public void Level(Action<BusData.LevelData> callMethod)
        {
            try
            {
                wrappedRoom.Level(callMethod);
            }
            catch (Exception exception)
            {
                GBLog.LogError($"Level with callback: {exception}");
            }
        }

        public void Abilities(string reason, Action<BusData.AbilitiesData> callMethod)
        {
            try
            {
                wrappedRoom.Abilities(reason, callMethod);
            }
            catch (Exception exception)
            {
                GBLog.LogError($"Abilities with callback: {exception}");
            }
        }
    }
}

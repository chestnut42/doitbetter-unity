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
    }
}

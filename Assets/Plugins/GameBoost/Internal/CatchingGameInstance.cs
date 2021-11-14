using System;
using System.Collections.Generic;
using Plugins.GameBoost.Core;

namespace Plugins.GameBoost
{
    internal class CatchingGameInstance : IGame
    {
        private readonly IGame wrappedGame;

        public CatchingGameInstance(IGame wrappedGame)
        {
            this.wrappedGame = wrappedGame;
        }

        public IArcheroRoom CreateArcheroRoom(
            string roomNumber,
            string roomName,
            Dictionary<string, object> roomDescription
        )
        {
            try
            {
                var room = wrappedGame.CreateArcheroRoom(roomNumber, roomName, roomDescription);
                return new CatchingArcheroRoom(room);
            }
            catch (Exception exception)
            {
                GBLog.LogError($"CreateArcheroRoom: {exception}");
                return null;
            }
        }

        public IAsyncResult<BusData.LevelData> LevelRequest(string roomNumber)
        {
            try
            {
                return wrappedGame.LevelRequest(roomNumber);
            }
            catch (Exception exception)
            {
                GBLog.LogError($"LevelRequest: {exception}");
            }
            return null;
        }

        public IAsyncResult<BusData.AbilitiesData> AbilitiesRequest(string roomNumber, string reason)
        {
            try
            {
                return wrappedGame.AbilitiesRequest(roomNumber, reason);
            }
            catch (Exception exception)
            {
                GBLog.LogError($"AbilitiesRequest: {exception}");
            }
            return null;
        }

        public void Level(string roomNumber, Action<BusData.LevelData> callMethod)
        {
            try
            {
                wrappedGame.Level(roomNumber, callMethod);
            }
            catch (Exception exception)
            {
                GBLog.LogError($"Level with callback: {exception}");
            }
        }

        public void Abilities(string roomNumber, string reason, Action<BusData.AbilitiesData> callMethod)
        {
            try
            {
                wrappedGame.Abilities(roomNumber, reason, callMethod);
            }
            catch (Exception exception)
            {
                GBLog.LogError($"Abilities with callback: {exception}");
            }
        }
    }
}

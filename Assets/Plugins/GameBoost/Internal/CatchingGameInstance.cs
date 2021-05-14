using System;
using System.Collections.Generic;

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
    }
}

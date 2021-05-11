using System.Collections.Generic;

namespace Plugins.GameBoost
{
    public interface IGame
    {
        /// <summary>
        /// Creates an object that can send events about this room.
        /// Create such an object every time the player starts to
        /// play a room.
        ///
        /// NOTE: This method does not send any events.
        /// Use it to send start even explicitly.
        ///
        /// NOTE: roomDescription is considered as "static data", i.e. it should contain only
        /// the data that does not change frequently and arbitrary.
        /// This data can only change under manual control of game designer,
        /// e.g. new app version.
        /// </summary>
        /// <param name="roomNumber">The number of the room player sees</param>
        /// <param name="roomName">The unique name of this particular room to be played</param>
        /// <param name="roomDescription">All data used to create this room</param>
        /// <returns>
        /// An archero room instance that can be used to send events.
        /// </returns>
        IArcheroRoom CreateArcheroRoom(
            string roomNumber,
            string roomName,
            Dictionary<string, object> roomDescription
        );
    }
}

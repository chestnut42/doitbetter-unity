using System;
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


        /// <summary>
        /// Requests level data for given room number. After the level data was requested
        /// the <code>callMethod</code> action is invoked. It's guaranteed that the callback
        /// will be invoked no later than 5 seconds after this method call.
        /// </summary>
        /// <param name="roomNumber">The number of the room player sees</param>
        /// <param name="callMethod">The callback receiving level data</param>
        public void Level(string roomNumber, Action<BusData.LevelData> callMethod);

        /// <summary>
        /// Requests abilities data for given room number and a reason. After the abilities data was requested
        /// the <code>callMethod</code> action is invoked. It's guaranteed that the callback
        /// will be invoked no later than 5 seconds after this method call.
        /// </summary>
        /// <param name="roomNumber">The number of the room player sees</param>
        /// <param name="reason">The reason the player is offered ability dialogue</param>
        /// <param name="callMethod">The callback receiving abilities data</param>
        public void Abilities(string roomNumber, string reason, Action<BusData.AbilitiesData> callMethod);

        /// <summary>
        /// Same as Level(string, Action), but suitable for coroutine based method
        /// </summary>
        /// <param name="roomNumber">The number of the room player sees</param>
        /// <returns>The object to monitor progress on</returns>
        public IAsyncResult<BusData.LevelData> LevelRequest(string roomNumber);

        /// <summary>
        /// Same as Abilities(string, string, Action), but suitable for coroutine based method
        /// </summary>
        /// <param name="roomNumber">The number of the room player sees</param>
        /// <param name="reason">The reason the player is offered ability dialogue</param>
        /// <returns>The object to monitor progress on</returns>
        public IAsyncResult<BusData.AbilitiesData> AbilitiesRequest(string roomNumber, string reason);
    }
}

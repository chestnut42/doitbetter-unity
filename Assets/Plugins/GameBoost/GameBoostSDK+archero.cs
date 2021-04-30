using System.Collections.Generic;

namespace Plugins.GameBoost
{
    public partial class GameBoostSDK
    {
        /// <summary>
        /// Creates an object that can send events about this room.
        /// Create such an object every time the player starts to
        /// play a room.
        ///
        /// NOTE: This method does not send any events.
        /// Use it to send start even explicitly.
        ///
        /// NOTE: Fields that are marked as "static data" should contain only
        /// the data that does not change frequently and arbitrary.
        /// This data can only change under manual control of game designer.
        /// e.g. new app version.
        /// </summary>
        /// <param name="roomNumber">The number of the room player sees</param>
        /// <param name="roomName">The unique name of this particular room to be played</param>
        /// <param name="roomDescription">All data used to create this room (static data)</param>
        /// <param name="balance">All game design data, that is not specific to any room (static data)</param>
        /// <returns></returns>
        public static IArcheroRoom CreateArcheroRoom(
            string roomNumber,
            string roomName,
            Dictionary<string, object> roomDescription,
            Dictionary<string, object> balance)
        {
            if (!isInitialized)
            {
                GBLog.LogError("SDK is not initialized. Initialize SDK prior to calling this method");
            }

            return gbImplementation?.CreateArcheroRoom(
                roomNumber,
                roomName,
                roomDescription,
                balance);
        }
    }
}

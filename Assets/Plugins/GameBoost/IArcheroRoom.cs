using System.Collections.Generic;

namespace Plugins.GameBoost
{
    public interface IArcheroRoom
    {
        /// <summary>
        /// Sends an event that the room has started.
        /// Please refer to the manual for detailed description
        /// on each function argument.
        /// </summary>
        /// <param name="playerState">All current player parameters</param>
        /// <param name="dynamicBalance">Dynamic balance values being used to this room launch</param>
        void Started(
            Dictionary<string, object> playerState,
            Dictionary<string, object> dynamicBalance);


        /// <summary>
        /// Sends an event killed enemies
        /// Please refer to the manual for detailed description
        /// on each function argument.
        /// </summary>
        /// <param name="playerState">All current player parameters</param>
        /// <param name="dynamicBalance">Dynamic balance values being used to this room launch</param>
        /// <param name="roomPlayData">Data that shows how good/fast the user finished this room</param>
        void EnemiesKilled(
            Dictionary<string, object> playerState,
            Dictionary<string, object> dynamicBalance,
            Dictionary<string, object> roomPlayData);

        /// <summary>
        /// Sends an event that the room has finished, i.e. player
        /// has won.
        /// Please refer to the manual for detailed description
        /// on each function argument.
        /// </summary>
        /// <param name="playerState">All current player parameters</param>
        /// <param name="dynamicBalance">Dynamic balance values being used to this room launch</param>
        /// <param name="roomPlayData">Data that shows how good/fast the user finished this room</param>
        void Finished(
            Dictionary<string, object> playerState,
            Dictionary<string, object> dynamicBalance,
            Dictionary<string, object> roomPlayData);


        /// <summary>
        /// Sends an event that the player has died.
        /// Please refer to the manual for detailed description
        /// on each function argument.
        /// </summary>
        /// <param name="playerState">All current player parameters</param>
        /// <param name="dynamicBalance">Dynamic balance values being used to this room launch</param>
        /// <param name="roomPlayData">Data that shows how good/fast the user finished this room</param>
        void PlayerDied(
            Dictionary<string, object> playerState,
            Dictionary<string, object> dynamicBalance,
            Dictionary<string, object> roomPlayData);


        /// <summary>
        /// Sends an event that the player has been resurrected.
        /// Please refer to the manual for detailed description
        /// on each function argument.
        /// </summary>
        /// <param name="playerState">All current player parameters</param>
        /// <param name="dynamicBalance">Dynamic balance values being used to this room launch</param>
        /// <param name="roomPlayData">Data that shows how good/fast the user finished this room</param>
        void PlayerResurrected(
            Dictionary<string, object> playerState,
            Dictionary<string, object> dynamicBalance,
            Dictionary<string, object> roomPlayData);


        /// <summary>
        /// Sends an event that the player has chosen an ability
        /// from the given list.
        /// Please refer to the manual for detailed description
        /// on each function argument.
        /// </summary>
        /// <param name="playerState">All current player parameters</param>
        /// <param name="dynamicBalance">Dynamic balance values being used to this room launch</param>
        /// <param name="roomPlayData">Data that shows how good/fast the user finished this room</param>
        void PlayerChoseAbility(
            Dictionary<string, object> playerState,
            Dictionary<string, object> dynamicBalance,
            Dictionary<string, object> roomPlayData);
    }
}

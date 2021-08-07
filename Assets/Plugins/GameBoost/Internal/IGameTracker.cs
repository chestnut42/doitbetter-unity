using System.Collections.Generic;

namespace Plugins.GameBoost
{
    internal interface IGameTracker
    {
        IGame CreateGame(Dictionary<string, object> balance);
    }
}

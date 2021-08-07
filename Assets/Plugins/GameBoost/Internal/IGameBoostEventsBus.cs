using System;
using System.Runtime.Serialization;

namespace Plugins.GameBoost
{
    internal interface IGameBoostEventsBus
    {
        void receiveBusMessage(string type, string content);
    }
    
}

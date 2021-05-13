using System.Collections.Generic;

namespace Plugins.GameBoost
{
    internal interface IEventTracker
    {
        void SendEvent(
            string eventName,
            Dictionary<string, object> eventData,
            string deduplicateId = null
        );
    }
}

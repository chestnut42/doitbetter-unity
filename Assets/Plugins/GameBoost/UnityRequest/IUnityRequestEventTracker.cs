#if UNITY_EDITOR
using System.Collections.Generic;

namespace Plugins.GameBoost.UnityRequest
{
    public interface IUnityRequestEventTracker
    {
        void SendEvent(
            string eventName,
            Dictionary<string, object> eventData,
            string deduplicateId
        );
    }
}
#endif

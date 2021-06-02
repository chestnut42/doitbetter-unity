#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEditor;

namespace Plugins.GameBoost.UnityRequest
{
    internal class UnityRequestEventTracker : IUnityRequestEventTracker
    {
        private readonly IEventStorage eventStorage;

        private readonly string sessionID = Guid.NewGuid().ToString();


        public UnityRequestEventTracker(IEventStorage eventStorage)
        {
            this.eventStorage = eventStorage;
        }

        public void SendEvent(
            string eventName,
            Dictionary<string, object> eventData,
            string deduplicateId
        )
        {
            var eventObject = new Event(
                eventName,
                deduplicateId ?? GUID.Generate().ToString(),
                DateTimeTo8601(DateTime.UtcNow),
                sessionID,
                eventData);
            eventStorage.Put(eventObject);
        }

        private static string DateTimeTo8601(DateTime dateTime)
        {
            return dateTime.ToString("o", System.Globalization.CultureInfo.InvariantCulture);
        }
    }
}
#endif

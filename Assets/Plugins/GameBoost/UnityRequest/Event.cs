#if UNITY_EDITOR
using System.Collections.Generic;

namespace Plugins.GameBoost.UnityRequest
{
    internal class Event
    {
        private readonly string name;
        private readonly string deduplicateId;
        private readonly string timestampString;
        private readonly string sessionID;
        private readonly Dictionary<string, object> data;

        public Event(
            string name,
            string deduplicateId,
            string timestampString,
            string sessionID,
            Dictionary<string, object> data
        )
        {
            this.name = name;
            this.deduplicateId = deduplicateId;
            this.timestampString = timestampString;
            this.sessionID = sessionID;
            this.data = data;
        }

        public string GetID()
        {
            return deduplicateId;
        }

        public Dictionary<string, object> GetJson(string userID)
        {
            var result = new Dictionary<string, object>();
            result["type"] = name;
            result["deduplicateId"] = deduplicateId;
            result["timestamp"] = timestampString;
            result["userId"] = userID;
            result["sessionId"] = sessionID;
            result["params"] = data;
            return result;
        }

        public Dictionary<string, object> GetRequestJson(string userID)
        {
            var eventData = GetJson(userID);
            var eventList = new List<object> {eventData};
            return new Dictionary<string, object> {["eventList"] = eventList};
        }
    }
}
#endif

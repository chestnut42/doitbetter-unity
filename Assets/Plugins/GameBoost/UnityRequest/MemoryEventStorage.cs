#if UNITY_EDITOR
using System.Collections.Generic;

namespace Plugins.GameBoost.UnityRequest
{
    internal class MemoryEventStorage : IEventStorage
    {
        private readonly Dictionary<string, Event> events = new Dictionary<string, Event>();


        public void Put(Event eventObject)
        {
            events[eventObject.GetID()] = eventObject;
        }

        public Event Peek()
        {
            using (var enumerator = events.Values.GetEnumerator())
            {
                return enumerator.MoveNext() ? enumerator.Current : null;
            }
        }

        public void Remove(string eventID)
        {
            events.Remove(eventID);
        }
    }
}
#endif

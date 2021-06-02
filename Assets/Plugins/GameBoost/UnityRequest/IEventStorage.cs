#if UNITY_EDITOR
namespace Plugins.GameBoost.UnityRequest
{
    internal interface IEventStorage
    {
        void Put(Event eventObject);
        Event Peek();
        void Remove(string eventID);
    }
}
#endif

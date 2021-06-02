#if UNITY_EDITOR
using UnityEngine.Networking;

namespace Plugins.GameBoost.UnityRequest
{
    public interface IWebRequestFactory
    {
        UnityWebRequest CreateRequest(string url, object body);
        UnityWebRequest CreateRequestWithJson(string url, string json);
    }
}
#endif

#if UNITY_EDITOR
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace Plugins.GameBoost.UnityRequest
{
    internal class WebRequestFactory : IWebRequestFactory
    {
        private readonly string projectID;
        private readonly string apiKey;

        public WebRequestFactory(
            string projectID,
            string apiKey)
        {
            this.projectID = projectID;
            this.apiKey = apiKey;
        }


        public UnityWebRequest CreateRequest(string url, object body)
        {
            return CreateRequestWithJson(url, JsonUtility.ToJson(body));
        }

        public UnityWebRequest CreateRequestWithJson(string url, string json)
        {
            var result = new UnityWebRequest($"https://api.doitbetter.tech{url}");
            result.method = UnityWebRequest.kHttpVerbPOST;
            result.SetRequestHeader("Content-Type", "application/json");
            result.SetRequestHeader("Accepts", "application/json");
            result.SetRequestHeader("BoostPlatform", "Unity3D");
            result.SetRequestHeader("BoostProjectID", projectID);
            result.SetRequestHeader("BoostAPIKey", apiKey);
            result.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(json));
            result.downloadHandler = new DownloadHandlerBuffer();
            return result;
        }
    }
}
#endif

#if UNITY_EDITOR
using Plugins.GameBoost.Core;
using UnityEngine;

namespace Plugins.GameBoost.UnityRequest
{
    public static class UnityRequestEventTrackerFactory
    {
        public static IUnityRequestEventTracker CreateTracker(
            string projectID,
            string apiKey,
            string appVersion,
            IJsonSerializer jsonSerializer)
        {
            var prefs = new UnityPrefs();
            var eventStorage = new MemoryEventStorage();
            var webRequestFactory = new WebRequestFactory(projectID, apiKey);

            var mainController = new MainController(
                appVersion,
                prefs,
                eventStorage,
                jsonSerializer
            );

            // create actor object
            var gameObject = new GameObject();
            gameObject.name = "GameBoostActor";
            Object.DontDestroyOnLoad(gameObject);

            // add component
            var gameBoostActor = gameObject.AddComponent<GameBoostActor>();
            gameBoostActor.StartCycle(
                mainController,
                webRequestFactory
            );

            return new UnityRequestEventTracker(eventStorage);
        }
    }
}
#endif

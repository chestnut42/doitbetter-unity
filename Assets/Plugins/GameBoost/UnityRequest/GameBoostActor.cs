#if UNITY_EDITOR
using System.Collections;
using Plugins.GameBoost.Core;
using UnityEngine;
using UnityEngine.Networking;

namespace Plugins.GameBoost.UnityRequest
{
    public class GameBoostActor : MonoBehaviour
    {
        internal void StartCycle(
            IMainController mainController,
            IWebRequestFactory webRequestFactory)
        {
            StartCoroutine(MainCycle(mainController, webRequestFactory));
        }

        private IEnumerator MainCycle(
            IMainController mainController,
            IWebRequestFactory webRequestFactory)
        {
            yield return StartCoroutine(SignUp(mainController, webRequestFactory));
            yield return StartCoroutine(SignIn(mainController, webRequestFactory));
            yield return StartCoroutine(SendEvents(mainController, webRequestFactory));
        }

        private IEnumerator SignUp(
            IMainController mainController,
            IWebRequestFactory webRequestFactory)
        {
            // sign up until it's not required
            GBLog.LogDebug("Starting sign up loop");
            for (var signUpRequest = mainController.GetSignUpRequest();
                signUpRequest != null;
                signUpRequest = mainController.GetSignUpRequest())
            {
                using (var webRequest = webRequestFactory.CreateRequest("/1.0/users/new", signUpRequest))
                {
                    yield return webRequest.SendWebRequest();

                    if (webRequest.result == UnityWebRequest.Result.Success)
                    {
                        var response = JsonUtility.FromJson<SignUpResponse>(webRequest.downloadHandler.text);
                        mainController.PutSignUpResponse(response);
                        GBLog.LogDebug($"Signed up with userID: {response.userID}");
                    }
                    else
                    {
                        GBLog.LogError($"Error signing up: {webRequest.error}");
                    }
                }

                yield return new WaitForSecondsRealtime(1.0f);
            }
        }

        private IEnumerator SignIn(
            IMainController mainController,
            IWebRequestFactory webRequestFactory)
        {
            // sign in until success
            GBLog.LogDebug("Starting sign in loop");
            bool signedIn = false;
            while (!signedIn)
            {
                var signInRequest = mainController.GetSignInRequest();
                using (var webRequest = webRequestFactory.CreateRequest("/1.0/users/login", signInRequest))
                {
                    yield return webRequest.SendWebRequest();

                    if (webRequest.result == UnityWebRequest.Result.Success)
                    {
                        signedIn = true;
                        GBLog.LogDebug($"Signed in");
                    }
                    else
                    {
                        GBLog.LogError($"Error signing in: {webRequest.error}");
                    }
                }

                yield return new WaitForSecondsRealtime(1.0f);
            }
        }

        private IEnumerator SendEvents(
            IMainController mainController,
            IWebRequestFactory webRequestFactory)
        {
            // send events infinitely
            GBLog.LogDebug("Starting event loop");
            while (gameObject)
            {
                var eventRequest = mainController.GetEventRequest();
                if (eventRequest != null)
                {
                    GBLog.LogDebug("Sending an event");

                    using (var webRequest = webRequestFactory.CreateRequestWithJson(
                        "/1.0/events", eventRequest.json))
                    {
                        yield return webRequest.SendWebRequest();

                        if (webRequest.result == UnityWebRequest.Result.Success)
                        {
                            mainController.ReportEventSuccess(eventRequest.id);
                            GBLog.LogDebug("Event sent");
                        }
                        else
                        {
                            GBLog.LogError($"Error sending event: {webRequest.error}");
                        }
                    }
                }

                yield return new WaitForSecondsRealtime(1.0f);
            }
        }
    }
}
#endif

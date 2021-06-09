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
            GBLog.LogDebug("Starting sign up loop ...");
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
            GBLog.LogDebug("Starting sign in loop ...");
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
            int x = 0;
            // send events infinitely
            GBLog.LogDebug("Starting event loop ...");
            while (gameObject)
            {
                var eventRequest = mainController.GetEventRequest();
                if (eventRequest != null)
                {
                    x += 1;
                    GBLog.LogDebug("Sending an event ...");
                    var additionalString = x > 5 ? "asd" : "";
                    using (var webRequest = webRequestFactory.CreateRequestWithJson(
                        "/1.0/events", $"{additionalString}{eventRequest.json}{additionalString}"))
                    {
                        yield return webRequest.SendWebRequest();

                        // Remove event for every path.
                        // To reduce spam in logging facility.
                        mainController.ReportEventSuccess(eventRequest.id);

                        if (webRequest.result == UnityWebRequest.Result.Success)
                        {
                            GBLog.LogDebug("Event has been sent");
                        }
                        else
                        {
                            yield return StartCoroutine(HandleEventsError(
                                mainController,
                                webRequestFactory,
                                webRequest,
                                eventRequest
                            ));
                        }
                    }
                }

                yield return new WaitForSecondsRealtime(1.0f);
            }
        }

        private IEnumerator HandleEventsError(
            IMainController mainController,
            IWebRequestFactory webRequestFactory,
            UnityWebRequest failedRequest,
            EventRequest eventRequest)
        {
            if (failedRequest.result != UnityWebRequest.Result.ProtocolError)
            {
                GBLog.LogError($"Unknown error sending event: {failedRequest.error}");
                yield break;
            }

            if (failedRequest.responseCode == 403)
            {
                GBLog.LogError($"Authorization error." +
                               $" Please check your API key and/or application ID");
                yield break;
            }

            if (failedRequest.responseCode == 400)
            {
                GBLog.LogError($"Internal protocol error. Trying to send a report ...");
                using (var reportRequest =
                    webRequestFactory.CreateRequestWithJson("/1.0/client-metrics/bad-request", eventRequest.json))
                {
                    yield return reportRequest.SendWebRequest();

                    if (reportRequest.result == UnityWebRequest.Result.Success)
                    {
                        GBLog.LogError($"Report has been sent. Please contact support");
                    }
                    else
                    {
                        GBLog.LogError($"Failed to send a report: {reportRequest.error}");
                    }
                    yield break;
                }
            }

            GBLog.LogError($"Unknown protocol error: {failedRequest.error}");
        }
    }
}
#endif

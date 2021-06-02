#if UNITY_EDITOR
using System.Collections.Generic;
using Plugins.GameBoost.Core;

namespace Plugins.GameBoost.UnityRequest
{
    internal class MainController : IMainController
    {
        private readonly string appVersion;
        private readonly IPrefs prefs;
        private readonly IEventStorage eventStorage;
        private readonly IJsonSerializer jsonSerializer;

        public MainController(
            string appVersion,
            IPrefs prefs,
            IEventStorage eventStorage,
            IJsonSerializer jsonSerializer
            )
        {
            this.appVersion = appVersion;
            this.prefs = prefs;
            this.eventStorage = eventStorage;
            this.jsonSerializer = jsonSerializer;
        }


        public SignUpRequest GetSignUpRequest()
        {
            if (prefs.UserID == null)
            {
                return new SignUpRequest();
            }
            return null;
        }

        public void PutSignUpResponse(SignUpResponse signUpResponse)
        {
            prefs.UserID = signUpResponse.userID;
        }

        public SignInRequest GetSignInRequest()
        {
            return new SignInRequest()
            {
                userId = prefs.UserID,
                deviceInfo = new DeviceInfo()
                {
                    appVersion = appVersion
                }
            };
        }

        public EventRequest GetEventRequest()
        {
            var userID = prefs.UserID;
            if (userID == null)
            {
                GBLog.LogError("Trying to send an event, but userID is null");
                return null;
            }

            var eventObject = eventStorage.Peek();
            if (eventObject != null)
            {
                var eventData = eventObject.GetRequestJson(userID);
                var eventJson = jsonSerializer.Serialize(eventData, false);
                return new EventRequest()
                {
                    id = eventObject.GetID(),
                    json = eventJson
                };
            }

            return null;
        }

        public void ReportEventSuccess(string id)
        {
            eventStorage.Remove(id);
        }
    }
}
#endif

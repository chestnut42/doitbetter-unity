#if UNITY_EDITOR
namespace Plugins.GameBoost.UnityRequest
{
    internal interface IMainController
    {
        SignUpRequest GetSignUpRequest();
        void PutSignUpResponse(SignUpResponse signUpResponse);

        SignInRequest GetSignInRequest();

        EventRequest GetEventRequest();
        void ReportEventSuccess(string id);
    }

    internal class EventRequest
    {
        public string id;
        public string json;
    }
}
#endif

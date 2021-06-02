#if UNITY_EDITOR
using System;

namespace Plugins.GameBoost.UnityRequest
{
    [Serializable]
    internal class SignUpRequest
    {
    }

    [Serializable]
    internal class SignUpResponse
    {
        public string userID;
    }

    [Serializable]
    internal class SignInRequest
    {
        public string userId;
        public string sandboxStatus = "sdbx";
        public DeviceInfo deviceInfo;
    }

    [Serializable]
    internal class SignInResponse
    {

    }

    [Serializable]
    internal class DeviceInfo
    {
        public string appVersion;
        public string buildVersion = "0";
        public string systemVersion = "0";
        public string deviceModel = "none";
    }
}
#endif

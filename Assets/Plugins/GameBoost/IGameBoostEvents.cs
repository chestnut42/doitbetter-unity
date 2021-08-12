using System;
using System.Runtime.Serialization;

namespace Plugins.GameBoost
{
    [Serializable]
    public enum SandboxStatus
    {
        [EnumMember(Value = "ukwn")] Unknown,

        [EnumMember(Value = "sdbx")] Sandbox,

        [EnumMember(Value = "prod")] Production
    }

    public interface IGameBoostEvents
    {
        public delegate void SandboxStatusHandler(SandboxStatus status);

        /// <summary>
        /// <c>sandboxStatus</c> is fired when the SDK detects if the app
        /// is running in a sandbox environment.
        /// </summary>
        /// <remarks>
        /// Could be fired several times per app launch.
        /// </remarks>
        public event SandboxStatusHandler sandboxStatus;
    }
}

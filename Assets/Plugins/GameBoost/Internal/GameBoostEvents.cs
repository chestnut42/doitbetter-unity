using System.Collections.Generic;
using Plugins.GameBoost.Core;

namespace Plugins.GameBoost
{
    internal class GameBoostEvents: IGameBoostEvents, IGameBoostEventsBus
    {
        private const string SANDBOX_STATUS = "SANDBOX_STATUS";


        #region IGameBoostEvents
        public event IGameBoostEvents.SandboxStatusHandler sandboxStatus;
        #endregion


        #region IGameBoostEventsBus
        public void receiveBusMessage(string type, string content)
        {
            switch (type)
            {
                case SANDBOX_STATUS:
                    ReceiveStatus(content);
                    break;
                default:
                    GBLog.LogError($"busMessage received unsupported type == {type}");
                    break;
            }
        }

        private void ReceiveStatus(string value)
        {
            var mapping = new Dictionary<string, SandboxStatus>
            {
                {"ukwn", SandboxStatus.Unknown},
                {"sdbx", SandboxStatus.Sandbox},
                {"prod", SandboxStatus.Production}
            };

            if (mapping.ContainsKey(value))
            {
                sandboxStatus?.Invoke(mapping[value]);
            }
            else
            {
                GBLog.LogError($"SandboxStatus receive unsupported type == {value}");
            }
        }
        #endregion
    }
}

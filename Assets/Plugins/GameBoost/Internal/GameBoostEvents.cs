using System.Collections.Generic;
using Plugins.GameBoost.Core;

namespace Plugins.GameBoost
{
    internal class GameBoostEvents: IGameBoostEvents, IGameBoostEventsBus
    {
        private const string SANDBOX_STATUS = "SANDBOX_STATUS";
        
        // IGameBoostEvents
        public event IGameBoostEvents.SandboxStatusHandler sandboxStatus;

        internal void Post(SandboxStatus status)
        {
            var handler = sandboxStatus;
            handler?.Invoke(status);
        }
        
        /*
         * TODO: add new events here
         */
        
        // IGameBoostEventsBus
        public void receiveBusMessage(string type, string content) {
            switch (type)
            {
                case SANDBOX_STATUS:
                    ReceiveStatus(content);
                    break;
                default:
                    GBLog.LogError($"busMessage recieve unsupported type == {type}");
                    break;
            }
        }

        private void ReceiveStatus(string value)
        {
            var mapping = new Dictionary<string, SandboxStatus>()
            {
                {"ukwn", SandboxStatus.Unknown},
                {"sdbx", SandboxStatus.Sandbox},
                {"prod", SandboxStatus.Production}
            };

            if (mapping.ContainsKey(value))
            {
                Post(mapping[value]);    
            }
            else
            {
                GBLog.LogError($"SandboxStatus receive unsupported type == {value}");                
            }
        }        
    }
}
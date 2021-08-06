using System;
using System.Runtime.Serialization;
using Plugins.GameBoost.Core;

namespace Plugins.GameBoost
{
    [Serializable]
    public enum SandboxStatus
    {
        [EnumMember(Value = "ukwn")]
        Unknown,
        
        [EnumMember(Value = "sdbx")]
        Sandbox,        
        
        [EnumMember(Value = "prod")]
        Production
    } 
    
    public interface IGameBoostEvents
    {
        public delegate void SandboxStatusHandler(SandboxStatus status);        
        public event SandboxStatusHandler sandboxStatus;
        
        //TODO: add new events here
    }
    
    public interface IGameBoostEventsBus
    {
        void receiveBusMessage(string type, string content);
    }
    
}

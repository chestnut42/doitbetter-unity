using System;
using System.Collections.Generic;

namespace Plugins.GameBoost
{
    internal class CatchingSDKImplementation : ISDKImplementation
    {
        private readonly ISDKImplementation wrappedSDK;

        public CatchingSDKImplementation(ISDKImplementation wrappedSDK)
        {
            this.wrappedSDK = wrappedSDK;
        }

        public void SetLoggingEnabled(bool isLoggingEnabled)
        {
            try
            {
                wrappedSDK.SetLoggingEnabled(isLoggingEnabled);
            }
            catch (Exception exception)
            {
                GBLog.LogError($"SetLoggingEnabled: {exception}");
            }
        }

        public void SendEvent(
            string eventName,
            Dictionary<string, object> eventData,
            string deduplicateId
        )
        {
            try
            {
                wrappedSDK.SendEvent(eventName, eventData, deduplicateId);
            }
            catch (Exception exception)
            {
                GBLog.LogError($"SendEvent: {exception}");
            }
        }

        public IGame CreateGame(Dictionary<string, object> balance)
        {
            try
            {
                var game = wrappedSDK.CreateGame(balance);
                return new CatchingGameInstance(game);
            }
            catch (Exception exception)
            {
                GBLog.LogError($"CreateGame: {exception}");
                return null;
            }
        }

        public void TrackPurchase(
            double amount,
            string currencyCode,
            string transactionId
        )
        {
            try
            {
                wrappedSDK.TrackPurchase(amount, currencyCode, transactionId);
            }
            catch (Exception exception)
            {
                GBLog.LogError($"TrackPurchase: {exception}");
            }
        }


        public void TrackRevenue(
            double amount,
            string currencyCode
        )
        {
            try
            {
                wrappedSDK.TrackRevenue(amount, currencyCode);
            }
            catch (Exception exception)
            {
                GBLog.LogError($"TrackRevenue: {exception}");
            }
        }
    }
}

using System;
using System.Collections.Generic;
using Plugins.GameBoost.Core;

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

        public void MarkAsDevelopment()
        {
            try
            {
                wrappedSDK.MarkAsDevelopment();
            }
            catch (Exception exception)
            {
                GBLog.LogError($"MarkAsDevelopment: {exception}");
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


        public void TrackLocalPurchase(
            double amount,
            string currencyCode,
            string source,
            Dictionary<string, object> info
        )
        {
            try
            {
                wrappedSDK.TrackLocalPurchase(amount, currencyCode, source, info);
            }
            catch (Exception exception)
            {
                GBLog.LogError($"TrackPurchase: {exception}");
            }
        }


        public void TrackRevenue(
            double amount,
            string currencyCode,
            string source
        )
        {
            try
            {
                wrappedSDK.TrackRevenue(amount, currencyCode, source);
            }
            catch (Exception exception)
            {
                GBLog.LogError($"TrackRevenue: {exception}");
            }
        }
    }
}

using System.Collections.Generic;

namespace Plugins.GameBoost
{
    internal class CombinedSDK : ISDKImplementation
    {
        private readonly IRevenueTracker revenueTracker;
        private readonly ILoggingSettings loggingSettings;
        private readonly IGameTracker gameTracker;

        public CombinedSDK(
            IRevenueTracker revenueTracker,
            ILoggingSettings loggingSettings,
            IGameTracker gameTracker
            )
        {
            this.revenueTracker = revenueTracker;
            this.loggingSettings = loggingSettings;
            this.gameTracker = gameTracker;
        }

        public void TrackPurchase(double amount, string currencyCode, string transactionId = null)
        {
            revenueTracker.TrackPurchase(amount, currencyCode, transactionId);
        }

        public void TrackRevenue(double amount, string currencyCode)
        {
            revenueTracker.TrackRevenue(amount, currencyCode);
        }

        public void SetLoggingEnabled(bool isLoggingEnabled)
        {
            loggingSettings.SetLoggingEnabled(isLoggingEnabled);
        }

        public IGame CreateGame(Dictionary<string, object> balance)
        {
            return gameTracker.CreateGame(balance);
        }
    }
}

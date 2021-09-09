using System.Collections.Generic;

namespace Plugins.GameBoost
{
    internal class CombinedSDK : ISDKImplementation
    {
        private readonly IRevenueTracker revenueTracker;
        private readonly ISettings settings;
        private readonly IGameTracker gameTracker;

        public CombinedSDK(
            IRevenueTracker revenueTracker,
            ISettings settings,
            IGameTracker gameTracker
            )
        {
            this.revenueTracker = revenueTracker;
            this.settings = settings;
            this.gameTracker = gameTracker;
        }

        public void TrackPurchase(double amount, string currencyCode, string transactionId = null)
        {
            revenueTracker.TrackPurchase(amount, currencyCode, transactionId);
        }

        public void TrackLocalPurchase(double amount, string currencyCode, string source,
            Dictionary<string, object> info)
        {
            revenueTracker.TrackLocalPurchase(amount, currencyCode, source, info);
        }

        public void TrackRevenue(double amount, string currencyCode, string source)
        {
            revenueTracker.TrackRevenue(amount, currencyCode, source);
        }

        public void SetLoggingEnabled(bool isLoggingEnabled)
        {
            settings.SetLoggingEnabled(isLoggingEnabled);
        }

        public void MarkAsDevelopment()
        {
            settings.MarkAsDevelopment();
        }

        public IGame CreateGame(Dictionary<string, object> balance)
        {
            return gameTracker.CreateGame(balance);
        }
    }
}

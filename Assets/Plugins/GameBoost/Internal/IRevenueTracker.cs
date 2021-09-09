using System.Collections.Generic;

namespace Plugins.GameBoost
{
    internal interface IRevenueTracker
    {
        void TrackPurchase(
            double amount,
            string currencyCode,
            string transactionId = null
        );

        void TrackLocalPurchase(
            double amount,
            string currencyCode,
            string source,
            Dictionary<string, object> info
        );

        void TrackRevenue(
            double amount,
            string currencyCode,
            string source
        );
    }
}

namespace Plugins.GameBoost
{
    internal interface IRevenueTracker
    {
        void TrackPurchase(
            double amount,
            string currencyCode,
            string transactionId = null
        );

        void TrackRevenue(
            double amount,
            string currencyCode
        );
    }
}

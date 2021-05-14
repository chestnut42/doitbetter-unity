using System.Collections.Generic;

namespace Plugins.GameBoost
{
    internal interface ISDKImplementation : IEventTracker
    {
        void SetLoggingEnabled(bool isLoggingEnabled);
        IGame CreateGame(Dictionary<string, object> balance);

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

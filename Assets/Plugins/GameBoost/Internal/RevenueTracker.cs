using System.Collections.Generic;

namespace Plugins.GameBoost
{
    internal class RevenueTracker : IRevenueTracker
    {
        private static readonly string EventKeyAmount = "amnt";
        private static readonly string EventKeyCurrency = "cur";

        private static readonly string EventNamePurchase = "iap";
        private static readonly string EventNameRevenue = "revenue";

        private readonly IEventTracker eventTracker;

        public RevenueTracker(IEventTracker eventTracker)
        {
            this.eventTracker = eventTracker;
        }


        public void TrackPurchase(
            double amount,
            string currencyCode,
            string transactionId
        )
        {
            var param = new Dictionary<string, object>();
            param[EventKeyAmount] = amount;
            param[EventKeyCurrency] = currencyCode;
            eventTracker.SendEvent(EventNamePurchase, param, transactionId);
        }


        public void TrackRevenue(
            double amount,
            string currencyCode
        )
        {
            var param = new Dictionary<string, object>();
            param[EventKeyAmount] = amount;
            param[EventKeyCurrency] = currencyCode;
            eventTracker.SendEvent(EventNameRevenue, param);
        }
    }
}

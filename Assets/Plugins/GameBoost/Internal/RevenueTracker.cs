using System.Collections.Generic;

namespace Plugins.GameBoost
{
    internal class RevenueTracker : IRevenueTracker
    {
        private static readonly string EventKeyAmount = "amnt";
        private static readonly string EventKeyCurrency = "cur";
        private static readonly string EventKeySource = "src";
        private static readonly string EventKeyInfo = "info";

        private static readonly string EventNamePurchase = "iap";
        private static readonly string EventNameRevenue = "revenue";
        private static readonly string EventNameLocalPurchase = "local_pur";

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


        public void TrackLocalPurchase(
            double amount,
            string currencyCode,
            string source,
            Dictionary<string, object> info
        )
        {
            var param = new Dictionary<string, object>();
            param[EventKeyAmount] = amount;
            param[EventKeyCurrency] = currencyCode;
            param[EventKeySource] = source;
            param[EventKeyInfo] = info;
            eventTracker.SendEvent(EventNameLocalPurchase, param);
        }


        public void TrackRevenue(
            double amount,
            string currencyCode,
            string source
        )
        {
            var param = new Dictionary<string, object>();
            param[EventKeyAmount] = amount;
            param[EventKeyCurrency] = currencyCode;
            param[EventKeySource] = source;
            eventTracker.SendEvent(EventNameRevenue, param);
        }
    }
}

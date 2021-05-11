namespace Plugins.GameBoost
{
    public partial class GameBoostSDK
    {
        /// <summary>
        /// Tracks in-app purchase.
        ///
        /// If you have a unique transaction ID, pass it as a parameter.
        /// This parameter is optional though.
        ///
        /// Currency code is the same code as you most likely already getting
        /// somewhere else for some other tracking SDK. For the sake of precision,
        /// here's the list of supported codes: https://www.iso.org/iso-4217-currency-codes.html
        /// </summary>
        /// <param name="amount">Amount of revenue to track</param>
        /// <param name="currencyCode">Currency code</param>
        /// <param name="transactionId">Transaction ID</param>
        public static void TrackPurchase(
            double amount,
            string currencyCode,
            string transactionId = null
        )
        {
            if (!isInitialized)
            {
                GBLog.LogError("SDK is not initialized. Initialize SDK prior to calling TrackPurchase");
            }

            gbImplementation?.TrackPurchase(amount, currencyCode, transactionId);
        }


        /// <summary>
        /// Track any revenue event other than in-app purchase.
        /// e.g. ad click, video ad view, etc.
        /// </summary>
        /// <param name="amount">Amount of revenue to track</param>
        /// <param name="currencyCode">Currency code</param>
        public static void TrackRevenue(
            double amount,
            string currencyCode
        )
        {
            if (!isInitialized)
            {
                GBLog.LogError("SDK is not initialized. Initialize SDK prior to calling TrackRevenue");
            }

            gbImplementation?.TrackRevenue(amount, currencyCode);
        }
    }
}

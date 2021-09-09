using System.Collections.Generic;
using Plugins.GameBoost.Core;

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

            sdkImplementation?.TrackPurchase(amount, currencyCode, transactionId);
        }


        /// <summary>
        /// Tracks in-game purchases in local hard currency.
        ///
        /// Currency code is some arbitrary string denoting a hard currency.
        /// For ex., "diamonds" or "crystals", etc. depending on what you
        /// are used to call your currencies.
        ///
        /// Source of purchase if the rough category of where the purchase was made.
        /// For ex. "resurrection" or "shop", or "room_shop"
        ///
        /// Info is a dictionary where you can put an information about where and what
        /// was exactly purchased. You can put here some IDs or "rarity"
        /// parameters of items. The more information the better.
        /// </summary>
        /// <param name="amount">Amount of revenue to track</param>
        /// <param name="currencyCode">Currency code</param>
        /// <param name="source">Source of purchase</param>
        /// <param name="info">Detailed information about the purchase</param>
        public static void TrackLocalPurchase(
            double amount,
            string currencyCode,
            string source,
            Dictionary<string, object> info
        )
        {
            if (!isInitialized)
            {
                GBLog.LogError("SDK is not initialized. Initialize SDK prior to calling TrackPurchase");
            }

            sdkImplementation?.TrackLocalPurchase(amount, currencyCode, source, info);
        }


        /// <summary>
        /// Track any revenue event other than in-app purchase.
        /// e.g. ad click, video ad view, etc.
        /// </summary>
        /// <param name="amount">Amount of revenue to track</param>
        /// <param name="currencyCode">Currency code</param>
        /// <param name="source">Source of the revenue, e.g. "ads" or "video_ad"</param>
        public static void TrackRevenue(
            double amount,
            string currencyCode,
            string source
        )
        {
            if (!isInitialized)
            {
                GBLog.LogError("SDK is not initialized. Initialize SDK prior to calling TrackRevenue");
            }

            sdkImplementation?.TrackRevenue(amount, currencyCode, source);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace EPLHouse.Cards.DataAccess.Models
{
    public class CardsLevel
    {
        /// <summary>
        /// Primary key
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Card Level
        /// </summary>
        public string cardLevel { get; set; }

        /// <summary>
        /// Fee when issue a new card
        /// </summary>
        public decimal IssueFee { get; set; }

        /// <summary>
        /// Benefits for some cards depends on level
        /// </summary>
        public string CardBenefit { get; set; }

        /// <summary>
        /// Daily cash limit in USD
        /// </summary>
        public decimal DailyCashLimit { get; set; }

        /// <summary>
        /// Daily POS limit in USD
        /// </summary>
        public decimal DailyPosLimit { get; set; }

        /// <summary>
        /// The static number that each card must start with, it differs depending on card level
        /// </summary>
        public int StartOfCardNumber { get; set; }


        public CardsType cardsType { get; set; }
    }
}

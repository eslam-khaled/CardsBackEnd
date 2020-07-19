using DTOs.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace DTOs.BusinessDTOs
{
    public class NewCardDtoBusiness
    {
        /// <summary>
        /// Primary key
        /// </summary>
        public int ClientId { get; set; }

        /// <summary>
        /// Currency for card balance
        /// </summary>
        public decimal Currency { get; set; }

        /// <summary>
        /// Card is major for the account owner or sub for any member from his family
        /// </summary>
        public CardMajorOrSubEnum majorOrSub { get; set; }

        /// <summary>
        /// card lever --> Gold, electron, platinium etc..
        /// </summary>
        public CardLevelEnum cardLevel { get; set; }


        public int cardLevelId { get; set; }

        public string cardNumber { get; set; }

        /// <summary>
        /// The branch where the client can recive the card
        /// </summary>
        public string deliveryBranch { get; set; }

        /// <summary>
        /// comments while establishing card
        /// </summary>
        public string comment { get; set; }

        /// <summary>
        /// the first name of the card holder
        /// </summary>
        public string firstNameOnCard { get; set; }

        /// <summary>
        /// the second name of the card holder - only one char
        /// </summary>
        public string secondNameOnCard { get; set; }

        /// <summary>
        /// the family name of the card holder
        /// </summary>
        public string familyNameOnCard { get; set; }

        /// <summary>
        /// Account number for the client - 13 digits
        /// </summary>
        public string accountNumber { get; set; }

        /// <summary>
        /// True or False for if the client is subscribed in 084 program or not
        /// </summary>
        public bool IsIn084Account { get; set; }

        /// <summary>
        /// Marks true in the database True when creating new card succeed and ready to be printed
        /// </summary>
        public bool ReadyToPrint { get; set; }
    }
}

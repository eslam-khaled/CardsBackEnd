using DTOs.Enums;
using System;
namespace DTOs.BusinessDTOs
{
    public class CardDtoBusiness
    {
        /// <summary>
        /// Primary key
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Card number - 16 digits
        /// </summary>
        public string cardNumber { get; set; }

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
        /// Daily cash which client can have
        /// </summary>
        public decimal DailyCashLimit { get; set; }

        /// <summary>
        /// Daily Point of sale which client is allowed to use
        /// </summary>
        public decimal DailyPosLimit { get; set; }

        /// <summary>
        /// Card buying limit
        /// </summary>
        public decimal buyingLimit { get; set; }

        /// <summary>
        /// Currency for card balance
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// Card status - Valid/Closed/Cancelled
        /// </summary>
        public CardStatusEnum CardStatus { get; set; }

        /// <summary>
        /// Daily limit of credit which can be withdrown
        /// </summary>
        public decimal WithdrawalLimit { get; set; }

        /// <summary>
        /// Account number for card owner
        /// </summary>
        public string accountNumber { get; set; }

        /// <summary>
        /// start date when card was created 
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Card expiry date 3 month from create 
        /// </summary>
        public DateTime CardExpiryDate { get; set; }

        /// <summary>
        /// CCV code on the back of card
        /// </summary>
        public int CCV { get; set; }

        /// <summary>
        /// PIN code - 6 digits
        /// </summary>
        public int PIN { get; set; }

        /// <summary>
        /// Major for the account owner, sub for any of his family
        /// </summary>
        public CardMajorOrSubEnum majorOrSub { get; set; }

        /// <summary>
        /// Master or Visa
        /// </summary>
        public string cardType { get; set; }

        /// <summary>
        /// card lever --> 
        /// </summary>
        public CardLevelEnum cardLevel { get; set; }

        /// <summary>
        /// The branch where the client can recive the card
        /// </summary>
        public string deliveryBranch { get; set; }

        /// <summary>
        /// comments while establishing card
        /// </summary>
        public string comment { get; set; }

        /// <summary>
        /// Marks true in the database True when creating new card succeed and ready to be printed
        /// </summary>
        public bool readyToPrint { get; set; }
    }
}

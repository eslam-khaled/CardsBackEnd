using DTOs.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DTOs.APIDtos
{
    public class CardDtoAPI
    {
        /// <summary>
        /// primary key
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Card number - 16 digits
        /// </summary>
        public string CardNumber { get; set; }

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
        /// Account number - 13 digits
        /// </summary>
        public string AccountNumber { get; set; }

        /// <summary>
        /// Creation date for the card
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Card status - Valid/Closed/Cancelled
        /// </summary>
        public string CardStatus { get; set; }

        /// <summary>
        /// Card expiry date - 3 years scince creation
        /// </summary>
        public DateTime CardExpiryDate { get; set; }

        /// <summary>
        /// CCV code - 3 digits - on the back of the card
        /// </summary>
        public int CCV { get; set; }

        /// <summary>
        /// PIN code - confdential - 6 digits
        /// </summary>
        public int PIN { get; set; }

        /// <summary>
        /// Currency for card balance
        /// </summary>
        public string Currency { get; set; }

        public CardMajorOrSubEnum majorOrSub { get; set; }
        /// <summary>
        /// Master or Visa
        /// </summary>
        public CardTypeEnum cardType { get; set; }

        /// <summary>
        /// card level --> Lookup which stores the static values for levels, and in relationship with CardType table & Cards table.
        /// </summary>
        public CardLevelEnum cardLevel { get; set; }

        /// <summary>
        /// First name on the card
        /// </summary>
        public string FirstNameOnCard { get; set; }

        /// <summary>
        /// Second name on the card - only one letter
        /// </summary>
        public string SecondNameOnCard { get; set; }

        /// <summary>
        /// Family name on the card
        /// </summary>
        public string FamilyNameOnCard { get; set; }

        /// <summary>
        /// Dilivery branch to recieve the card
        /// </summary>
        public string DeliveryBranch { get; set; }

        /// <summary>
        /// Any additional comments
        /// </summary>
        public string Comment { get; set; }
    }
}

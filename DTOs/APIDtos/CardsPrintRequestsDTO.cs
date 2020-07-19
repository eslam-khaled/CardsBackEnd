using System;
using System.Collections.Generic;
using System.Text;

namespace EPLHouse.Cards.DTOs.APIDtos
{
    public class CardsPrintRequestsDTO
    {
        /// <summary>
        /// card number - 16 digits
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
        /// Start date for issuing new card
        /// </summary>
        public string StartDate { get; set; }

        /// <summary>
        /// Card expiry date - 3 years scince created
        /// </summary>
        public string CardExpiryDate { get; set; }

        /// <summary>
        /// The date when request was created
        /// </summary>
        public string RequestDate { get; set; }

        /// <summary>
        /// The generated number for the request which holds a list of cards that want to be printed
        /// </summary>
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string RequestNumber { get; set; }
    }
}

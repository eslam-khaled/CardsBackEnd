using System;
using System.Collections.Generic;
using System.Text;

namespace DTOs.APIDtos
{
    public class NewCardDtoAPI
    {
        /// <summary>
        /// Primary key
        /// </summary>
        public int ClientId { get; set; }

        /// <summary>
        /// Card is major for the account owner or sub for any member from his family
        /// </summary>
        public string majorOrSub { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string cardLevel { get; set; }


        /// <summary>
        /// The branch where the client can recive the card
        /// </summary>
        public string DeliveryBranch { get; set; }

        /// <summary>
        /// comments while establishing card
        /// </summary>
        public string Comment { get; set; }

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
        /// Account number for the client
        /// </summary>
        public string AccountNumber { get; set; }
    }
}

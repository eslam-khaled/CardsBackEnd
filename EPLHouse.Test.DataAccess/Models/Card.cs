using EPLHouse.Cards.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EPLHouse.Test.DataAccess.Models
{
    public class Card
    {
        /// <summary>
        /// Primary key
        /// </summary>
        [Key]
        public int Id { get; set; }

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
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Card expiry date - 3 years scince created
        /// </summary>
        public DateTime CardExpiryDate { get; set; }

        /// <summary>
        /// CCV for card, on the white space on the back of card
        /// </summary>
        public int CVV { get; set; }

        public int CVV_2 { get; set; }

        public int ICVV { get; set; }
        /// <summary>
        /// 6 digits
        /// </summary>
        public int PIN { get; set; }

        /// <summary>
        /// Card is major for the account owner or sub for any member from his family
        /// </summary
        public int MajorOrSub { get; set; }

        /// <summary>
        /// The branch where the client can recive the card
        /// </summary>
        public string DeliveryBranch { get; set; }

        /// <summary>
        /// comments while establishing card
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Marks true in the database True when creating new card succeed and ready to be printed
        /// </summary>
        public bool ReadyToPrint { get; set; }

        /// <summary>
        /// Daily limit of credit which can be withdrown
        /// </summary>
        public decimal WithdrawalLimit { get; set; }

        /// <summary>
        /// Card buying limit
        /// </summary>
        public decimal BuyingLimit { get; set; }

        /// <summary>
        /// Currency for card balance
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// Card status - Valid/Closed/Cancelled
        /// </summary>
        public int CardStatus { get; set; }

        /// <summary>
        /// Flag stores if the card is printed or not
        /// </summary>
        public bool IsPrinted { get; set; }

        /// <summary>
        /// Stores if requested to print as true and false if not
        /// </summary>
        public bool IsRequestedToPrint { get; set; }

        /// <summary>
        /// Foreign Key for Account table
        /// </summary>
        public int Account_Id { get; set; }

        /// <summary>
        /// Obj from Account, one account can has many cards
        /// </summary>
        [ForeignKey("Account_Id")]
        public virtual Account Accounts { get; set; }

        /// <summary>
        /// Foreign Key for Client table
        /// </summary>
        public int? Client_Id { get; set; }

        /// <summary>
        /// Obj from Client, one client can has many cards
        /// </summary>
        [ForeignKey("Client_Id")]
        public virtual Client Client { get; set; }

        /// <summary>
        /// Foreign Key for cards level table
        /// </summary>
        public int CardsLevel_Id { get; set; }

        /// <summary>
        /// Relationship for Cards levels
        /// </summary>
        [ForeignKey("CardsLevel_Id")]
        public virtual CardsLevel cardsLevel { get; set; }

        /// <summary>
        /// Foreign key for table Print Requests
        /// </summary>
        
        public int? printRequestsID { get; set; }

        [ForeignKey("printRequestsID")]
        /// <summary>
        /// Relationship for Cards levels
        /// </summary>
        public virtual PrintRequests printRequests { get; set; }
    }
}
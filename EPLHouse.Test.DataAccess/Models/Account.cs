using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EPLHouse.Test.DataAccess.Models
{
    public class Account
    {
        /// <summary>
        /// Id is the primary key for table account
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Account number - 13 digits
        /// </summary>
        public string AccountNumber { get; set; }

        /// <summary>
        /// Account balance for client
        /// </summary>
        public decimal Balance { get; set; }

        /// <summary>
        /// True or False for if client account is subscreibed to 084 program
        /// </summary>
        public bool IsIn084Account { get; set; }

        /// <summary>
        /// list of cards in each account - One to many relationship
        /// </summary>
        public List<Card> cards { get; set; }

        public int? Client_Id { get; set; }

        [ForeignKey("Client_Id")]
        public virtual Client Client { get; set; }
    }
}

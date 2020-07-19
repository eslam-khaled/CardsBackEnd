using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EPLHouse.Test.DataAccess.Models
{
    public class Client
    {
        /// <summary>
        /// Primary key
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Client first name
        /// </summary>
        public string firstName { get; set; }

        /// <summary>
        /// Client second name - only one letter
        /// </summary>
        public string SecondName { get; set; }

        /// <summary>
        /// Client family name
        /// </summary>
        public string FamilyName { get; set; }

        /// <summary>
        /// client identity number or passport
        /// </summary>
        public string PersonIdentity { get; set; }

        /// <summary>
        /// Type of persone identity, ID card or passport
        /// </summary>
        public string personIdentityType { get; set; }

        /// <summary>
        /// client birthdate
        /// </summary>
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// client age
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// Client address
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// List of cards, one client can has many cards - one to many relationship
        /// </summary>
        public List<Card> card { get; set; }

        /// <summary>
        /// List of accounts, one client can has many accounts - one to many relationship
        /// </summary>
        public List<Account> account { get; set; }
    }
}

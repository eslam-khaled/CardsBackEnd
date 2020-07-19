using System;
using System.Collections.Generic;
using System.Text;

namespace DTOs.BusinessDTOs
{
    public class ClientDtoBusiness
    {
        /// <summary>
        /// primary key
        /// </summary>
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
        /// account number - 16 digits
        /// </summary>
        public string accountNumber { get; set; }

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
        /// True or False for if the client is subscribed in 084 program or not
        /// </summary>
        public bool IsIn084Account { get; set; }
    }
}

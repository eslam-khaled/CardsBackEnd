using System;
using System.Collections.Generic;
using System.Text;

namespace DTOs.APIDtos
{
    public class ClientDtoAPI
    {
        /// <summary>
        /// Primary key
        /// </summary>
        public int ClientId { get; set; }

        /// <summary>
        /// Client first name
        /// </summary>
        public string firstName { get; set; }

        /// <summary>
        /// Client last name
        /// </summary>
        public string lastName { get; set; }

        /// <summary>
        /// Account number for the client - 13 digits
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

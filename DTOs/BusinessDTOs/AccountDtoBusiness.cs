using System;
using System.Collections.Generic;
using System.Text;

namespace DTOs.BusinessDTOs
{
    public class AccountDtoBusiness
    {
        /// <summary>
        /// Primary key
        /// </summary>
        public int AccountId { get; set; }

        /// <summary>
        /// Account number - 13 digitss
        /// </summary>
        public int AccountNumber { get; set; }

        /// <summary>
        /// True if subscribed in 084 program, false if not
        /// </summary>
        public bool IsIn084Account { get; set; }
    }
}

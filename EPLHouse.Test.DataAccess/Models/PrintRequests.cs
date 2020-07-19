using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using EPLHouse.Test.DataAccess.Models;

namespace EPLHouse.Cards.DataAccess.Models
{
    public class PrintRequests
    {
        /// <summary>
        /// ID, the primary key
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The date when request was created
        /// </summary>
        public DateTime RequestDate { get; set; }

        /// <summary>
        /// The generated number for the request which holds a list of cards that want to be printed
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string RequestNumber { get; set; }

        public virtual List<Card> Cards { get; set; }
    }
}

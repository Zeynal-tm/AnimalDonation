using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalDonation.Models
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public string Description { get; set; }

        [NotMapped]
        public decimal Price
        {
            get
            {
                return (decimal)Amount / 100;
            }
        }

        [NotMapped]
        public string Currency => $"{Price} TMT";

    }
}

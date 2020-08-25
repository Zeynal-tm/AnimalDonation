using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnimalDonation.Models
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public string Description { get; set; }
    }
}

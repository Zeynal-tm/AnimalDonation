using System;
using System.Collections.Generic;
using System.Text;

namespace AnimalDonation.Core.Classes
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public string Description { get; set; }
        public string PaymentSystemOrderId { get; set; }
        public bool Paid { get; set; }
    }
}

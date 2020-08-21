using System;
using System.Collections.Generic;
using System.Text;

namespace AnimalDonation.Core.Classes
{
    public class OrderStatusResponse
    {
        public int Id { get; set; }
        public int errorCode { get; set; }
        public string errorMessage { get; set; }
        public int orderStatus { get; set; }
        public string orderNumber { get; set; }
        public int amount { get; set; }
    }
}

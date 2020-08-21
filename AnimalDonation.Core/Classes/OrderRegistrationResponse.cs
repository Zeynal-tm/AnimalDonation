using System;
using System.Collections.Generic;
using System.Text;

namespace AnimalDonation.Core.Classes
{
    public class OrderRegistrationResponse
    {
        public int Id { get; set; }
        public string orderId { get; set; }
        public string formUrl { get; set; }
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
    }
}

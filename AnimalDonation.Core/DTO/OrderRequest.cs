using System;
using System.Collections.Generic;
using System.Text;

namespace AnimalDonation.Core.Classes
{
    public class OrderRequest
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string orderId { get; set; }
    }
}

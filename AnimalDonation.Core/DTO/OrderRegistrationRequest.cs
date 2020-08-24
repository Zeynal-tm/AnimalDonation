using System;
using System.Collections.Generic;
using System.Text;

namespace AnimalDonation.Core.Classes
{
    public class OrderRegistrationRequest
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string OrderNumber { get; set; }
        public int Amount { get; set; }
        public string ReturnUrl { get; set; }
        public string FailUrl { get; set; }
        public string Description { get; set; }
    }
}

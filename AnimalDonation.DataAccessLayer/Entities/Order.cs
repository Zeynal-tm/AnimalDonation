using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnimalDonation.DataAccessLayer.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public string Description { get; set; }
        public string PaymentSystemOrderId { get; set; }
        public bool Paid { get; set; }
    }
}

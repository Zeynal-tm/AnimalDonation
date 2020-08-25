using System.ComponentModel.DataAnnotations;

namespace AnimalDonation.Core.Classes
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public string Description { get; set; }
        public string PaymentSystemOrderId { get; set; }

        public decimal Price
        {
            get
            {
                return (decimal)Amount / 100;
            }
        }

        public string Currency => $"{Price} TMT";
    }
}

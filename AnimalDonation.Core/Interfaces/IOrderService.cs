using AnimalDonation.Core.Classes;
using AnimalDonation.DataAccessLayer.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalDonation.Core.Interfaces
{
    public interface IOrderService
    {
        Task<OrderRegistrationResponse> CreateOrder(int amount, string description);

        Task<OrderStatusResponse> RequestOrderStatus(string orderId);

        IEnumerable<OrderDTO> GetPaidDonationers();
    }
}

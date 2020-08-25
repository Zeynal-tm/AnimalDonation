using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AnimalDonation.DataAccessLayer.Interfaces
{
    public interface IOrderRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAllPaidOrders();
        T Get(string id);
        void Create(T item);
        void Update(T item);
    }
}

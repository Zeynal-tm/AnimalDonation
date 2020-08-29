using AnimalDonation.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AnimalDonation.DataAccessLayer.Interfaces
{
    public interface IUnitOfWork 
    {
        IOrderRepository<Order> Orders { get; }
        void Save();
        Task SaveAsync();
    }
}

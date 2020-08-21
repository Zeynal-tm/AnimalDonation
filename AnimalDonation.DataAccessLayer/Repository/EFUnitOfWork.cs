using AnimalDonation.DataAccessLayer.Context;
using AnimalDonation.DataAccessLayer.Entities;
using AnimalDonation.DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AnimalDonation.DataAccessLayer.Repository
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private DatabaseContext context;
        private OrderRepository orderRepository;

        public EFUnitOfWork(DatabaseContext context)
        {
            this.context = context;
        }

        public IOrderRepository<Order> Orders
        {
            get
            {
                if (orderRepository == null)
                    orderRepository = new OrderRepository(context);
                return orderRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}

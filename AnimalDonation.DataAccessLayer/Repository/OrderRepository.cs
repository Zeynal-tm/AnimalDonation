using AnimalDonation.DataAccessLayer.Context;
using AnimalDonation.DataAccessLayer.Entities;
using AnimalDonation.DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalDonation.DataAccessLayer.Repository
{
    public class OrderRepository : IOrderRepository<Order>
    {
        private DatabaseContext context;

        public OrderRepository(DatabaseContext context)
        {
            this.context = context;
        }

        public void Create(Order item)
        {
            context.Orders.Add(item);
        }

        public Order Get(string id)
        {
            return context.Orders.First(item => item.PaymentSystemOrderId == id);
        }

        public IEnumerable<Order> GetAll()
        {
            return context.Orders;
        }

        public void Update(Order item)
        {
            context.Entry(item).State = EntityState.Modified;
        }
    }
}

using AnimalDonation.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace AnimalDonation.DataAccessLayer.Context
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }
    }
}

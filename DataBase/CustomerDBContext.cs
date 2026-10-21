using Microsoft.EntityFrameworkCore;
using MultipleDBContext.Api.Customers;

namespace MultipleDBContext.Api.DataBase
{
    public class CustomerDBContext
        : DbContext
    {


        public CustomerDBContext(DbContextOptions<CustomerDBContext> options)
            : base(options) { 
        
          
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("MyDemoCustomers");
            modelBuilder.ApplyConfiguration(new CustomerConfiguration());
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Customer> Customers { get; set; }

    }
}

using Microsoft.EntityFrameworkCore;
using MultipleDBContext.Api.Orders;

namespace MultipleDBContext.Api.DataBase
{
    public class OrderDbContext
        : DbContext
    {

        public OrderDbContext(DbContextOptions<OrderDbContext>? options)
            : base(options)
        {


        }

        public DbSet<Order> Orders { get; set; }

        public DbSet<LineItem> LineItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.HasDefaultSchema("MyDemoOrders");

            modelBuilder.Entity<Order>().HasMany(d => d.LineItems).WithOne().HasForeignKey(li => li.Id);

            base.OnModelCreating(modelBuilder);
        }
    }
}

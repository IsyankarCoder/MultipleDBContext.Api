using Microsoft.EntityFrameworkCore;
using MultipleDBContext.Api.Products;


namespace MultipleDBContext.Api.DataBase
{
    public class ProductsDbContext : DbContext
    {

        public ProductsDbContext(DbContextOptions<ProductsDbContext> options)
            : base(options)
        {

        }


        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("MyDemoProducts");
            //modelBuilder.Entity<Product>().HasData(SeedProducts);
            base.OnModelCreating(modelBuilder);
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            base.ConfigureConventions(configurationBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            base.OnConfiguring(optionsBuilder);
        }

       /* private static readonly Product[] SeedProducts =
   {
    new() { Id = Guid.Parse("11111111-1111-1111-1111-111111111111"), Name = "Product #1", Price = 100 },
    new() { Id = Guid.Parse("22222222-2222-2222-2222-222222222222"), Name = "Product #2", Price = 1001 },
    new() { Id = Guid.Parse("33333333-3333-3333-3333-333333333333"), Name = "Product #3", Price = 1002 },
    new() { Id = Guid.Parse("44444444-4444-4444-4444-444444444444"), Name = "Product #4", Price = 1003 },
    new() { Id = Guid.Parse("55555555-5555-5555-5555-555555555555"), Name = "Product #5", Price = 1004 },
    new() { Id = Guid.Parse("66666666-6666-6666-6666-666666666666"), Name = "Product #6", Price = 1005 },
    new() { Id = Guid.Parse("77777777-7777-7777-7777-777777777777"), Name = "Product #7", Price = 1006 },
    new() { Id = Guid.Parse("88888888-8888-8888-8888-888888888888"), Name = "Product #8", Price = 1007 },
    new() { Id = Guid.Parse("99999999-9999-9999-9999-999999999999"), Name = "Product #9", Price = 1008 },
    new() { Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), Name = "Product #10", Price = 1009 }
};*/

    }
}

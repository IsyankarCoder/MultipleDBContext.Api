using Microsoft.EntityFrameworkCore;
using MultipleDBContext.Api.Shipments;

namespace MultipleDBContext.Api.DataBase
{
    public class ShipmentDbContext
        : DbContext
    {

        public ShipmentDbContext(DbContextOptions<ShipmentDbContext> options)
            : base(options)
        {

        }

        public DbSet<Shipments.Shipment> Shipments { get; set; }

        /*
         Complex types came in EF Core 8, and they are made for value objects,
         which are objects with no identity of their own (like an address or a money amount).
         Starting with EF Core 10, you can map a complex type to a JSON column too.
          
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>(builder =>
            {
                builder.ComplexProperty(o => o.Shipping, shipping => shipping.ToJson());
                builder.ComplexCollection(o => o.Notes, notes => notes.ToJson());
            });
        }*/


        /*
      --------------------------------------------------------------------------------------------
         Aspect	                       Owned entity types	            Complex types
      Identity	                    Hidden key and identity	     No identity, compared by value
      Sharing one instance	            Not allowed	                      Allowed
      Assignment	                Reference semantics	         Value semantics (values are copied)
      .NET type	                    Reference types only	     Reference or value types
      Can contain navigations	            Yes	                            No
      ExecuteUpdate support	            Not supported	                 Supported
      ---------------------------------------------------------------------------------------------   
         */
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Shipment>(builder =>
            {
                builder.OwnsOne(d => d.Shipping, shipping => shipping.ToJson());
                builder.OwnsMany(g => g.Notes, notes => notes.ToJson());
            });

            modelBuilder.Entity<Shipment>()
                       .HasIndex(o => new { o.ShipmentNumber, o.CreatedAt })
        .HasDatabaseName("IX_Shipment_Status_CreatedAtUtc");


            base.OnModelCreating(modelBuilder);
        }
    }
}

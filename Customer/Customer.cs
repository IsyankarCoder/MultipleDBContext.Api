using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MultipleDBContext.Api.Customers
{
    public   class Customer
    {
        public Guid Id { get; set; }


        /* public string Street { get; set; }
         public string City { get; set; }
         public string PostalCode { get; set; }
         public string Country { get; set; }*/

        public string Name { get; set; } = null!;
        public Address Address { get; set; } = null!;

    }


    public sealed class Address
    {
        public string Street { get; set; } = null!;
        public string City { get; set; } = null!;
        public string PostalCode { get; set; } = null!;
        public string Country { get; set; } = null!;
        public Coordinates Coordinates { get; set; } = null!;
    }

    public sealed class Coordinates
    {
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
    }


    public sealed class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name).HasMaxLength(150).IsRequired();

            builder.OwnsOne(c => c.Address, address =>
            {
                address.Property(a => a.Street).HasMaxLength(50).IsRequired().HasColumnName("Street");
                address.Property(a => a.City).HasMaxLength(100).IsRequired().HasColumnName("City");
                address.Property(a => a.PostalCode).HasMaxLength(20).IsRequired().HasColumnName("PostalCode");
                address.Property(a => a.Country).HasMaxLength(100).IsRequired().HasColumnName("Country");

                address.OwnsOne(a => a.Coordinates, coordinates =>
                {
                    coordinates.Property(c => c.Latitude).HasColumnName("Latitude");
                    coordinates.Property(c => c.Longitude).HasColumnName("Longitude");
                });
                address.Navigation(a => a.Coordinates).IsRequired();
            });
            builder.Navigation(c => c.Address).IsRequired();
        }
    }
}

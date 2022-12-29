using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SupermarketApp.Models;

namespace SupermarketApp.Data.Context.SupermarketConfig
{
    public class ManufacturerConfiguration : IEntityTypeConfiguration<Manufacturer>
    {
        public void Configure(EntityTypeBuilder<Manufacturer> builder)
        {
            builder.ToTable(nameof(Manufacturer)).HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName("ManufacturerId").ValueGeneratedOnAdd();
            builder.Property(e => e.Name).HasMaxLength(100).IsRequired();
            builder.Property(e => e.Image);
        }
    }
}

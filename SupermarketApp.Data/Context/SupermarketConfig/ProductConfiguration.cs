using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SupermarketApp.Data.Entities;

namespace SupermarketApp.Data.Context.SupermarketConfig
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable(nameof(Product)).HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName("ProductId").ValueGeneratedOnAdd();
            builder.Property(e => e.Name).HasMaxLength(100).IsRequired();
            builder.Property(e => e.Description).HasMaxLength(1000).IsRequired();
            builder.Property(e => e.Price).HasColumnType("money").IsRequired();
            builder.Property(e => e.Amount).IsRequired();
            builder.Property(e => e.CreationDate).IsRequired();
            builder.Property(e => e.ExpirationDate).IsRequired();
            builder.Property(e => e.Image);
            builder.Property(e => e.DepartmentId);
            builder.Property(e => e.ManufacturerId).IsRequired();

            builder.HasOne(e => e.Department)
                .WithMany(e => e.Products)
                .HasForeignKey(e => e.DepartmentId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(e => e.Manufacturer)
                .WithMany(e => e.Products)
                .HasForeignKey(e => e.ManufacturerId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

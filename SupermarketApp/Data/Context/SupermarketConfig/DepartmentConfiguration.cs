using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SupermarketApp.Models;

namespace SupermarketApp.Data.Context.SupermarketConfig
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.ToTable(nameof(Department)).HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName("DepartmentId").ValueGeneratedOnAdd();
            builder.Property(e => e.Name).HasMaxLength(100).IsRequired();
            builder.Property(e => e.Description).HasMaxLength(1000).IsRequired();
            builder.Property(e => e.Image);

            builder.HasMany(e => e.Manufacturers)
                .WithMany(e => e.Departments);
        }
    }
}

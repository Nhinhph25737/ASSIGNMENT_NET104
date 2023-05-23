using _3_Asp.Net_MVC.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace _3_Asp.Net_MVC.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product");
            builder.HasKey(c => c.ID);
            builder.Property(c=> c.ProductName).IsRequired();
            builder.Property(p => p.TypeName).IsUnicode(true).IsRequired();
            builder.Property(p => p.Supplier).IsUnicode(true).IsRequired();
            builder.Property(p => p.Description).HasColumnType("nvarchar(1000)");
            builder.Property(c=> c.Price).IsRequired();
            builder.Property(c=> c.AvailableQuantity).HasColumnType("int").IsRequired();
            builder.Property(c=> c.Status).HasColumnType("int").IsRequired();
            // Khóa ngoại
            builder.HasOne(p => p.Category).WithMany(p => p.Products).HasForeignKey(p => p.TypeName);

        }
    }
}

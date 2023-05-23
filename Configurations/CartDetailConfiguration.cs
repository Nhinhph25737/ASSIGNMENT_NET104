using _3_Asp.Net_MVC.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace _3_Asp.Net_MVC.Configurations
{
    public class CartDetailConfiguration : IEntityTypeConfiguration<CartDetail>
    {
        public void Configure(EntityTypeBuilder<CartDetail> builder)
        {
            builder.ToTable("CartDetail"); // Đặt tên cho bảng
            builder.HasKey(c=> c.ID);
            builder.Property(c=> c.UserID).IsRequired();
            builder.Property(c=> c.Quantity).HasColumnType("int").IsRequired();
            builder.HasOne(p => p.Product).WithMany().
                            HasForeignKey(p => p.ProductID);
            //builder.HasOne(p => p.User).WithMany().
            //                HasForeignKey(p => p.UserID);

        }
    }
}

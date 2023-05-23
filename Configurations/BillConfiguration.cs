using _3_Asp.Net_MVC.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace _3_Asp.Net_MVC.Configurations
{
    public class BillConfiguration : IEntityTypeConfiguration<Bill>
    {
        public void Configure(EntityTypeBuilder<Bill> builder)
        {
            builder.ToTable("Bill"); // Đặt tên cho bảng
            builder.HasKey(c => c.ID); // Set khóa chính
            // Set thuộc tính cho mỗi cột, mỗi Property là 1 cột
            builder.Property(c => c.UserID).
                HasColumnName("UserID").IsRequired();
            builder.Property(c => c.Status).HasColumnType("int")
                .IsRequired();
            builder.Property(c => c.PhoneNumber).HasColumnType("varchar(256)").IsRequired();
            builder.Property(c => c.Recipient).HasColumnType("nvarchar(256)").IsRequired();
            builder.Property(c => c.Address). HasColumnType("nvarchar(256)").IsRequired();
            builder.Property(c => c.CreateDate).HasColumnType("Datetime");
            // Khóa ngoại tính tiếp
            builder.HasOne(p => p.User).WithMany(p => p.Bills).HasForeignKey(p => p.UserID);
        }
    }
}

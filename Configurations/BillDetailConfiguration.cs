using _3_Asp.Net_MVC.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace _3_Asp.Net_MVC.Configurations
{
    public class BillDetailConfiguration : IEntityTypeConfiguration<BillDetail>
    {
        public void Configure(EntityTypeBuilder<BillDetail> builder)
        {
            builder.ToTable("BillDetail");
            builder.HasKey(c=> c.ID);
            builder.Property(c=> c.Quantity).HasColumnType("int").IsRequired();
            builder.Property(c => c.Price).IsRequired();
            //Khóa ngoại
            builder.HasOne(p => p.Bill).WithMany(q => q.BillDetails).
               HasForeignKey(p => p.BillID);
            builder.HasOne(p => p.Product).WithMany(q => q.BillDetails).
                HasForeignKey(p => p.ProductID);
        }
    }
}

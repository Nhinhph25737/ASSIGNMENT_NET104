using _3_Asp.Net_MVC.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace _3_Asp.Net_MVC.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>

    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.ID);
            builder.Property(p => p.UserName).IsRequired().
                HasColumnType("varchar(256)");
            builder.Property(p => p.UserName).IsRequired().
                HasColumnType("varchar(256)");
           
        }
    }
}

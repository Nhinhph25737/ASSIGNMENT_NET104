using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace _3_Asp.Net_MVC.Models
{
    public class ShoppingDBContext :DbContext
    {
        public ShoppingDBContext()
        {
            
        }
        public ShoppingDBContext(DbContextOptions options):base(options)
        {
        }
        public DbSet<Bill> Bills { get; set; } 
        public DbSet<User> Users { get; set; }
        public DbSet<BillDetail> BillDetails { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<CartDetail> CartDetails { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=LAPTOP-G189FU38\SQLEXPRESS;Initial Catalog=AssignmentNet104;Integrated Security=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

    }
}

using System.ComponentModel.DataAnnotations;

namespace _3_Asp.Net_MVC.Models
{
    public class Product
    {
        public Guid ID { get; set; }
        [MinLength(5)]
        public string ProductName { get; set; }
        public string? Image { get; set; }
        public long Price { get; set; }
        public int AvailableQuantity { get; set; }
        public Guid? TypeName { get; set; }
        public string Supplier { get; set; }
        public string Size { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? EditDate { get; set; }
        public List<BillDetail>? BillDetails { get; set; }
        public Category Category { get; set; }
    }
}

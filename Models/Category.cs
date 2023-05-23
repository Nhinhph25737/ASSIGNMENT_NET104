namespace _3_Asp.Net_MVC.Models
{
    public class Category
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public int? Status { get; set; }
        public virtual List<Product>? Products { get; set; }

    }
}

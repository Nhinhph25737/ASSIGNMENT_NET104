namespace _3_Asp.Net_MVC.Models
{
    public class BillDetail
    {
        public Guid ID { get; set; }    
        public Guid BillID { get; set; }
        public Guid ProductID { get; set; }
        public int Quantity { get; set; }
        public long Price { get; set; }
        public virtual Bill Bill { get; set; }  
        public virtual Product Product { get; set; }

    }
}

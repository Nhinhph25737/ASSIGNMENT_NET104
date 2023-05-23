namespace _3_Asp.Net_MVC.Models
{
    public class Bill
    {
        public Guid ID { get; set; }
        public DateTime CreateDate { get; set; }
        public Guid UserID { get; set; }
        public int Status { get; set; }
        public string Recipient { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public List<BillDetail> BillDetails { get; set; }
        public User User { get; set; }
    }
}

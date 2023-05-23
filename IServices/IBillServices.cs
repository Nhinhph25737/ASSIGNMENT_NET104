using _3_Asp.Net_MVC.Models;

namespace _3_Asp.Net_MVC.IServices
{
    public interface IBillServices
    {
        public List<Bill> GetAllBill();
        public Bill GetBillById(Guid id);
        //Phuong thuc them
        public bool CreateBill(Bill p);
        //Phuong thuc sua
        public bool UpdateBill(Bill p);
        //Phuong thuc xoa
        public bool DeleteBill(Guid id);
    }
}

using _3_Asp.Net_MVC.Models;

namespace _3_Asp.Net_MVC.IServices
{
    public interface IBillDetailServices
    {
        public List<BillDetail> GetAllBillDetail();
        public BillDetail GetBillDetailById(Guid id);
        //Phuong thuc them
        public bool CreateBillDetail(BillDetail p);
        //Phuong thuc sua
        public bool UpdateBillDetail(BillDetail p);
        //Phuong thuc xoa
        public bool DeleteBillDetail(Guid id);
    }
}

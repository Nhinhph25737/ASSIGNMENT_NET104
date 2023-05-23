using _3_Asp.Net_MVC.IServices;
using _3_Asp.Net_MVC.Models;

namespace _3_Asp.Net_MVC.Services
{
    public class BillDetailServices : IBillDetailServices
    {
        ShoppingDBContext context;
        public BillDetailServices()
        {
            context = new ShoppingDBContext();
        }
        public bool CreateBillDetail(BillDetail p)
        {
            try
            {
                context.BillDetails.Add(p);
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteBillDetail(Guid id)
        {
            try
            {
                var bd = context.BillDetails.FirstOrDefault(p=> p.ID == id);
                context.BillDetails.Remove(bd);
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<BillDetail> GetAllBillDetail()
        {
           return context.BillDetails.ToList();
        }

        public BillDetail GetBillDetailById(Guid id)
        {
            return context.BillDetails.FirstOrDefault(p=> p.ID==id);
        }

        public bool UpdateBillDetail(BillDetail p)
        {
            try
            {
                var bd = context.BillDetails.FirstOrDefault(c => c.ID == p.ID);
                bd.BillID = p.BillID;
                bd.ProductID = p.ProductID;
                bd.Price = p.Price;
                bd.Quantity = p.Quantity;
                
                context.BillDetails.Update(bd);
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}

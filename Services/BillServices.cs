using _3_Asp.Net_MVC.IServices;
using _3_Asp.Net_MVC.Models;

namespace _3_Asp.Net_MVC.Services
{
    public class BillServices : IBillServices
    {
        ShoppingDBContext context;
        public BillServices()
        {
            context = new ShoppingDBContext();
        }
        public bool CreateBill(Bill p)
        {
            try
            {
                context.Bills.Add(p);
                context.SaveChanges();
                return true;
            }catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteBill(Guid id)
        {
            try
            {
                var b = context.Bills.Find(id);
                context.Bills.Remove(b);
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Bill> GetAllBill()
        {
            return context.Bills.ToList();
        }

        public Bill GetBillById(Guid id)
        {
            return context.Bills.Where(c=> c.ID == id).FirstOrDefault();
        }

        public bool UpdateBill(Bill p)
        {
            try
            {
                var b = context.Bills.FirstOrDefault(b=> b.ID == p.ID);
                b.Status = p.Status;
                b.CreateDate = p.CreateDate;
                b.Recipient = p.Recipient;
                b.PhoneNumber = p.PhoneNumber;
                b.Address = p.Address;
                context.Bills.Update(p);
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

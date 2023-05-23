using _3_Asp.Net_MVC.IServices;
using _3_Asp.Net_MVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace _3_Asp.Net_MVC.Controllers
{
    public class BillController : Controller
    {
        private readonly IBillServices billServices;
        public BillController()
        {
            billServices = new BillServices();
        }
        public IActionResult ListBills()
        {
            var bills = billServices.GetAllBill().OrderByDescending(c=> c.CreateDate).ToList();
            return View(bills);
        }
    }
}

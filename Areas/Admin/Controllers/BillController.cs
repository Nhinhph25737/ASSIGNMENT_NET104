using _3_Asp.Net_MVC.IServices;
using _3_Asp.Net_MVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace _3_Asp.Net_MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]")]
    public class BillController : Controller
    {
        private readonly ILogger<BillController> _logger;
        private readonly IBillServices billServices;
        private readonly IBillDetailServices billDetailServices;

        public BillController(ILogger<BillController> logger)
        {
            _logger = logger;
            billServices = new BillServices();
            billDetailServices = new BillDetailServices();
        }
        public IActionResult ListBills()
        {
            var bills = billServices.GetAllBill().OrderByDescending(c => c.CreateDate).ToList();
            return View(bills);
        }
        public IActionResult BillDetails(Guid id)
        {
            var bill = billServices.GetAllBill().Where(c => c.ID == id).FirstOrDefault();
            ViewData["BillID"] = bill;
            var billdetails = billDetailServices.GetAllBillDetail().Where(c => c.BillID == id).ToList();
            return View(billdetails);
        }
        public IActionResult FilterBillStatus(int status)
        {
            var bills = billServices.GetAllBill().OrderByDescending(c => c.CreateDate).ToList();

            if (status != 999)
            {
                var bill = bills.Where(c => c.Status == status).ToList();
                return View("ListBills", bill);
            }
            else
            {
                return RedirectToAction("ListBills");
            }
        }
        public IActionResult SearchID(Guid id)
        {
            var bills = billServices.GetAllBill().OrderByDescending(c => c.CreateDate).ToList();
            if (id != null)
            {
                bills = bills.Where(c => c.ID == id).ToList();
                return View("ListBills", bills);
            }
            return View("ListBills", bills);
        }
        public IActionResult SearchSDT(string sdt)
        {
            var bills = billServices.GetAllBill().OrderByDescending(c => c.CreateDate).ToList();
            if (sdt != null)
            {
                bills = bills.Where(c => c.PhoneNumber == sdt).ToList();
                return View("ListBills", bills);
            }
            return View("ListBills", bills);
        }
        public IActionResult BillConfirm(Guid id)
        {
            var bills = billServices.GetAllBill().OrderByDescending(c => c.CreateDate).ToList();
            var bill = billServices.GetAllBill().FirstOrDefault(c => c.ID == id);
            if (bill != null)
            {
                bill.Status = 1;
                billServices.UpdateBill(bill);
                return View("ListBills", bills);
            }
            return View("ListBills", bills);
        }
    }
}

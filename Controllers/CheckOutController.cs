using _3_Asp.Net_MVC.IServices;
using _3_Asp.Net_MVC.Models;
using _3_Asp.Net_MVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace _3_Asp.Net_MVC.Controllers
{
    public class CheckOutController : Controller
    {
        private readonly IProductServices productServices;
        private readonly IUserServices userServices;
        private readonly IBillDetailServices billDetailServices;
        private readonly IBillServices billServices;
        private readonly ICartDetailServices cartDetailServices;
        ShoppingDBContext shopDBContext;
        public CheckOutController()
        {
            userServices = new UserServices();
            billDetailServices = new BillDetailServices();
            billServices = new BillServices();
            productServices = new ProductServices();
            shopDBContext = new ShoppingDBContext();
            cartDetailServices = new CartDetailServices();
        }
        //THANH TOÁN
        [HttpPost]
        public IActionResult CheckOut(IFormCollection f)
        {
            if (int.Parse(f["soluong"]) > 0)
            {
                if (HttpContext.Session.GetString("IdUser") != null) // Nếu đăng nhập
                {
                    Guid id = Guid.Parse(HttpContext.Session.GetString("IdUser"));
                    // Tạo hóa đơn
                    Bill bill = new Bill()
                    {
                        UserID = Guid.Parse(HttpContext.Session.GetString("IdUser")),
                        CreateDate = DateTime.Now,
                        Recipient = f["name"],
                        PhoneNumber = f["sdt"],
                        Address = f["diachi"],
                        Status = 1,
                    };
                    shopDBContext.Bills.Add(bill);
                    shopDBContext.SaveChanges();
                    // Lưu vào hóa đơn chi tiết
                    var CartUser = cartDetailServices.GetAllCartDetail().Where(c => c.UserID == id).ToList();
                    for (int i = 0; i < CartUser.Count(); i++)
                    {
                        var product = productServices.GetAllProduct().Where(c => c.ID == CartUser[i].ProductID).FirstOrDefault();
                        BillDetail billDetail = new BillDetail()
                        {
                            BillID = bill.ID,
                            ProductID = CartUser[i].ProductID,
                            Quantity = CartUser[i].Quantity,
                            Price = product.Price,
                        };
                        if (billDetailServices.CreateBillDetail(billDetail))//Kiểm tra lưu thành công
                        {
                            // Trừ số lượng sản phẩm trong kho
                            product.AvailableQuantity -= CartUser[i].Quantity;
                            productServices.UpdateProduct(product);
                            // Xóa trong cart
                            shopDBContext.Remove(CartUser[i]);
                            shopDBContext.SaveChanges();
                        }
                    }
                    return View("CheckOutSuccess");
                }
                else // chưa đăng nhập
                {
                    //Tạo tài khoản
                   User user = new User()
                   {
                       UserName = "null",
                       Password = "null",
                       RoleID = Guid.Parse("F5F014A3-541B-4937-9A09-A4198CDA9CDF"),
                       Status = 0,
                   };
                    shopDBContext.Users.Add(user);
                    shopDBContext.SaveChanges();
                    HttpContext.Session.SetString("IdUser", (user.ID).ToString());
                    // Tạo hóa đơn
                    Bill bill = new Bill()
                    {
                        UserID = user.ID,
                        CreateDate = DateTime.Now,
                        Recipient = f["name"],
                        PhoneNumber = f["sdt"],
                        Address = f["diachi"],
                        Status = 1,
                    };
                    shopDBContext.Bills.Add(bill);
                    shopDBContext.SaveChanges();
                    // Lưu vào Hóa đơn chi tiết
                    var CartUser = SessionServices.GetObjFormSession(HttpContext.Session, "Cart");
                    for (int i = 0; i < CartUser.Count(); i++)
                    {
                        var product = productServices.GetAllProduct().Where(c => c.ID == CartUser[i].ProductID).FirstOrDefault();
                        BillDetail billDetail = new BillDetail()
                        {
                            BillID = bill.ID,
                            ProductID = CartUser[i].ProductID,
                            Quantity = CartUser[i].Quantity,
                            Price = product.Price,
                        };
                        if (billDetailServices.CreateBillDetail(billDetail))//Kiểm tra lưu thành công
                        {
                            // Trừ số lượng sản phẩm trong kho
                            product.AvailableQuantity -= CartUser[i].Quantity;
                            productServices.UpdateProduct(product);
                        }
                    }
                    // Xóa tất cả sản phẩm trong giỏ hàng
                    CartUser.Clear();
                    SessionServices.SetObjToJson(HttpContext.Session, "Cart", CartUser);// Cập nhật lại giỏ hàng
                    return View("CheckOutSuccess");
                }
            }
            else
            {
                TempData["Fail"] = "Bạn chưa có sản phẩm nào trong giỏ";
                return RedirectToAction("ShowCart", "ShoppingCart");
            }
        }
        public IActionResult CheckOutSuccess()
        {
            return View();
        }
        

        public IActionResult UserBills() // Hiển thị tất cả hóa đơn của User hiện tại // Phải đăng nhập mới xem được
        {
            if(HttpContext.Session.GetString("IdUser") != null)// Nếu đăng nhập
            {
                var Userbills = billServices.GetAllBill().Where(c => c.UserID == Guid.Parse(HttpContext.Session.GetString("IdUser"))).OrderByDescending(c => c.CreateDate).ToList();
                if (Userbills.Count == 0)
                {
                    TempData["Message"] = "Không có hóa đơn nào!";
                }
                return View(Userbills);
            }
            return RedirectToAction("ShowCart","ShoppingCart");
        }
        public IActionResult BillDetails(Guid id)
        {
            var bill = billServices.GetAllBill().Where(c => c.ID == id).FirstOrDefault();
            ViewData["BillID"] = bill;
            var billdetails = billDetailServices.GetAllBillDetail().Where(c => c.BillID == id).ToList();
            return View(billdetails);
        }
        [HttpGet]
        public IActionResult EditBill(Guid id)
        {
            var bill = billServices.GetAllBill().Where(c => c.ID == id).FirstOrDefault();
            return View(bill);
        }
        [HttpPost]
        public IActionResult EditBill(Bill bill)
        {
            if (billServices.UpdateBill(bill))
            {
                return RedirectToAction("UserBills", "ShoppingCart");
            }
            return View();
        }
    }
}


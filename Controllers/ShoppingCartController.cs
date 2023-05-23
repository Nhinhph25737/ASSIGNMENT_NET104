using _3_Asp.Net_MVC.IServices;
using _3_Asp.Net_MVC.Models;
using _3_Asp.Net_MVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace _3_Asp.Net_MVC.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly ILogger<ShoppingCartController> _logger;
        private readonly IProductServices _productServices;
        private readonly ICartDetailServices _cartDetailService;
        private readonly IUserServices _userServices;
        ShoppingDBContext _dbContext;

        public ShoppingCartController(ILogger<ShoppingCartController> logger)
        {
            _logger = logger;
            _productServices = new ProductServices();
            _cartDetailService = new CartDetailServices();
            _userServices = new UserServices();
            _dbContext = new ShoppingDBContext();
        }
        // THÊM SẢN PHẨM VÀO GIỎ HÀNG
        public IActionResult AddToCart(Guid id)
        {
            // Từ Id lấy được của Sản phẩm ta lấy ra sản phẩm đó
            var product = _productServices.GetProductById(id);
            if (HttpContext.Session.GetString("IdUser") == null)  // Nếu không đăng nhập, thêm sp thì lưu vào session giỏ hàng
            {
                //Đọc dữ liệu từ Session xem trong Cart nó có cái gì chưa?
                var cartproducts = SessionServices.GetObjFormSession(HttpContext.Session, "Cart");
                if (cartproducts.Count == 0)
                {
                    var cartitem = new CartDetail()
                    {
                        UserID = new Guid(),
                        ProductID = product.ID,
                        Quantity = 1,
                    };
                    cartproducts.Add(cartitem); // Nếu Cart rỗng thì thêm sp vào luôn
                    // Đưa dữ liệu về lại Session
                    SessionServices.SetObjToJson(HttpContext.Session, "Cart", cartproducts);
                }
                else
                {
                    if (!SessionServices.CheckProductInCart(id, cartproducts)) // Cart không rỗng nhưng k chứa sản phẩm đó
                    {
                        var cartitem = new CartDetail()
                        {
                            UserID = new Guid(),
                            ProductID = product.ID,
                            Quantity = 1,

                        };
                        cartproducts.Add(cartitem); // Nếu Cart chưa chứa sản phẩm thì thêm sp vào luôn
                                                    // Đưa dữ liệu về lại Session
                        SessionServices.SetObjToJson(HttpContext.Session, "Cart", cartproducts);
                    }
                    else // Nếu đã có sản phẩm trong giỏ rồi thì tăng số lượng lên 1
                    {
                        var cartitem = cartproducts.Where(c => c.ProductID == id).FirstOrDefault();
                        cartitem.Quantity++;
                        SessionServices.SetObjToJson(HttpContext.Session, "Cart", cartproducts);
                    }
                }
            } // Ngược lại có đăng nhập
            else
            {
                Guid iduser = Guid.Parse(HttpContext.Session.GetString("IdUser"));
                List<CartDetail> cartDetailList = _cartDetailService.GetCartDetailByIdUser(iduser);// Lấy cartitem trong CSDL

                if (cartDetailList.Count == 0)// Giỏ rỗng thì thêm mới
                {
                    var cartitem = new CartDetail()
                    {
                        UserID = Guid.Parse(HttpContext.Session.GetString("IdUser")),
                        ProductID = product.ID,
                        Quantity = 1,
                    };
                    _cartDetailService.CreateCartDetail(cartitem);
                }
                else // Không rỗng thì ktra có chứa sản phẩm đấy không
                {
                    if (!_cartDetailService.CheckPRoductInCart(cartDetailList, id)) // 
                    {
                        var cartitem = new CartDetail()
                        {
                            UserID = Guid.Parse(HttpContext.Session.GetString("IdUser")),
                            ProductID = product.ID,
                            Quantity = 1,
                        };
                        _cartDetailService.CreateCartDetail(cartitem);
                    }
                    else // Nếu đã có sản phẩm trong giỏ rồi thì tăng số lượng lên 1
                    {
                        var cartitem = cartDetailList.Where(c => c.ProductID == id).FirstOrDefault();
                        cartitem.Quantity++;
                        _cartDetailService.UpdateCartDetail(cartitem);
                    }
                }
            }
            return RedirectToAction("ShowCart");
        }


        // HIỂN THỊ SẢN PHẨM TRONG GIỎ HÀNG
        public IActionResult ShowCart()
        {
            // Nếu chưa đăng nhập thì hiển thị toàn bộ cart trong session
            if (HttpContext.Session.GetString("IdUser") == null)
            {
                var products = SessionServices.GetObjFormSession(HttpContext.Session, "Cart");
                if (products.Count == 0)
                {
                    TempData["Message"] = "Không có sản phẩm nào trong giỏ hàng!";
                }
                return View(products);
            }
            else // Lấy toàn bộ cartdetail của người dùng 
            {
                Guid iduser = Guid.Parse(HttpContext.Session.GetString("IdUser"));
                var products = _cartDetailService.GetCartDetailByIdUser(iduser);
                if (products.Count == 0)
                {
                    TempData["Message"] = "Không có sản phẩm nào trong giỏ hàng!";
                }
                return View(products);
            }
        }

        // CẬP NHẬT SỐ LƯỢNG SẢN PHẨM TRONG GIỎ
        public IActionResult Update_Quantity(IFormCollection f)
        {
            var countsp = _productServices.GetProductById(Guid.Parse(f["ID_product"].ToString())).AvailableQuantity; // Lấy Số lượng sản phẩm còn trong DB
            Guid id = Guid.Parse(f["ID_product"].ToString()); // Lấy id sp bị chỉnh sửa trong giỏ
            if (HttpContext.Session.GetString("IdUser") != null) // đã đăng nhập
            {
                var cartdetailUser = _cartDetailService.GetAllCartDetail().Where(c => c.UserID == Guid.Parse(HttpContext.Session.GetString("IdUser"))).ToList();

                if (int.Parse(f["Quantity"].ToString()) == 0) // bằng 0 thì xóa luôn
                {
                    var x = cartdetailUser.Where(c => c.ProductID == id).FirstOrDefault();
                    _dbContext.CartDetails.Remove(x);
                    _dbContext.SaveChanges();

                }
                else if (int.Parse(f["Quantity"].ToString()) > countsp) // Lớn hơn thì thông báo
                {
                    TempData["OverQuantity"] = "Vượt quá số lượng sản phẩm trong kho!";
                    return RedirectToAction("ShowCart");
                }
                else
                { // cập nhật vào giỏ hàng
                    var x = cartdetailUser.Where(c => c.ProductID == id).FirstOrDefault();
                    x.Quantity = int.Parse(f["Quantity"].ToString());
                    _cartDetailService.UpdateCartDetail(x);
                }
                return RedirectToAction("ShowCart");
            }
            else // chưa đăng nhập
            {
                var products = SessionServices.GetObjFormSession(HttpContext.Session, "Cart"); // Lấy cart trong session
                if (int.Parse(f["Quantity"].ToString()) == 0)
                {
                    var x = products.Where(c => c.ProductID == id).FirstOrDefault();
                    products.Remove(x);
                }
                else if (int.Parse(f["Quantity"].ToString()) > countsp)
                {
                    TempData["OverQuantity"] = "Vượt quá số lượng sản phẩm trong kho!";
                    return RedirectToAction("ShowCart");
                }
                else
                {
                    var x = products.Where(c => c.ProductID == id).FirstOrDefault();
                    x.Quantity = int.Parse(f["Quantity"].ToString());
                }
                SessionServices.SetObjToJson(HttpContext.Session, "Cart", products);
                return RedirectToAction("ShowCart");
            }
        }
        // XÓA SẢN PHẨM TRONG GIỎ HÀNG
        public IActionResult RemoveAll()
        {
            if (HttpContext.Session.GetString("IdUser") != null)// đã đăng nhập
            {
                var cartdetails = _cartDetailService.GetCartDetailByIdUser(Guid.Parse(HttpContext.Session.GetString("IdUser")));
                for (int i = 0; i < cartdetails.Count; i++)
                {
                    _dbContext.CartDetails.Remove(cartdetails[i]);
                    _dbContext.SaveChanges();
                }
                return RedirectToAction("ShowCart");
            }
            else
            {
                SessionServices.RemoveAll(HttpContext.Session, "Cart");
                return RedirectToAction("ShowCart");
            }
        }
        public IActionResult RemoveCartItem(Guid id)
        {
            if (HttpContext.Session.GetString("IdUser") != null)// đã đăng nhập
            {
                var cartdetails = _cartDetailService.GetCartDetailByIdUser(Guid.Parse(HttpContext.Session.GetString("IdUser")));
                CartDetail cartItem = cartdetails.Where(c=> c.ID == id).FirstOrDefault();
                    _dbContext.CartDetails.Remove(cartItem);
                    _dbContext.SaveChanges();
                return RedirectToAction("ShowCart");
            }
            else
            {
                var products = SessionServices.GetObjFormSession(HttpContext.Session, "Cart");
                SessionServices.RemoveCartItem(id, products);
                SessionServices.SetObjToJson(HttpContext.Session, "Cart", products);
                return RedirectToAction("ShowCart");
            }
        }
        // TỔNG SẢN PHẨM TRONG GIỎ HÀNG
        public PartialViewResult BagCart()
        {
            int total = 0;
            var products = SessionServices.GetObjFormSession(HttpContext.Session, "Cart");
            if (products != null)
                total = products.Sum(x => x.Quantity);
            ViewBag.QuantityCart = total;
            return PartialView("BagCart");
        }
    }
}

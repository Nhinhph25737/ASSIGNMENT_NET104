using _3_Asp.Net_MVC.IServices;
using _3_Asp.Net_MVC.Models;
using _3_Asp.Net_MVC.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using System.IO;

namespace _3_Asp.Net_MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger; // _logger ghi lại tất cả hành động   
        private readonly IProductServices _productServices; // Interface
        private readonly ICategoryServices _categoryServices;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _productServices = new ProductServices(); // new class (truyen vao 1 su phu thuoc) 
            _categoryServices = new CategoryServices();
        }
        public void LoadCategory()
        {
            var loai = _categoryServices.GetAllCategory();
            ViewBag.TypeName = loai;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }
       
        [HttpGet]
        public IActionResult Redirect()
        {
            LoadCategory();
            var Products = _productServices.GetAllProduct();
            if(Products.Count == 0)
            {
                TempData["Message"] = "Không có sản phẩm nào !";
            }
            return View("Product", Products); // Trả về một View cụ thể đi kèm với Model
        }
        [HttpGet]
        public IActionResult AllProduct()
        {
            var Products = _productServices.GetAllProduct().Where(c=> c.AvailableQuantity >0).ToList();
            return View("AllProduct", Products); // Trả về một View cụ thể đi kèm với Model
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
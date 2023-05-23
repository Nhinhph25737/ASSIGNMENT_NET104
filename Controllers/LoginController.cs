using _3_Asp.Net_MVC.IServices;
using _3_Asp.Net_MVC.Models;
using _3_Asp.Net_MVC.Services;
using _3_Asp.Net_MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace _3_Asp.Net_MVC.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserServices userServices;
        public LoginController()
        {
            userServices = new UserServices();
        }
        public IActionResult Login() // Chỉ thực hiện việc hiển thị ra form Create
        {
            var response = new LoginViewModel();
            return View(response);
        }
        [HttpPost]
        public IActionResult Login (LoginViewModel model)
        {
            if(!ModelState.IsValid) return View(model);
            var user = userServices.GetAllUser().Where(c=> c.UserName == model.UserName).FirstOrDefault();
            if(user != null)
            {
                foreach(var item in userServices.GetAllUser())
                {
                    if (user.Password.Equals(item.Password))
                    {
                        HttpContext.Session.SetString("UserName", user.UserName);
                        HttpContext.Session.SetString("IdUser", (user.ID).ToString());
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        TempData["Message"] = "Tên đăng nhập hoặc mật khẩu không đúng, Hãy nhập lại";
                        if (!ModelState.IsValid) return View(model);
                    }
                }
            }
            TempData["Message"] = "Tên đăng nhập hoặc mật khẩu không đúng, Hãy nhập lại";
            return View(model);
        }
        
        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Login");
        }
        
    }
}

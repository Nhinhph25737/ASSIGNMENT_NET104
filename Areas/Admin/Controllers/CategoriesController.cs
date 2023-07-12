using _3_Asp.Net_MVC.IServices;
using _3_Asp.Net_MVC.Models;
using _3_Asp.Net_MVC.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _3_Asp.Net_MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]")]
    public class CategoriesController : Controller
    {
        // GET: CategoriesController
        private readonly ICategoryServices categoryServices;
        public CategoriesController()
        {
            categoryServices = new CategoryServices();
        }
        public ActionResult ListCategory()
        {
           return View();
        }

        // GET: CategoriesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        
        [HttpGet]
        public ActionResult Create(string Name, string Description, int Status)
        {
            Category ct = new Category();
            ct.Name = Name;
            ct.Status = Status;
            ct.Description = Description;
            categoryServices.CreateCategory(ct);
            return RedirectToAction("ListCategory");
        }

        // POST: CategoriesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoriesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CategoriesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        [HttpGet]
        public ActionResult Delete(Guid id)
        {
            var bill = categoryServices.GetCategoryById(id);
            if (categoryServices.DeleteCategory(bill))
            {
                return RedirectToAction("ListCategory");
            }
            return RedirectToAction("ListCategory");
        }
    }
}

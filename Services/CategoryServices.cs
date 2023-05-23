using _3_Asp.Net_MVC.IServices;
using _3_Asp.Net_MVC.Models;

namespace _3_Asp.Net_MVC.Services
{
    public class CategoryServices : ICategoryServices
    {
        ShoppingDBContext context;
        public CategoryServices()
        {
            context = new ShoppingDBContext();
        }
        public bool CreateCategory(Category p)
        {
            try
            {
                context.Categories.Add(p);
                context.SaveChanges();
                return true;
            }catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteCategory(Category id)
        {
            try
            {
                var c = context.Categories.Find(id);
                context.Categories.Remove(c);
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Category> GetAllCategory()
        {
            return context.Categories.ToList();
        }

        public Category GetCategoryById(Guid id)
        {
            return context.Categories.FirstOrDefault(c=> c.ID == id);
        }

        public Category GetCategoryByName(string name)
        {
            return context.Categories.Where(c=> c.Name == name).FirstOrDefault();
        }

        public bool UpdateCategory(Category p)
        {
            try
            {
                var c = context.Categories.Find(p.ID);
                c.Name = p.Name;
                c.Description = p.Description;
                c.Status = p.Status;
                context.Categories.Update(c);
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

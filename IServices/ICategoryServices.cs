using _3_Asp.Net_MVC.Models;

namespace _3_Asp.Net_MVC.IServices
{
    public interface ICategoryServices
    {
        public List<Category> GetAllCategory();
        public Category GetCategoryById(Guid id);
        public Category GetCategoryByName(string name);
        //Phuong thuc them
        public bool CreateCategory(Category p);
        //Phuong thuc sua
        public bool UpdateCategory(Category p);
        //Phuong thuc xoa
        public bool DeleteCategory(Category id);
    }
}


using _3_Asp.Net_MVC.Models;

namespace _3_Asp.Net_MVC.IServices
{
    public interface IProductServices
    {
        public List<Product> GetAllProduct();
        public Product GetProductById(Guid id);
        public List<Product> GetProductByName(string name);
        //Phuong thuc them
        public bool CreateProduct(Product p);
        //Phuong thuc sua
        public bool UpdateProduct(Product p);
        //Phuong thuc xoa
        public bool DeleteProduct(Guid id);
        // Phuong thuc tim kiem theo mota
        public List<Product> GetProductByDescription(string description);
        // Phuong thuc check trùng tên sản phẩm và nhà sản xuất
        public bool CheckTrung(Product p);
        
    }
}

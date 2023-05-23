using _3_Asp.Net_MVC.IServices;
using _3_Asp.Net_MVC.Models;

namespace _3_Asp.Net_MVC.Services
{
    public class ProductServices : IProductServices
    {
        ShoppingDBContext context;
        public ProductServices()
        {
            context = new ShoppingDBContext();
        }

        public bool CheckTrung(Product p)
        {
            var products = context.Products.ToList();
            foreach (var item in products)
            {
                if (item.ProductName.Equals(p.ProductName) && item.Supplier.Equals(p.Supplier))
                {
                    return true;
                    break;
                }
            }
            return false;
        }

        public bool CreateProduct(Product p)
        {
            try
            {
                context.Products.Add(p);
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteProduct(Guid id)
        {
            try
            {
                var product = context.Products.Find(id); // Find chỉ dùng duy nhất cho ID
                context.Products.Remove(product); // context.Product la DB<Set>
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Product> GetAllProduct()
        {
            return context.Products.ToList(); // Lấy toàn bộ sản phẩm trong cơ sở dữ liệu
        }

        public List<Product> GetProductByDescription(string description)
        {
            return context.Products.Where(c => c.Description.Contains(description)).ToList();
        }

        public Product GetProductById(Guid id)
        {
            return context.Products.FirstOrDefault(p => p.ID == id); // Chay loi ra null
            //return context.Products.SingleOrDefault(p=>p.ID == id); // Chay loi thi ra exception 
        }

        public List<Product> GetProductByName(string name)
        {
            return context.Products.Where(p => p.ProductName.Contains(name)).ToList();
            // Trả về danh sách Sản phẩm mà tên chứa chuỗi cần tìm
        }

        public bool UpdateProduct(Product p)
        {
            try
            {
                var product = context.Products.Find(p.ID);
                product.ProductName = p.ProductName;
                product.Description = p.Description;
                product.Price = p.Price;
                product.Image = p.Image;
                product.Size = p.Size;
                product.TypeName = p.TypeName;
                product.Supplier = p.Supplier;
                product.Status = p.Status;
                product.AvailableQuantity = p.AvailableQuantity;
                product.EditDate = p.EditDate;

                context.Products.Update(product);
                context.SaveChanges(); // Bắt buộc phải có
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}

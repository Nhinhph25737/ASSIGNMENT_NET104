using _3_Asp.Net_MVC.IServices;
using _3_Asp.Net_MVC.Models;

namespace _3_Asp.Net_MVC.Services
{
    public class CartDetailServices : ICartDetailServices
    {
        ShoppingDBContext context;
        public CartDetailServices()
        {
            context = new ShoppingDBContext();
        }

        public bool CheckPRoductInCart(List<CartDetail> cartDetails, Guid id)
        {
            var cart = cartDetails.FirstOrDefault(c=> c.ProductID == id);
            if (cart == null) return false;
            return true;
        }

        public bool CreateCartDetail(CartDetail p)
        {
            try
            {
                context.CartDetails.Add(p);
                context.SaveChanges();
                return true;
            }catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteCartDetail(CartDetail id)
        {
            try
            {
                var c = context.CartDetails.Find(id);
                context.CartDetails.Remove(c);
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<CartDetail> GetAllCartDetail()
        {
            return context.CartDetails.ToList();
        }

        public List<CartDetail> GetCartDetailByIdUser(Guid id)// Trả về những sản phẩm trong giỏ của 1 user
        {
            return context.CartDetails.Where(c=> c.UserID == id).ToList();
        }


        public bool UpdateCartDetail(CartDetail p)
        {
            try
            {
                var c = context.CartDetails.FirstOrDefault(c=> c.ID == p.ID);
                c.UserID = p.UserID;
                c.ProductID = p.ProductID;
                c.Quantity = p.Quantity;
                context.CartDetails.Update(c);
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

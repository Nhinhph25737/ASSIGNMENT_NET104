using _3_Asp.Net_MVC.Models;
using Newtonsoft.Json;
namespace _3_Asp.Net_MVC.Services
{
    static class SessionServices
    {
        // Đưa dữ liệu vào Session dưới dạng Json
        public static void SetObjToJson(ISession session, string key, object value)// object value là dữ liệu thêm vào Session
        {
            // Chuyển đổi dữ liệu qua dạng JsonString
            var JsonString = JsonConvert.SerializeObject(value); // Đưa dạng List về dạng string
            session.SetString(key, JsonString);// Đưa vào Session
        }
        // Lấy và đưa dữ liệu từ Session về dạng List<obj>
        public static List<CartDetail> GetObjFormSession(ISession session, string key)
        {
            var data = session.GetString(key); // Đọc dữ liệu từ Session ở dạng chuỗi
            if (data != null)
            {
                var listObj = JsonConvert.DeserializeObject<List<CartDetail>>(data); // Đưa dạng string về dạng List
                return listObj;
            }
            else
            {
                return new List<CartDetail>();
            }

        }
        // Đưa dữ liệu vào Session dưới dạng Json
        public static void SetProductToJson(ISession session, string key, object value)// object value là dữ liệu thêm vào Session
        {
            // Chuyển đổi dữ liệu qua dạng JsonString
            var JsonString = JsonConvert.SerializeObject(value); // Đưa dạng List về dạng string
            session.SetString(key, JsonString);// Đưa vào Session
        }
        // Lấy và đưa dữ liệu từ Session về dạng List<obj>
        public static List<Product> GetProductFromSession(ISession session, string key)
        {
            var data = session.GetString(key); // Đọc dữ liệu từ Session ở dạng chuỗi
            if (data != null)
            {
                var listObj = JsonConvert.DeserializeObject<List<Product>>(data); // Đưa dạng string về dạng List
                return listObj;
            }
            else
            {
                return new List<Product>();
            }

        }
        // Kiểm tra xem có tồn tại sản phẩm trong giỏ hàng hay không 
        public static bool CheckProductInCart(Guid id, List<CartDetail> cartProducts)
        {
            return cartProducts.Any(p => p.ProductID == id);
        }
        public static void UpdateQuantity(Guid id, List<CartDetail> cartProducts, int quantity)
        {
            var cartItem = cartProducts.FirstOrDefault(p => p.ID == id);
            cartItem.Quantity = quantity;
        }
        public static void RemoveCartItem(Guid id, List<CartDetail> cartProducts)
        {
            var cartItem = cartProducts.FirstOrDefault(p => p.ID == id);
            cartProducts.Remove(cartItem);
        }
        public static void RemoveAll(ISession session, string key)
        {
            session.Clear();
        }
    }
}

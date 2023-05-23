using _3_Asp.Net_MVC.Models;

namespace _3_Asp.Net_MVC.IServices
{
    public interface ICartDetailServices
    {
        public List<CartDetail> GetAllCartDetail();
        public List<CartDetail> GetCartDetailByIdUser(Guid id);
        public bool CheckPRoductInCart(List<CartDetail> cartDetails, Guid id);
        //Phuong thuc them
        public bool CreateCartDetail(CartDetail p);
        //Phuong thuc sua
        public bool UpdateCartDetail(CartDetail p);
        //Phuong thuc xoa
        public bool DeleteCartDetail(CartDetail id);
    }
}

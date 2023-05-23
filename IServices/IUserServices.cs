using _3_Asp.Net_MVC.Models;

namespace _3_Asp.Net_MVC.IServices
{
    public interface IUserServices
    {
        public List<User> GetAllUser();
        public User GetUserById(Guid id);
        //Phuong thuc them
        public bool CreateUser(User p);
        //Phuong thuc sua
        public bool UpdateUser(User p);
        //Phuong thuc xoa
        public bool DeleteUser(User id);
    }
}

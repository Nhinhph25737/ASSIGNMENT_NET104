using _3_Asp.Net_MVC.IServices;
using _3_Asp.Net_MVC.Models;

namespace _3_Asp.Net_MVC.Services
{
    public class UserServices : IUserServices
    {
        ShoppingDBContext context;
        public UserServices()
        {
            context = new ShoppingDBContext();
        }
        public bool CreateUser(User p)
        {
            try
            {
                context.Users.Add(p);
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteUser(User id)
        {
            try
            {
                var user = context.Users.Find(id);
                context.Users.Remove(user);
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<User> GetAllUser()
        {
           return context.Users.ToList();
        }

        public User GetUserById(Guid id)
        {
            return context.Users.Find(id);
        }

        public bool UpdateUser(User p)
        {
            try
            {
                var user = context.Users.FirstOrDefault(c=> c.ID == p.ID);
                user.UserName = p.UserName;
                user.Password = p.Password;
                user.RoleID = p.RoleID;
                user.Status = p.Status;
                context.Users.Update(user);
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

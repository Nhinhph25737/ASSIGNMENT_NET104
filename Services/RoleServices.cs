using _3_Asp.Net_MVC.IServices;
using _3_Asp.Net_MVC.Models;

namespace _3_Asp.Net_MVC.Services
{
    public class RoleServices : IRoleServices
    {
        ShoppingDBContext context;
        public RoleServices()
        {
            context = new ShoppingDBContext();
        }
        public bool CreateRole(Role p)
        {
            try
            {
                context.Roles.Add(p);
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteRole(Role id)
        {
            try
            {
                var r = context.Roles.Find(id);
                context.Roles.Remove(r);
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Role> GetAllRole()
        {
            return context.Roles.ToList();
        }

        public Role GetRoleById(Guid id)
        {
            return context.Roles.Find(id);
        }

        public bool UpdateRole(Role p)
        {
            try
            {
                var r = context.Roles.FirstOrDefault(r=> r.ID == p.ID);
                r.RoleName = p.RoleName;
                r.Description = p.Description;
                r.Status = p.Status;
                context.Roles.Update(r);
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

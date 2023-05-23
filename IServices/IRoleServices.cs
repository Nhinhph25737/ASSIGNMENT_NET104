using _3_Asp.Net_MVC.Models;

namespace _3_Asp.Net_MVC.IServices
{
    public interface IRoleServices
    {
        public List<Role> GetAllRole();
        public Role GetRoleById(Guid id);
        //Phuong thuc them
        public bool CreateRole(Role p);
        //Phuong thuc sua
        public bool UpdateRole(Role p);
        //Phuong thuc xoa
        public bool DeleteRole(Role id);
    }
}

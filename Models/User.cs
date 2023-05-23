using System.ComponentModel.DataAnnotations;

namespace _3_Asp.Net_MVC.Models
{
    public class User
    {
        public Guid ID { get; set; }
        [MinLength(6,ErrorMessage ="Tối thiểu 6 ký tự")]
        public string? UserName { get; set; }
        [MinLength(6, ErrorMessage = "Tối thiểu 6 ký tự")]
        public string? Password { get; set; }
        public Guid RoleID { get; set; }
        public int Status { get; set; }
        public Role Role { get; set; }
        public virtual ICollection<CartDetail> CartDetails { get; set; }
        public virtual ICollection<Bill> Bills { get; set; }


    }
}

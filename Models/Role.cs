namespace _3_Asp.Net_MVC.Models
{
    public class Role
    {
        public Guid ID { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public virtual ICollection<User> Users { get; set; }

    }
}

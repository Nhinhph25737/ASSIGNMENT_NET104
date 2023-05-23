using System.ComponentModel.DataAnnotations;

namespace _3_Asp.Net_MVC.ViewModels
{
    public class LoginViewModel
    {
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage ="Không chứa ký tự đặc biệt")]
        [Required(ErrorMessage ="UserName is required")]
        [MinLength(6, ErrorMessage = "Tối thiểu 6 ký tự")]
        public string UserName { get; set; }
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Không chứa ký tự đặc biệt")]
        [Required(ErrorMessage = "UserName is required")]
        [MinLength(6, ErrorMessage = "Tối thiểu 6 ký tự")]
        public string Password { get; set; }
    }
}

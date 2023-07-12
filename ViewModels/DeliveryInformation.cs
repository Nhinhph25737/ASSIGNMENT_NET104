using System.ComponentModel.DataAnnotations;

namespace _3_Asp.Net_MVC.ViewModels
{
    public class DeliveryInformation
    {
        [Required]
        public string NguoiNhan { get; set; }
        [Required]
        public string SDT { get; set; }
        [Required]
        public string DiaChi { get; set; }  
    }
}

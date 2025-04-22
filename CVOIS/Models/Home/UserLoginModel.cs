using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CVOIS.Models.Home
{
    public class UserLoginModel
    {
        [DisplayName("User Name")]
        [Required(ErrorMessage = "Username is required.")]
        public string Username { get; set; }

        [DisplayName("Password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [DisplayName("Captcha Code")]
        [Required(ErrorMessage = "Captcha is required.")]
        public string Captchacode { get; set; }
    }
}

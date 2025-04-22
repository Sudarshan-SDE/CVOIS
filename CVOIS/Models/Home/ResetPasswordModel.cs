using System.ComponentModel.DataAnnotations;

namespace CVOIS.Models.Home
{
    public class ResetPasswordModel
    {
        public string UserId { get; set; }

        [Required(ErrorMessage = "Old password is required")]
        [Display(Name = "Old Password")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "New password is required")]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{6,}$",
    ErrorMessage = "Password must be at least 6 characters long and include uppercase, lowercase, number, and special character.")]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; }


        [Required(ErrorMessage = "Please confirm your new password")]
        [Compare("NewPassword", ErrorMessage = "Passwords do not match")]
        [Display(Name = "Confirm New Password")]
        public string ConfirmNewPassword { get; set; }
        public string SessionID { get; set; }
    }
}

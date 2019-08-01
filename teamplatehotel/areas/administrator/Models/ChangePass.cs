using System.ComponentModel.DataAnnotations;

namespace TeamplateHotel.Areas.Administrator.Models
{
    public class ChangePass
    {
        public int UserId { get; set; }
        public string Password { get; set; }
        [Required(ErrorMessage = "Please enter new password")]
        [MinLength(5, ErrorMessage = "New password must more than 4 characters")]
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
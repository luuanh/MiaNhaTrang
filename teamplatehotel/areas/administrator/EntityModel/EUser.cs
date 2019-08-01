using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TeamplateHotel.Areas.Administrator.EntityModel
{
    public class EUser
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên đăng nhập")]
        [DisplayName("Tên đăng nhập")]
        [MinLength(8, ErrorMessage = "Tên đăng nhập phải tối thiểu có 8 ký tự")]
        [MaxLength(50, ErrorMessage = "Tối đa 50 ký tự")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        [MinLength(8, ErrorMessage = "Mật khẩu phải tối thiểu có 8 ký tự")]
        [DisplayName("Mật khẩu")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập họ tên")]
        [DisplayName("Họ tên")]
        [MaxLength(50, ErrorMessage = "Tối đa 50 ký tự")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập địa chỉ email")]
        [DisplayName("Địa chỉ email")]
        [EmailAddress(ErrorMessage = "Email address is not correct in form")]
        [MaxLength(100, ErrorMessage = "Tối đa 100 ký tự")]
        public string Email { get; set; }

        [DisplayName("Trạng thái hoạt động")]
        public bool Status { get; set; }

        public string PasswordOld { get; set; }
    }
}
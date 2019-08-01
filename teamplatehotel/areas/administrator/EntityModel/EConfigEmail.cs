using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TeamplateHotel.Areas.Administrator.EntityModel
{
    public class EConfigEmail
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập Port")]
        public int Port { get; set; }

        [DisplayName("Email")]
        [Required(ErrorMessage = "Vui lòng nhập email")]
        [EmailAddress(ErrorMessage = "Vui lòng nhập đúng địa chỉ email")]
        public string Email { get; set; }

        [DisplayName("Password")]
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DisplayName("Host")]
        [Required(ErrorMessage = "Vui lòng nhập host")]
        public string Host { get; set; }
    }
}
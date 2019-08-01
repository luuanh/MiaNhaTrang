using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TeamplateHotel.Areas.Administrator.EntityModel
{
    public class ESendEmail
    {
        public int ID { get; set; }

        [DisplayName("Loại thông báo")]
        [Required(ErrorMessage = "Vui lòng chọn loại thông báo")]
        [Range(1, int.MaxValue, ErrorMessage = "Vui lòng chọn loại thông báo")]
        public int Type { get; set; }

        [DisplayName("Tiêu đề thông báo")]
        [Required(ErrorMessage = "Vui lòng nhập tiêu đề thông báo")]
        [MaxLength(500, ErrorMessage = "Tối đa 500 ký tự")]
        public string Title { get; set; }

        [DisplayName("Nội dung thông báo")]
        [Required(ErrorMessage = "Vui lòng nhập mô tả chi tiết")]
        public string Content { get; set; }

        [DisplayName("Lời nhắn khi gửi thành công")]
        [Required(ErrorMessage = "Vui lòng nhập lời nhắn khi gửi thành công")]
        public string Success { get; set; }

        [DisplayName("Lời nhắn khi gửi thất bại")]
        [Required(ErrorMessage = "Vui lòng nhập lời nhắn khi gửi thất bại")]
        public string Error { get; set; }

    }
}
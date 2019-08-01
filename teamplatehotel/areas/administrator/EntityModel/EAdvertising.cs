using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TeamplateHotel.Areas.Administrator.EntityModel
{
    public class EAdvertising
    {
        public int ID { get; set; }

        [DisplayName("Tiêu đề quảng cáo")]
        [Required(ErrorMessage = "Vui lòng nhập tiêu đề quảng cáo")]
        [MaxLength(250, ErrorMessage = "Tối đa 250 ký tự")]
        public string Title { get; set; }

        [DisplayName("Đường link truy cập")]
        [Url(ErrorMessage = "Vui lòng nhập đúng đường link")]
        [MaxLength(250, ErrorMessage = "Tối đa 250 ký tự")]
        public string Url { get; set; }

        [DisplayName("Hình ảnh quảng cáo")]
        [Required(ErrorMessage = "Vui lòng chọn hình ảnh quảng cáo")]
        public string Image { get; set; }

        [DisplayName("Mở tab mới")]
        public string Target { get; set; }

        [DefaultValue(0)]
        public int? Index { get; set; }

        [DisplayName("Trạng thái hiển thị")]
        public bool Status { get; set; }
    }
}
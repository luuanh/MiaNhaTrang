using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TeamplateHotel.Areas.Administrator.EntityModel
{
    public class ESlider
    {
        public int ID { get; set; }

        public int LanguageID { get; set; }

        public string MenuIDs { get; set; }

        [DisplayName("Tên slide")]
        [MaxLength(250, ErrorMessage = "Tối đa 250 ký tự")]
        [Required(ErrorMessage = "Vui lòng nhập tên slide")]
        public string Title { get; set; }

        [DisplayName("Hình ảnh")]
        [Required(ErrorMessage = "Vui lòng chọn hình ảnh chạy slide")]
        public string Image { get; set; }

        [DisplayName("Link liên kết")]
        [MaxLength(500, ErrorMessage = "Tối đa 500 ký tự")]
        [Url(ErrorMessage = "vui lòng nhập đúng đường link liên kết")]
        public string Link { get; set; }

        public int? Index { get; set; }

        [DisplayName("Hiện thị mọi chuyên mục")]
        public bool ViewAll { get; set; }

        [DisplayName("Trạng thái hiển thị")]
        public bool Status { get; set; }
    }
}
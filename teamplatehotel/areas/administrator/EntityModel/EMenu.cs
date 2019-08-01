using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TeamplateHotel.Areas.Administrator.EntityModel
{
    public class EMenu
    {
        public int ID { get; set; }

        [DisplayName("Tiêu đề")]
        [MaxLength(250, ErrorMessage = "Tối đa 250 ký tự")]
        [Required(ErrorMessage = "Vui lòng nhập tiêu đề menu")]
        public string Title { get; set; }

        [DisplayName("Alias")]
        public string Alias { get; set; }

        [DisplayName("Nằm trong chuyên mục nào")]
        public int? ParentID { get; set; }

        [DisplayName("Kiểu thiển thị")]
        [Required(ErrorMessage = "Vui lòng chọn kiểu hiển thị")]
        [Range(1, int.MaxValue, ErrorMessage = "Vui lòng chọn kiểu hiển thị")]
        public int Type { get; set; }

        [DisplayName("Hình đại diện")]
        [MaxLength(300, ErrorMessage = "Tối đa 300 ký tự")]
        [Required(ErrorMessage = "Vui lòng chọn hình đại diện")]
        public string Image { get; set; }
        public int? Index { get; set; }

        public int Location { get; set; }

        public int Level { get; set; }

        [DisplayName("Đường link")]
        [MaxLength(250, ErrorMessage = "Tối đa 250 ký tự")]
        [Url(ErrorMessage = "Vui lòng nhập đúng đường link")]
        public string Link { get; set; }

        [DisplayName("Tiêu đề trang")]
        [MaxLength(250, ErrorMessage = "Tối đa 250 ký tự")]
        public string MetaTitle { get; set; }

        [DisplayName("Thẻ mô tả")]
        [MaxLength(250, ErrorMessage = "Tối đa 250 ký tự")]
        public string MetaDescription { get; set; }

        [DisplayName("Trạng thái hiển thị")]
        public bool Status { get; set; }

        [DisplayName("Mô tả")]
        public string Description { get; set; }
    }
}
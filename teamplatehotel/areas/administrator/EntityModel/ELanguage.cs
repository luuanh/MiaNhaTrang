using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TeamplateHotel.Areas.Administrator.EntityModel
{
    public class ELanguage
    {
        
        [DisplayName("Ký hiệu ngôn ngữ")]
        [MaxLength(10, ErrorMessage = "Tối đa 10 ký tự")]
        [Required(ErrorMessage = "Vui lòng nhập ký hiệu ngôn ngữ")]
        public string ID { get; set; }

        [DisplayName("Tên ngôn ngữ")]
        [MaxLength(50, ErrorMessage = "Tối đa 50 ký tự")]
        [Required(ErrorMessage = "Vui lòng nhập tên ngôn ngữ")]
        public string Name { get; set; }

        [DisplayName("Icon")]
        [Required(ErrorMessage = "Vui lòng chọn Icon")]
        public string Icon { get; set; }

        [DisplayName("Mặc định")]
        public bool Default { get; set; }
    }
}
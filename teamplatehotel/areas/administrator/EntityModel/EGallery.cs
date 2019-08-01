using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TeamplateHotel.Areas.Administrator.EntityModel
{
    public class EGallery
    {
        public int ID { get; set; }

        [DisplayName("Tên ảnh")]
        public string Title { get; set; }

        public int? Index { get; set; }

        [DisplayName("Hình ảnh")]
        [Required(ErrorMessage = "Vui lòng lựa chọn hình ảnh")]
        public string Image { get; set; }
    }
}
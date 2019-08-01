using System.ComponentModel;

namespace TeamplateHotel.Areas.Administrator.ModelShow
{
    public class ShowSlider
    {
        public int ID { get; set; }

        [DisplayName("Tên slide ảnh")]
        public string Title { get; set; }

        [DisplayName("Hình ảnh")]
        public string Image { get; set; }

        [DisplayName("Đường link")]
        public string Link { get; set; }

        [DisplayName("Trạng thái hiển thị")]
        public bool Status { get; set; }

        [DisplayName("Hiển thị mọi chuyên mục")]
        public bool ViewAll { get; set; }

        [DisplayName("Thứ tự hiển thị")]
        public int? Index { get; set; }
    }
}
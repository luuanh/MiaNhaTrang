using System.ComponentModel;

namespace TeamplateHotel.Areas.Administrator.ModelShow
{
    public class ShowArticle
    {
        public int ID { get; set; }

        [DisplayName("Tiêu đề")]
        public string Title { get; set; }

        [DisplayName("Chuyên mục")]
        public string TitleMenu { get; set; }

        [DisplayName("Thứ tự hiển thị")]
        public int? Index { get; set; }

        [DisplayName("Trạng thái hiển thị")]
        public bool Status { get; set; }

        [DisplayName("Bài viết giới thiệu")]
        public bool Home { get; set; }

        [DisplayName("Bài viết hot")]
        public bool Hot { get; set; }

        [DisplayName("Ý kiến khách hàng")]
        public bool Customer { get; set; }

        [DisplayName("Bài viết nổi bật")]
        public bool New { get; set; }
    }
}
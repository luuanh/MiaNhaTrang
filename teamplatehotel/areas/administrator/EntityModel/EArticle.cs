using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TeamplateHotel.Areas.Administrator.EntityModel
{
    public class EArticle
    {
        public int ID { get; set; }

        [DisplayName("Chuyên mục bài viết")]
        [Required(ErrorMessage = "Vui lòng chọn chuyên mục bài viết")]
        [Range(1, int.MaxValue, ErrorMessage = "Vui lòng chọn chuyên mục bài viết")]
        public int MenuID { get; set; }

        [DisplayName("Tiêu đề bài viết")]
        [Required(ErrorMessage = "Vui lòng nhập tiêu đề bài viết")]
        [MaxLength(250, ErrorMessage = "Tối đa 250 ký tự")]
        public string Title { get; set; }

        [DisplayName("Alias")]
        [MaxLength(250, ErrorMessage = "Tối đa 250 ký tự")]
        public string Alias { get; set; }

        [DisplayName("Ảnh đại diện")]
        [MaxLength(300, ErrorMessage = "Tối đa 300 ký tự")]
        public string Image { get; set; }

        [DisplayName("Mô tả")]
        public string Description { get; set; }

        [DisplayName("Mô tả chi tiết")]
        [Required(ErrorMessage = "Vui lòng nhập mô tả chi tiết")]
        public string Content { get; set; }

        public int? Index { get; set; }

        [DisplayName("Tiêu đề trang")]
        [MaxLength(250, ErrorMessage = "Tối đa 250 ký tự")]
        public string MetaTitle { get; set; }

        [DisplayName("Thẻ mô tả")]
        [MaxLength(250, ErrorMessage = "Tối đa 250 ký tự")]
        public string MetaDescription { get; set; }

        [DisplayName("Trạng thái hiển thị")]
        public bool Status { get; set; }

        [DisplayName("Hiển thị trang chủ")]
        public bool Home { get; set; }

        [DisplayName("Bài viết hot")]
        public bool Hot { get; set; }

        [DisplayName("Top")]
        public bool Top { get; set; }

        [DisplayName("Bài viết nổi bật")]
        public bool New { get; set; }

      public List<EGalleryArticle> EGalleryArticles { get; set; }

    }
    public class EGalleryArticle
    {
        public string Image { get; set; }
    }
}
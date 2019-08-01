using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ProjectLibrary.Database;

namespace TeamplateHotel.Areas.Administrator.EntityModel
{
    public class ETour
    {
        public int ID { get; set; }

        [DisplayName("Chuyên mục Tour")]
        [Range(1, int.MaxValue, ErrorMessage = "Vui lòng chọn chuyên mục tour")]
        public int MenuID { get; set; }

        [DisplayName("Tên tour")]
        [MaxLength(250, ErrorMessage = "Tối đa 250 ký tự")]
        [Required(ErrorMessage = "Vui lòng nhập tên tour")]
        public string Title { get; set; }

        [DisplayName("Alias")]
        [MaxLength(250, ErrorMessage = "Tối đa 250 ký tự")]
        public string Alias { get; set; }

        [DisplayName("Hình đại diện")]
        [MaxLength(300, ErrorMessage = "Tối đa 300 ký tự")]
        [Required(ErrorMessage = "Vui lòng chọn hình đại diện")]
        public string Image { get; set; }

        

        [DisplayName("Mô tả")]
        public string Description { get; set; }

        public int? Index { get; set; }

        [DisplayName("Tiêu đề trang")]
        [MaxLength(250, ErrorMessage = "Tối đa 250 ký tự")]
        public string MetaTitle { get; set; }

        [DisplayName("Thẻ mô tả")]
        [MaxLength(250, ErrorMessage = "Tối đa 250 ký tự")]
        public string MetaDescription { get; set; }

        [DisplayName("Trạng thái hiển thị")]
        public bool Status { get; set; }
        [DisplayName("Ngày khởi hành")]
        public DateTime  DateStart { get; set; }
        [DisplayName("Địa điểm khởi hành")]
        public string AddressStart { get; set; }
        [DisplayName("Giá")]
        public float Price { get; set; }
        [DisplayName("Số ngày tour")]
        public int NumberDay { get; set; }
        public List<EGalleryITem> EGalleryITems { get; set; }

        public List<TabTour> TabTours { get; set; }
    }
}
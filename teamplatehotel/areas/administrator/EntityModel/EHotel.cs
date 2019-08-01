using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TeamplateHotel.Areas.Administrator.EntityModel
{
    public class EHotel
    {
        public int ID { get; set; }

        public string LanguageID { get; set; }

        [DisplayName("Tên khách sạn")]
        [Required(ErrorMessage = "Vui lòng nhập tên khách sạn")]
        [MaxLength(250, ErrorMessage = "Tối đa 250 ký tự")]
        public string Name { get; set; }

        [DisplayName("Logo khách sạn")]
        [Required(ErrorMessage = "Vui lòng chọn logo")]
        public string Logo { get; set; }

        [DisplayName("Ảnh đại diện")]
        [Required(ErrorMessage = "Vui lòng chọn ảnh đại diện")]
        public string Image { get; set; }

        [DisplayName("Số điện thoại")]
        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        [MaxLength(100, ErrorMessage = "Tối đa 100 ký tự")]
        public string Tel { get; set; }

        [DisplayName("Fax")]
        [MaxLength(100, ErrorMessage = "Tối đa 100 ký tự")]
        public string Fax { get; set; }

        [DisplayName("Địa chỉ email")]
        [Required(ErrorMessage = "Vui lòng nhập địa chỉ email")]
        [EmailAddress(ErrorMessage="vui lòng nhập đúng địa chỉ email")]
        public string Email { get; set; }

        [DisplayName("Địa chỉ khách sạn")]
        [Required(ErrorMessage = "Vui lòng nhập địa chỉ khách sạn")]
        [MaxLength(250, ErrorMessage = "Tối đa 250 ký tự")]
        public string Address { get; set; }

        [DisplayName("Vị trí khách sạn trên google")]
        [MaxLength(50, ErrorMessage = "Tối đa 50 ký tự")]
        public string Location { get; set; }

        [DisplayName("Ký hiệu mã booking")]
        [Required(ErrorMessage = "Vui lòng nhập ký hiệu mã booking")]
        [MaxLength(20, ErrorMessage = "Tối đa 20 ký tự")]
        public string CodeBooking { get; set; }

        [DisplayName("Website")]
        [Required(ErrorMessage = "Vui lòng nhập địa chỉ website của khách sạn")]
        [MaxLength(100, ErrorMessage = "Tối đa 100 ký tự")]
        public string Website { get; set; }


        [DisplayName("Mã đánh giá tripadvisor")]
        [MaxLength(2000, ErrorMessage = "Tối đa 2000 ký tự")]
        public string Tripadvisor { get; set; }

        [DisplayName("Link trang facebook")]
        [MaxLength(250, ErrorMessage = "Tối đa 250 ký tự")]
        public string FaceBook { get; set; }

        [DisplayName("Link Instagram")]
        [MaxLength(250, ErrorMessage = "Tối đa 250 ký tự")]
        public string Instagram { get; set; }

        [DisplayName("Link twitter")]
        [MaxLength(250, ErrorMessage = "Tối đa 250 ký tự")]
        public string Twitter { get; set; }

        [DisplayName("Link youtube")]
        [MaxLength(250, ErrorMessage = "Tối đa 250 ký tự")]
        public string Youtube { get; set; }

        [DisplayName("Copyright")]
        [Required(ErrorMessage = "Vui lòng nhập copyright")]
        [MaxLength(300, ErrorMessage = "Tối đa 300 ký tự")]
        public string CopyRight { get; set; }

        [DisplayName("Tiêu đề trang")]
        [MaxLength(250, ErrorMessage = "Tối đa 250 ký tự")]
        public string MetaTitle { get; set; }

        [DisplayName("Thẻ mô tả")]
        [MaxLength(250, ErrorMessage = "Tối đa 250 ký tự")]
        public string MetaDescription { get; set; }

        [DisplayName("Term & Condition")]
        [Required(ErrorMessage = "Vui lòng nhập Term & Condition")]
        public string Condition { get; set; }

    }
}
using System.ComponentModel;

namespace TeamplateHotel.Areas.Administrator.ModelShow
{
    public class ShowMenu
    {
        public int ID { get; set; }

        [DisplayName("Tiêu đề")]
        public string Title{ get; set; }
        [DisplayName("Thứ tự hiển thị")]
        public int? Index { get; set; }
        [DisplayName("Kiểu hiển thị")]
        public string TypeMenu { get; set; }
        [DisplayName("Trạng thái hiển thị")]
        public bool Status { get; set; }
        public int Level { get; set; }
       
    }
}
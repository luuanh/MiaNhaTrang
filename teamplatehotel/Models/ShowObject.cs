namespace TeamplateHotel.Models
{
    public class ShowObject
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Alias { get; set; }
        public string MenuAlias { get; set; }
        public string Image { get; set; }
        public string ImageBackground { get; set; }
        public string Description { get; set; }
        public int? Index { get; set; }
        public decimal? Price { get; set; }
        public string SecondMenu { get; set; }
        public int? ParentID { get; set; }
    }
}
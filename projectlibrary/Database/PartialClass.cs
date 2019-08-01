using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

// ReSharper disable CheckNamespace
namespace ProjectLibrary.Database
// ReSharper restore CheckNamespace
{

    public partial class User
    {
        [NotMapped]
        public string ConfirmPassword { get; set; }
    }

    public partial class Article
    {
        [NotMapped]
        public string MenuAlias { get; set; }
    }

    public partial class Room
    {
        [NotMapped]
        public string MenuAlias { get; set; }
    }

    public partial class Service
    {
        [NotMapped]
        public string MenuAlias { get; set; }
    }

    public partial class Tour
    {
        [NotMapped]
        public string MenuAlias { get; set; }
    }

    public partial class Contact
    {
        [NotMapped]
        public string Captcha { get; set; }
    }

    public partial class BookRoom
    {
        [NotMapped]
        public List<ListRoomBooking> ListRoomBookings { get; set; }
    }

    public class ListRoomBooking
    {
        public int RoomId { get; set; }
        public string NameRoom { get; set; }
        public decimal? Price { get; set; }
        public int MaxPeople { get; set; }
        public int Number { get; set; }
        public string Content { get; set; }
    }

}

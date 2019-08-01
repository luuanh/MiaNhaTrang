using System.Collections.Generic;

namespace ProjectLibrary.Config
{
    public class SystemMenuType
    {
        public const int Home = 1;
        public const int Article = 2;
        public const int RoomRate = 3;
        public const int Tour = 4;
        public const int Contact = 5;
        public const int Booking = 6;
        public const int Gallery = 7;
        public const int Location = 8;
        public const int OutSite = 9;
        public const int Service = 10;
        public const int About = 11;
        public const int WiningDining = 12;
        public const int MeetingWedding = 13;
        public static Dictionary<int, string> CategoryType = new Dictionary<int, string>()
                                                                 {
                                                                     {Home, "Trang chủ"},
                                                                     {Article, "Trang bài viết"},
                                                                     {RoomRate, "Trang phòng"},
                                                                     {Tour, "Trang tours"},
                                                                     {Service, "Trang dịch vụ"},
                                                                     {Contact, "Trang liên hệ"},
                                                                     {Booking, "Trang đặt phòng"},
                                                                     {Gallery, "Trang gallery"},
                                                                     {Location, "Trang vị trí"},
                                                                     {OutSite, "Trang link ra ngoài"},
                                                                     {About,"Giới thiệu" },
                                                                      {WiningDining,"Wining Dining" },
                                                                     {MeetingWedding,"Meeting Wedding" },

                                                                 };
    }
}

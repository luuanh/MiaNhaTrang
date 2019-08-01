using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace ProjectLibrary.Config
{
    public class TypeSendEmail
    {
        public static int Contact = 1;
        public static int BookTour = 2;
        public static int BookRoom = 3;

        public static List<ListItem> ListTypeSendEmail()
        {
            var listTypeSendEmail = new List<ListItem>
            {
                new ListItem
                {
                    Value = Contact.ToString(),
                    Text = "Liên hệ",
                },
                new ListItem
                {
                    Value = BookTour.ToString(),
                    Text = "Đặt tour",
                },
                new ListItem
                {
                    Value = BookRoom.ToString(),
                    Text = "Đặt phòng",
                },
            };

            return listTypeSendEmail;
        }
    }
}
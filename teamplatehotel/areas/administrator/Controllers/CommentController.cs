using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using ProjectLibrary.Config;
using ProjectLibrary.Database;
using ProjectLibrary.Security;

namespace TeamplateHotel.Areas.Administrator.Controllers
{
    public class CommentController : BaseController
    {
        //////////////////////// Room ///////////////////////////////////
        public static List<SelectListItem> ListRoom()
        {
            //check logged
            using (var db = new MyDbDataContext())
            {
                var listRoom = new List<SelectListItem>
                {
                    new SelectListItem
                    {
                        Text = "Lựa chọn phòng",
                        Value = "0"
                    }
                };
                listRoom.AddRange(
                    db.Rooms.Select(
                        a => new SelectListItem {Text = a.Title, Value = a.ID.ToString(CultureInfo.InvariantCulture)})
                        .ToList());
                return listRoom;
            }
        }
        //Danh sách ngôn ngữ
        public static List<Language> ListLanguage()
        {
            using (var db = new MyDbDataContext())
            {
                return db.Languages.ToList();
            }
        }
        //Ngôn ngữ hiện tại
        public static string CurentLanguage(string languageId)
        {
            using (var db = new MyDbDataContext())
            {
                var firstOrDefault = db.Languages.FirstOrDefault(a=>a.ID == languageId);
                if (firstOrDefault != null)
                    return firstOrDefault.Name ?? "";
                else
                {
                    return "";
                }
            }
        }
        //lấy họ tên người đăng nhập
        public static string GetFullName(string cookie)
        {
            using (var db = new MyDbDataContext())
            {
                string cookieClient = cookie;
                string deCodecookieClient = CryptorEngine.Decrypt(cookieClient, true);
                string userName = deCodecookieClient.Substring(0, deCodecookieClient.IndexOf("||"));
                return db.Users.FirstOrDefault(a => a.UserName == userName).FullName;
            }
        }
        //lấy danh sách menu theo theo kiểu menu
        [HttpPost]
        public JsonResult GetListMenu(int typeMenu, string languageID)
        {
            //check logged
            var listMenu = new List<Menu>();

            listMenu = MenuController.GetListMenu(SystemMenuLocation.ListLocationMenu().ToList()[0].LocationId, languageID);
            listMenu = listMenu.Where(a => a.Type == typeMenu).ToList();

            foreach (Menu menu in listMenu)
            {
                string titleMenu = "";
                for (int i = 1; i <= menu.Level; i++)
                {
                    titleMenu += "|--";
                }
                menu.Title = titleMenu + menu.Title;
            }
            return Json(new {Result = "OK", ListMenu = listMenu.Select(a => new {MenuId = a.ID, a.Title}).ToList()});
        }
        //Show messages
        public static string Messages(object messages)
        {
            if (messages != null)
                return messages.ToString();
            return "";
        }
    }
}
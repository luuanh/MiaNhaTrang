using System.Collections.Generic;
using System.Globalization;
using System.Web.Mvc;
using ProjectLibrary.Config;
using ProjectLibrary.Database;
using System.Linq;
using ProjectLibrary.Security;
using TeamplateHotel.Models;

namespace TeamplateHotel.Controllers
{
    public class TourController : Controller
    {
       ////Lấy danh sách tour cấp1
       // public static ListTourRoot GetListAllTour(Menu menu)
       // {
       //     using (var db = new MyDbDataContext())
       //     {

       //         var listMenu = new List<Menu>();
       //         if (menu.ParentId == 0)
       //         {
       //             listMenu =
       //                 db.Menus.Where(
       //                     a =>
       //                     a.LanguageId == CurrentSession.LanguageId && a.ParentId != 0 && a.Status &&
       //                     a.TypeMenu == SystemMenuType.TypeTour).ToList();
       //         }
       //         else
       //         {
       //             listMenu= db.Menus.Where(a => a.ParentId == menu.MenuId).ToList();
       //         }

       //         var listTour =
       //             db.Tours.Where(a => a.Status).ToList().Join(listMenu, a => a.MenuId, b => b.MenuId, (a, b) => new ShowObject()
       //                                                                                                     {
       //                                                                                                         Id = a.TourID,
       //                                                                                                         MenuId = b.MenuId,
       //                                                                                                         Alias = a.AliasTour,
       //                                                                                                         Name = a.TitleTour,
       //                                                                                                         Description = a.Description,
       //                                                                                                         Image = a.Image,
       //                                                                                                         MenuAlias = b.AliasMenu
       //                                                                                                     }).ToList();
       //         ListTourRoot listTourRoot = new ListTourRoot
       //                                         {
       //                                             Menus = listMenu,
       //                                             ShowObjects = listTour
       //                                         };
       //         return listTourRoot ?? new ListTourRoot();
       //     }
       // }
       // //Danh sách tuor thuộc một chuyên mục
       // public static PagingOject GetListTourByMenuTour(int menuId, int page)
       // {
       //     using(var db = new MyDbDataContext())
       //     {
       //         var detailMenu = db.Menus.FirstOrDefault(a => a.MenuId == menuId);
       //         var listTour = db.Tours.Where(a => a.MenuId == menuId).Select(a=> new ShowObject()
       //         {
       //             Id = a.TourID,
       //             MenuId = a.MenuId,
       //             Alias = a.AliasTour,
       //             Name = a.TitleTour,
       //             Description = a.Description,
       //             Image = a.Image,
       //             MenuAlias = detailMenu.AliasMenu
       //         }).ToList();
       //         //Lấy danh sách và phân trang
       //         PagingOject pagingOject = CommentController.PagingOnject(listTour, page);
       //         return pagingOject;
       //     }
       // }
    }

}

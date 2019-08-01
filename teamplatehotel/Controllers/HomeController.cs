using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectLibrary.Config;
using ProjectLibrary.Database;
using TeamplateHotel.Models;

namespace TeamplateHotel.Controllers
{
    public class HomeController : BasicController
    {
        [HttpGet]
        public ActionResult Index(object aliasMenuSub, object idSub, object aliasSub)
        {
            var db = new MyDbDataContext();
            Hotel hotel = CommentController.DetailHotel(Request.Cookies["LanguageID"].Value);
            ViewBag.MetaTitle = hotel.MetaTitle;
            ViewBag.MetaDesctiption = hotel.MetaDescription;


            if (aliasMenuSub.ToString() == "System.Object")
            {
                Menu menu1 = db.Menus.FirstOrDefault(a => a.Type == SystemMenuType.Home && a.LanguageID== Request.Cookies["LanguageID"].Value);
                ViewBag.Menu = menu1;
                return View("Index");
            }
            if (aliasMenuSub.ToString() == "SelectLanguge")
            {
                Language language = db.Languages.FirstOrDefault(a => a.ID == idSub.ToString());
                if (language == null)
                {
                    language = db.Languages.FirstOrDefault();
                }
                HttpCookie langCookie = Request.Cookies["LanguageID"];
                langCookie.Value = language.ID;
                langCookie.Expires = DateTime.Now.AddDays(10);
                HttpContext.Response.Cookies.Add(langCookie);
                return Redirect("/");
            }
            if (aliasMenuSub.ToString() == "offer")
            {
                return View("Offer");
            }
            if (aliasMenuSub.ToString() == "about")
            {
                return View("About");
            }
            // xac dinh phong
            Room room = db.Rooms.FirstOrDefault(a => a.Alias == aliasMenuSub.ToString());
            if (room != null)
            {

                return View("Room/DetailRoom", room);
            }
            // xác định menu => tìm ra Kiểu hiển thị của menu
            Menu menu = db.Menus.FirstOrDefault(a => a.Alias == aliasMenuSub.ToString());
            if (menu == null)
            {
                return View("404");
            }
            //Seo
            ViewBag.MetaTitle = menu.MetaTitle;
            ViewBag.MetaDesctiption = menu.MetaDescription;
            ViewBag.Menu = menu;

            switch (menu.Type)
            {
                case SystemMenuType.Article:
                    goto Trangbaiviet;
                case SystemMenuType.MeetingWedding:
                    goto MeetingWedding;

                case SystemMenuType.WiningDining:
                    goto WiningDining;
                  
                case SystemMenuType.Tour:
                    goto TrangTour;
                case SystemMenuType.RoomRate:
                    goto TrangRoom;
                case SystemMenuType.Service:
                    goto Service;
                case SystemMenuType.Booking:
                    return RedirectToAction("MakeReservation", "Booking");
                case SystemMenuType.Contact:
                    Random random = new Random();
                    int iNumber = random.Next(10000, 99999);
                    Session["Captcha"] = iNumber.ToString();
                    return View("Contact");
                case SystemMenuType.About:
                    return View("About");
                case SystemMenuType.Gallery:
                    return View("Gallery", CommentController.Gallery());
                case SystemMenuType.Location:
                    //Lấy bài viết Location
                    ViewBag.ArticleByRoomRate = db.Articles.FirstOrDefault(a => a.MenuID == menu.ID);
                    return View("Location");
                default:
                    return View("Index");
            }

        #region "Trang bài viết"
        Trangbaiviet:
            if (idSub.ToString() != "System.Object")
            {
                int idArticle;
                int.TryParse(idSub.ToString(), out idArticle);
                DetailArticle detailArticle = CommentController.Detail_Article(idArticle);
                ViewBag.MetaTitle = detailArticle.Article.MetaTitle;
                ViewBag.MetaDesctiption = detailArticle.Article.MetaDescription;
                return View("Article/DetailArticle", detailArticle);
            }
            //Danh sách bài viết
            List<Article> articles = CommentController.GetArticles(menu.ID);
            //if (articles.Count == 1)
            //{
            //    DetailArticle detailArticle = CommentController.Detail_Article(articles[0].ID);
            //    ViewBag.MetaTitle = detailArticle.Article.MetaTitle;
            //    ViewBag.MetaDesctiption = detailArticle.Article.MetaDescription;
            //    return View("Article/DetailArticle", detailArticle);
            //}
            return View("Article/ListArticle", articles);
        #endregion

        //Trường hợp: Room
        #region "Kiểu Room & rate"
        TrangRoom:
            if (idSub.ToString() != "System.Object")
            {
                int id;
                int.TryParse(idSub.ToString(), out id);
                //Kiểm tra xem alias truyền đến có phải là 1 bài viết không
                var articleRoom = db.Articles.FirstOrDefault(a => a.ID == id);
                if (articleRoom != null)
                {
                    ViewBag.MetaTitle = articleRoom.MetaTitle;
                    ViewBag.MetaDesctiption = articleRoom.MetaDescription;
                    return View("Article/DetailArticle", CommentController.Detail_Article(id));
                }
                //chi tiết Room
                DetailRoom detailRoom = CommentController.Detail_Room(id, menu.ID);
                ViewBag.MetaTitle = detailRoom.Room.MetaTitle;
                ViewBag.MetaDesctiption = detailRoom.Room.MetaDescription;

                return View("Room/DetailRoom", detailRoom);
            }
            //Lấy bài viết RoomRate
            var articlrByRoomRate = db.Articles.FirstOrDefault(a => a.MenuID == menu.ID);
            if (articlrByRoomRate != null)
            {
                articlrByRoomRate.MenuAlias = articlrByRoomRate.Menu.Alias;
            }
            ViewBag.ArticleByRoomRate = articlrByRoomRate;
            return View("Room/ListRoom", CommentController.GetRooms(Request.Cookies["languageID"].Value));
        #endregion
        //Trang Service
        #region "Trang Service"
        Service:
            if (idSub.ToString() != "System.Object")
            {
                int id;
                int.TryParse(idSub.ToString(), out id);
                DetailService detailService = CommentController.Detail_Service(id);
                ViewBag.MetaTitle = detailService.Service.MetaTitle;
                ViewBag.MetaDesctiption = detailService.Service.MetaDescription;
                return View("Service/DetailService", detailService);

            }
            List<Service> services = CommentController.GetServices(menu.ID);
            //if (services.Count == 1)
            //{
            //    DetailService detailService = CommentController.Detail_Service(services[0].ID);
            //    ViewBag.MetaTitle = detailService.Service.MetaTitle;
            //    ViewBag.MetaDesctiption = detailService.Service.MetaDescription;
            //    return View("Service/DetailService", detailService);
            //}
            return View("Service/ListService", services);
        #endregion

        //trường hợp: Tour
        #region "Kiếu tour"
        TrangTour:
            if (idSub.ToString() != "System.Object")
            {
                int idTour;
                int.TryParse(idSub.ToString(), out idTour);
                DetailTour detailTour = CommentController.Detail_Tour(idTour);
                ViewBag.MetaTitle = detailTour.Tour.MetaTitle;
                ViewBag.MetaDesctiption = detailTour.Tour.MetaDescription;
                return View("Tour/DetailTour", detailTour);
            }
            return View("Tour/ListTour", CommentController.GetTours());
            #endregion
            // kieu MeetingWedding
            #region "MeetingWedding"
            MeetingWedding:
            List<Article> listArticle = CommentController.GetArticles(menu.ID);
            return View("Article/Meeting", listArticle);
            #endregion

            // kieu WiningDining
            #region "WiningDining"
            WiningDining:
            List<ShowArticle> WiningDining = CommentController.ShowArticle(menu.ID);
            return View("Article/Dinning", WiningDining);
            #endregion
        }
    }
}
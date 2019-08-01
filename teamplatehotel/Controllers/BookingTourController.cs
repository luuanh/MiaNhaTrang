using System;
using System.Linq;
using System.Web.Mvc;
using ProjectLibrary.Config;
using ProjectLibrary.Database;

namespace TeamplateHotel.Controllers
{
    public class BookingTourController : Controller
    {
        // GET: /BookingTour/
        [HttpGet]
        public ActionResult BookTour(int id, string alias)
        {
            using (var db = new MyDbDataContext())
            {
                var bookTour = new BookTour();
                Tour tour = db.Tours.FirstOrDefault(a => a.ID == id);
                if (tour == null)
                    return View("404");
                bookTour.ID = tour.ID;
                bookTour.InfoBooking = tour.Title;
                TempData["TourName"] = tour.Title;
                return View("BookTour",tour);
            }
        }

        [HttpPost]
        public ActionResult SendBooking(BookTour model)
        {
            string status = "success";
            model.CreateDate = DateTime.Now;
          
            if (ModelState.IsValid)
            {
                try
                {
                    using (var db = new MyDbDataContext())
                    {
                        Hotel hotel = CommentController.DetailHotel(Request.Cookies["LanguageID"].Value);
                        Tour tour = db.Tours.Where(a => a.ID == model.TourID).FirstOrDefault();
                        model.Departure =(DateTime)tour.DateStart;
                        model.InfoBooking = tour.Title;
                        string codeBooking = hotel.CodeBooking + "T1";
                        if (db.BookTours.Any())
                        {
                            codeBooking = hotel.CodeBooking + "T" +
                                          (db.BookTours.OrderByDescending(a => a.ID).FirstOrDefault().ID + 1);
                        }
                        model.Code = codeBooking;
                        db.BookTours.InsertOnSubmit(model);
                        db.SubmitChanges();

                        SendEmail sendEmail =
                        db.SendEmails.FirstOrDefault(
                            a => a.Type == TypeSendEmail.BookTour && a.LanguageID == Request.Cookies["LanguageID"].Value);

                        sendEmail.Title = sendEmail.Title.Replace("{HotelName}", hotel.Name);
                        string content = sendEmail.Content;
                        content = content.Replace("{Code}", model.Code);
                        content = content.Replace("{Departure}", model.Departure.ToString("MMMM, dd, yyyy"));
                        content = content.Replace("{InfoBooking}", model.InfoBooking);
                        content = content.Replace("{Gender}", model.Gender);
                        content = content.Replace("{FullName}", model.FullName);
                        content = content.Replace("{Address}", model.Address);
                        content = content.Replace("{Tel}", model.Tel);
                        //content = content.Replace("{Country}", model.Country);
                        content = content.Replace("{Email}", model.Email);
                        content = content.Replace("{Request}", model.Request);
                        content = content.Replace("{HotelName}", hotel.Name);
                        content = content.Replace("{HotelEmail}", hotel.Email);
                        content = content.Replace("{HotelTel}", hotel.Tel);
                        content = content.Replace("{Website}", hotel.Website);

                        //Gửi thông tin cho khách hàng
                        MailHelper.SendMail(model.Email, sendEmail.Title, content);
                        //Gửi thông tin cho khách sạn
                        MailHelper.SendMail(hotel.Email, hotel.Name + " (" + model.Code + ")- Booking tour of " + model.FullName, content);
                    }
                }

                catch(Exception ex)
                {
                    status = "error";
                }
            }
            return Redirect("/BookTour/Messages?status=" + status);
        }

        [HttpGet]
        public ActionResult Messages()
        {
            using (var db = new MyDbDataContext())
            {
                SendEmail sendEmail =
                       db.SendEmails.FirstOrDefault(
                           a => a.Type == TypeSendEmail.BookTour && a.LanguageID == Request.Cookies["LanguageID"].Value);

                string status = Request.Params["status"];
                ViewBag.Messages = "";
                if (string.IsNullOrEmpty(status) == false)
                {
                    if (status.Equals("success"))
                    {
                        ViewBag.Messages = sendEmail.Success;
                    }
                    else
                    {
                        ViewBag.Messages = sendEmail.Error;
                    }
                }
                else
                {
                    ViewBag.Messages = sendEmail.Error;
                }
                return View();
            }
        }
    }
}
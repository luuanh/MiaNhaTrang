using System;
using System.Linq;
using System.Web.Mvc;
using ProjectLibrary.Config;
using ProjectLibrary.Database;

namespace TeamplateHotel.Controllers
{
    public class SendContactController : Controller
    {
        [HttpPost]
        public ActionResult SubmitContact(Contact model)
        {
            model.CreateDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                using (var db = new MyDbDataContext())
                {
                    db.Contacts.InsertOnSubmit(model);
                    db.SubmitChanges();

                    SendEmail sendEmail =
                        db.SendEmails.FirstOrDefault(
                            a => a.Type == TypeSendEmail.Contact && a.LanguageID == Request.Cookies["LanguageID"].Value);
                    Hotel hotel = CommentController.DetailHotel(Request.Cookies["LanguageID"].Value);

                    sendEmail.Title = sendEmail.Title.Replace("{NameHotel}", hotel.Name);
                    string content = sendEmail.Content;
                    content = content.Replace("{Gender}", model.Gender);
                    content = content.Replace("{FullName}", model.FullName);
                    content = content.Replace("{Tel}", model.Tel);
                    content = content.Replace("{Email}", model.Email);
                    content = content.Replace("{Country}", model.Country);
                    content = content.Replace("{Request}", model.Request);

                    content = content.Replace("{NameHotel}", hotel.Name);
                    content = content.Replace("{TelHotel}", hotel.Tel);
                    content = content.Replace("{EmailHotel}", hotel.Email);
                    content = content.Replace("{AddressHotel}", hotel.Address);
                    content = content.Replace("{Website}", hotel.Website);

                    MailHelper.SendMail(model.Email, sendEmail.Title, content);
                    MailHelper.SendMail(hotel.Email, hotel.Name + " Contact of " + model.FullName, content);
                    return Redirect("/Contact/Messages?status=success");
                }
            }
            return Redirect("/Contact/Messages?status=error");
        }

        [HttpGet]
        public ActionResult Messages()
        {
            using (var db = new MyDbDataContext())
            {
                SendEmail sendEmail =
                       db.SendEmails.FirstOrDefault(
                           a => a.Type == TypeSendEmail.Contact && a.LanguageID == Request.Cookies["LanguageID"].Value);

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
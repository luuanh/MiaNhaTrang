using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ProjectLibrary.Database;

namespace TeamplateHotel.Areas.Administrator.Controllers
{
    public class ContactController : BaseController
    {
        //
        // GET: /Administrator/Contact/
        public ActionResult Index()
        {
            ViewBag.Messages = CommentController.Messages(TempData["Messages"]);
            ViewBag.Title = "Trang liên hệ";
            return View();
        }

        [HttpPost]
        public JsonResult List(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                var db = new MyDbDataContext();
                List<Contact> listContact = db.Contacts.ToList();

                var records = listContact.Select(a => new
                {
                    a.ID,
                    CreateDate = a.CreateDate.ToShortDateString(),
                    a.FullName,
                    a.Tel,
                    a.Email,
                    a.Country
                }).Skip(jtStartIndex).Take(jtPageSize).ToList();
                //Return result to jTable
                return Json(new {Result = "OK", Records = records, TotalRecordCount = listContact.Count()});
            }
            catch (Exception ex)
            {
                return Json(new {Result = "ERROR", ex.Message});
            }
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            var db = new MyDbDataContext();
            Contact del = db.Contacts.FirstOrDefault(a => a.ID == id);
            if (del == null)
            {
                return Json(new {Result = "ERROR", Message = "Liên hệ không tồn tại"});
            }
            db.Contacts.DeleteOnSubmit(del);
            db.SubmitChanges();
            return Json(new {Result = "OK", Message = "Xóa liên hệ thành công"});
        }

        [HttpGet]
        public ActionResult Detail(int Id)
        {
            var db = new MyDbDataContext();
            Contact detail = db.Contacts.FirstOrDefault(a => a.ID == Id);
            if (detail == null)
            {
                TempData["Messages"] = "Liên hệ không tồn tại";
                return RedirectToAction("Index");
            }
            return View("Detail", detail);
        }
    }
}
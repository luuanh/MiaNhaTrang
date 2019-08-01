using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectLibrary.Database;
using ProjectLibrary.Security;
using TeamplateHotel.Areas.Administrator.EntityModel;

namespace TeamplateHotel.Areas.Administrator.Controllers
{
    public class LanguageController : BaseController
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Trang ngôn ngữ";
            return View();
        }

        public ActionResult Select(string id)
        {
            using (var db = new MyDbDataContext())
            {
                Language lang = db.Languages.FirstOrDefault(b => b.ID == id);
                if (lang != null)
                {
                    HttpCookie langCookie = Request.Cookies["lang_client"];
                    langCookie.Value = lang.ID;
                    langCookie.Expires = DateTime.Now.AddDays(30);
                    HttpContext.Response.Cookies.Add(langCookie);
                    TempData["Messages"] = "Thay đổi ngôn ngữ thành công";
                }
                else
                {
                    TempData["Messages"] = "Đổi ngôn ngữ không thành công, ngôn ngữ không tồn tại";
                    ViewBag.Messages = CommentController.Messages(TempData["Messages"]);
                }
                return RedirectToAction("Index", "ControlPanel");
            }
        }
        [HttpPost]
        public JsonResult List(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                using (var db = new MyDbDataContext())
                {
                    List<ELanguage> records =
                        db.Languages.Select(
                            a =>
                                new ELanguage
                                {
                                    Icon = a.Icon,
                                    ID = a.ID,
                                    Default = a.Default,
                                    Name = a.Name
                                }).ToList();
                    //Return result to jTable
                    return Json(new {Result = "OK", Records = records, TotalRecordCount = records.Count});
                }
            }
            catch (Exception ex)
            {
                return Json(new {Result = "OK", message = ex.Message});
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult Create(ELanguage model)
        {
            using (var db = new MyDbDataContext())
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        var insert = new Language
                        {
                            ID = model.ID,
                            Name = model.Name,
                            Icon = model.Icon,
                            Default = model.Default
                        };

                        db.Languages.InsertOnSubmit(insert);
                        db.SubmitChanges();
                        string message = "Thêm ngôn ngữ thành công";
                        return Json(new {Result = "OK", Message = message, Record = model});
                    }
                    catch (Exception exception)
                    {
                        return Json(new {Result = "OK", Message = "Error: " + exception.Message});
                    }
                }

                return
                    Json(
                        new
                        {
                            Result = " OK",
                            Errors = ModelState.Errors(),
                            Message = "Dữ liệu đầu vào không đúng định dạng"
                        }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult Edit(ELanguage model)
        {
            using (var db = new MyDbDataContext())
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        Language edit = db.Languages.FirstOrDefault(b => b.ID == model.ID);
                        if (edit != null)
                        {
                            edit.ID = model.ID;
                            edit.Name = model.Name;
                            edit.Icon = model.Icon;
                            edit.Default = model.Default;
                            db.SubmitChanges();

                            string message = "Sửa ngôn ngữ thành công";
                            return Json(new {Result = "OK", Message = message});
                        }
                        return Json(new {Result = "ERROR", Message = "Ngôn ngữ không tồn tại"});
                    }
                    catch (Exception exception)
                    {
                        return Json(new {Result = "OK", Message = "Error: " + exception.Message});
                    }
                }
                return
                    Json(
                        new
                        {
                            Result = " OK",
                            Errors = ModelState.Errors(),
                            Message = "Dữ liệu đầu vào không đúng định dạng"
                        }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult Delete(string id)
        {
            try
            {
                using (var db = new MyDbDataContext())
                {
                    Language del = db.Languages.FirstOrDefault(c => c.ID == id);
                    if (del != null)
                    {
                        db.Languages.DeleteOnSubmit(del);
                        db.SubmitChanges();
                        return Json(new {Result = "OK", Message = "Xóa ngôn ngữ thành công"});
                    }
                    return Json(new {Result = "ERROR", Message = "Ngôn ngữ không tồn tại"});
                }
            }
            catch (Exception ex)
            {
                return Json(new {Result = "OK", Message = "Error: " + ex.Message});
            }
        }
    }
}
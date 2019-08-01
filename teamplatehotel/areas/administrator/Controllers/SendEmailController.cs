using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using ProjectLibrary.Config;
using ProjectLibrary.Database;
using TeamplateHotel.Areas.Administrator.EntityModel;

namespace TeamplateHotel.Areas.Administrator.Controllers
{
    public class SendEmailController : BaseController
    {
        // GET: /Administrator/SendEmail/
        public ActionResult Index()
        {
            ViewBag.Messages = CommentController.Messages(TempData["Messages"]);
            ViewBag.Title = "Cấu hình email thông báo";
            return View();
        }

        [HttpPost]
        public JsonResult List(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                using (var db = new MyDbDataContext())
                {
                    var listSendEmail =
                        db.SendEmails.Where(a => a.LanguageID == Request.Cookies["lang_client"].Value).Select(a => new
                        {
                            a.ID,
                            a.Title
                        }).ToList();
                    //Return result to jTable
                    return Json(new {Result = "OK", Records = listSendEmail, TotalRecordCount = listSendEmail.Count});
                }
            }
            catch (Exception ex)
            {
                return Json(new {Result = "ERROR", ex.Message});
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            LoadData();
            ViewBag.Title = "Thêm bài viết";
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(ESendEmail model)
        {
            using (var db = new MyDbDataContext())
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        if (
                            db.SendEmails.Any(
                                a => a.LanguageID == Request.Cookies["lang_client"].Value && a.Type == model.Type))
                        {
                            TempData["Messages"] = "Cấu hình thông báo email này đã tồn tại";
                            return RedirectToAction("Index");
                        }
                        var sendEmail = new SendEmail
                        {
                            Title = model.Title,
                            Type = model.Type,
                            LanguageID = Request.Cookies["lang_client"].Value,
                            Content = model.Content,
                            ContentDefault = model.Content,
                            Success = model.Success,
                            Error = model.Error
                        };

                        db.SendEmails.InsertOnSubmit(sendEmail);
                        db.SubmitChanges();

                        TempData["Messages"] = "Thêm email thông báo thành công";
                        return RedirectToAction("Index");
                    }
                    catch (Exception exception)
                    {
                        LoadData();
                        ViewBag.Messages = "Error: " + exception.Message;
                        return View();
                    }
                }
                LoadData();
                return View();
            }
        }

        [HttpGet]
        public ActionResult Update(int id)
        {
            ViewBag.Title = "Cập nhật email thông báo";
            using (var db = new MyDbDataContext())
            {
                SendEmail sendEmail = db.SendEmails.FirstOrDefault(a => a.ID == id);

                if (sendEmail == null)
                {
                    TempData["Messages"] = "Email thông báo không tồn tại";
                    return RedirectToAction("Index");
                }

                var eSendEmail = new ESendEmail
                {
                    ID = sendEmail.ID,
                    Type = sendEmail.Type,
                    Title = sendEmail.Title,
                    Content = sendEmail.Content,
                    Success = sendEmail.Success,
                    Error = sendEmail.Error
                };
                return View(eSendEmail);
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Update(ESendEmail model)
        {
            using (var db = new MyDbDataContext())
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        SendEmail sendEmail = db.SendEmails.FirstOrDefault(b => b.ID == model.ID);
                        if (sendEmail != null)
                        {
                            sendEmail.Title = model.Title;
                            sendEmail.Content = model.Content;
                            sendEmail.Success = model.Success;
                            sendEmail.Error = model.Error;

                            db.SubmitChanges();
                            TempData["Messages"] = "Cập nhật email thông báo thành công";
                            return RedirectToAction("Index");
                        }
                    }
                    catch (Exception exception)
                    {
                        ViewBag.Messages = "Lỗi: " + exception.Message;
                        return View();
                    }
                }
                return View(model);
            }
        }

        public void LoadData()
        {
            var listType = new List<SelectListItem>();
            listType.Add(new SelectListItem
            {
                Text = "Chọn kiểu thông báo",
                Value = "",
            });
            listType.AddRange(TypeSendEmail.ListTypeSendEmail().Select(a => new SelectListItem
            {
                Text = a.Text,
                Value = a.Value.ToString(CultureInfo.InvariantCulture)
            }).ToList());
            ViewBag.Types = listType;
        }
    }
}
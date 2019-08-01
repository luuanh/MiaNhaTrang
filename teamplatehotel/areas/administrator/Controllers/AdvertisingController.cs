using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ProjectLibrary.Database;
using ProjectLibrary.Security;
using TeamplateHotel.Areas.Administrator.EntityModel;

namespace TeamplateHotel.Areas.Administrator.Controllers
{
    public class AdvertisingController : BaseController
    {
        // GET: /Administrator/Advertising/
        public ActionResult Index()
        {
            ViewBag.Messages = CommentController.Messages(TempData["Messages"]);
            ViewBag.Title = "Trang quảng cáo";
            return View();
        }

        [HttpPost]
        public ActionResult UpdateIndex()
        {
            using (var db = new MyDbDataContext())
            {
                List<Advertising> records =
                    db.Advertisings.Where(r => r.LanguageID == Request.Cookies["lang_client"].Value).ToList();
                foreach (Advertising record in records)
                {
                    string itemAdv = Request.Params[string.Format("Sort[{0}].Index", record.ID)];
                    int index;
                    int.TryParse(itemAdv, out index);
                    record.Index = index;
                    db.SubmitChanges();
                }
                TempData["Messages"] = "Sắp xếp thành công";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public JsonResult List(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                using (var db = new MyDbDataContext())
                {
                    List<EAdvertising> listAdv =
                        db.Advertisings.Where(a => a.LanguageID == Request.Cookies["lang_client"].Value).Select(a => new EAdvertising
                        {
                            ID = a.ID,
                            Title = a.Title,
                            Url = a.Url,
                            Image = a.Image,
                            Target = a.Target,
                            Index = a.Index,
                            Status = a.Status
                        }).OrderBy(a=>a.Index).ToList();
                    //Return result to jTable
                    return Json(new {Result = "OK", Records = listAdv, TotalRecordCount = listAdv.Count});
                }
            }
            catch (Exception ex)
            {
                return Json(new {Result = "ERROR", ex.Message});
            }
        }

        [HttpPost]
        public JsonResult Create(EAdvertising model)
        {
            using (var db = new MyDbDataContext())
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        var adv = new Advertising
                        {
                            Title = model.Title,
                            LanguageID = Request.Cookies["lang_client"].Value,
                            Url = model.Url,
                            Index = 0,
                            Image = model.Image,
                            Target = model.Target,
                            Status = model.Status,
                        };
                        db.Advertisings.InsertOnSubmit(adv);
                        db.SubmitChanges();

                        string message = "Thêm quảng cáo thành công";
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
        public JsonResult Update(EAdvertising model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (var db = new MyDbDataContext())
                    {
                        Advertising advertising = db.Advertisings.FirstOrDefault(b => b.ID == model.ID);
                        if (advertising != null)
                        {
                            advertising.Title = model.Title;
                            advertising.LanguageID = Request.Cookies["lang_client"].Value;
                            advertising.Url = model.Url;
                            advertising.Image = model.Image;
                            advertising.Target = model.Target;
                            advertising.Status = model.Status;

                            db.SubmitChanges();
                            string message = "Sửa quảng cáo thành công";
                            model.Index = advertising.Index;
                            return Json(new {Result = "OK", Message = message, Record = model});
                        }
                        else
                        {
                            return Json(new {Result = "ERROR", Message = "Quảng cáo này không tồn tại"});
                        }
                    }
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

        [HttpPost]
        public JsonResult Delete(int id)
        {
            try
            {
                using (var db = new MyDbDataContext())
                {
                    Advertising del = db.Advertisings.FirstOrDefault(c => c.ID == id);
                    if (del != null)
                    {
                        db.Advertisings.DeleteOnSubmit(del);
                        db.SubmitChanges();
                        return Json(new {Result = "OK", Message = "Xóa quảng cáo thành công"});
                    }
                    return Json(new {Result = "ERROR", Message = "Quảng cáo không tồn tại"});
                }
            }
            catch (Exception ex)
            {
                return Json(new {Result = "ERROR", ex.Message});
            }
        }
    }
}
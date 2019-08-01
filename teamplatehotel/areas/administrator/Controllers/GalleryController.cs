using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ProjectLibrary.Config;
using ProjectLibrary.Database;
using TeamplateHotel.Areas.Administrator.EntityModel;

namespace TeamplateHotel.Areas.Administrator.Controllers
{
    public class GalleryController : BaseController
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Trang gallery";
            ViewBag.Messages = CommentController.Messages(TempData["Messages"]);
            return View();
        }

        [HttpPost]
        public JsonResult List(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                using (var db = new MyDbDataContext())
                {
                    List<EGallery> list = db.Galleries.OrderBy(a => a.Index).Select(a=> new EGallery()
                    {
                        ID = a.ID,
                        Title = a.Title,
                        Image = a.LargeImage,
                        Index = a.Index,
                    }).ToList();
                    //Return result to jTable
                    return Json(new {Result = "OK", Records = list, TotalRecordCount = list.Count});
                }
            }
            catch (Exception ex)
            {
                return Json(new {Result = "ERROR", message = ex.Message});
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult Create(EGallery model)
        {
            using (var db = new MyDbDataContext())
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        var insert = new Gallery
                        {
                            Title = model.Title,
                            Index = 0,
                            LargeImage = model.Image,
                            SmallImage = ReturnSmallImage.GetImageSmall(model.Image),

                        };

                        db.Galleries.InsertOnSubmit(insert);
                        db.SubmitChanges();
                        string message = "Thêm gallery thành công";
                        return Json(new {Result = "OK", Message = message, Record = model});
                    }
                    catch (Exception exception)
                    {
                        return Json(new {Result = "Error", Message = "Error: " + exception.Message});
                    }
                }
                return
                    Json(
                        new
                        {
                            Result = " Error",
                            Errors = ModelState.Errors(),
                            Message = "Dữ liệu đầu vào không đúng định dang"
                        }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult Edit(EGallery model)
        {
            using (var db = new MyDbDataContext())
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        Gallery edit = db.Galleries.FirstOrDefault(b => b.ID == model.ID);
                        string imageSmall = "/Files/_thumbs" + model.Image.Substring(6, model.Image.Length - 6);
                        if (edit != null)
                        {
                            edit.Title = model.Title;
                            edit.LargeImage = model.Image;
                            edit.SmallImage = imageSmall;
                            db.SubmitChanges();

                            string message = "Sửa gallery thành công";
                            return Json(new {Result = "OK", Message = message, Record = model});
                        }
                        return Json(new {Result = "ERROR", Message = "Gallery không tồn tại"});
                    }
                    catch (Exception exception)
                    {
                        return Json(new {Result = "Error", Message = "Error: " + exception.Message});
                    }
                }
                return
                    Json(
                        new
                        {
                            Result = " Error",
                            Errors = ModelState.Errors(),
                            Message = "Dữ liệu đầu vào không đúng định dạng"
                        }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            try
            {
                using (var db = new MyDbDataContext())
                {
                    Gallery del = db.Galleries.FirstOrDefault(c => c.ID == id);
                    if (del != null)
                    {
                        db.Galleries.DeleteOnSubmit(del);
                        db.SubmitChanges();
                        return Json(new {Result = "OK", Message = "Xóa gallery thành công"});
                    }
                    return Json(new {Result = "ERROR", Message = "Gallery không tồn tại"});
                }
            }
            catch (Exception ex)
            {
                return Json(new {Result = "Error", Message = "Error: " + ex.Message});
            }
        }

        [HttpPost]
        public ActionResult UpdateIndex()
        {
            using (var db = new MyDbDataContext())
            {
                List<Gallery> records = db.Galleries.ToList();
                foreach (Gallery record in records)
                {
                    string itemAdv = Request.Params[string.Format("Sort[{0}].Index", record.ID)];
                    int index;
                    int.TryParse(itemAdv, out index);
                    record.Index = index;
                    db.SubmitChanges();
                }
                TempData["Messages"] = "Sắp xếp gallery thành công";
                return RedirectToAction("Index");
            }
        }
    }
}
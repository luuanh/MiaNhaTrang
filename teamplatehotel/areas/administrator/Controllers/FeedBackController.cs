using ProjectLibrary.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeamplateHotel.Areas.Administrator.EntityModel;
namespace TeamplateHotel.Areas.Administrator.Controllers
{
    public class FeedBackController : Controller
    {
        //
        // GET: /Administrator/FeedBack/

        public ActionResult Index()
        {
            ViewBag.Messages = CommentController.Messages(TempData["Messages"]);
            ViewBag.Title = "FeedBack";
            return View();
        }
        [HttpPost]
        public JsonResult List(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                using (var db = new MyDbDataContext())
                {
                    var listFeedBack = db.FeedBacks.Where(a => a.LanguageID == Request.Cookies["lang_client"].Value && a.Status == true).Skip(jtStartIndex).Take(jtPageSize).ToList();
                          
                   
                    //Return result to jTable
                    return Json(new { Result = "OK", Records = listFeedBack, TotalRecordCount = listFeedBack.Count() });
                }
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", ex.Message });
            }
        }
        [HttpGet]
        public ActionResult Update(int id)
        {
            ViewBag.Title = "Cập nhật Feedback";
            using (var db = new MyDbDataContext())
            {
                FeedBack detailFeedBack = db.FeedBacks.FirstOrDefault(a => a.ID == id);

                if (detailFeedBack == null)
                {
                    TempData["Messages"] = "Bài viết không tồn tại";
                    return RedirectToAction("Index");
                }

                var feedback = new EFeedBack
                {
                    ID = detailFeedBack.ID,
                   LanguageID=detailFeedBack.LanguageID,
                   Home=(bool)detailFeedBack.Home,
                   Status=(bool)detailFeedBack.Status,
                   Content=detailFeedBack.Content,
                };
                //lấy danh sách hình ảnh
               
               
                return View(feedback);
            }
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Update(EFeedBack model)
        {
            using (var db = new MyDbDataContext())
            {
               
                    
                    try
                    {
                        FeedBack feedBack = db.FeedBacks.FirstOrDefault(b => b.ID == model.ID);
                        if (feedBack != null)
                        {
                            feedBack.Home = model.Home;
                            feedBack.Status = model.Status;
                            feedBack.Content = model.Content;
                            db.SubmitChanges();

                           


                            TempData["Messages"] = "Cập nhật bài viết thành công";
                            return RedirectToAction("Index");
                        }
                    }
                    catch (Exception exception)
                    {
                      
                        ViewBag.Messages = "Lỗi: " + exception.Message;
                        return View();
                    }
               
             
                return View(model);
            }
        }
        [HttpGet]
        public ActionResult Create()
        {
          
            ViewBag.Title = "Thêm bài viết FeedBack";
            EFeedBack eFeedBack = new EFeedBack();
            return View(eFeedBack);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(EFeedBack model)
        {
            using (var db = new MyDbDataContext())
            {
                if (ModelState.IsValid)
                {
                   
                    try
                    {
                        var feedBack = new FeedBack
                        {

                            Content = model.Content,
                            Status = model.Status,
                            Home = model.Home,
                            LanguageID = Request.Cookies["lang_client"].Value,
                           
                        };

                        db.FeedBacks.InsertOnSubmit(feedBack);
                        db.SubmitChanges();
                        //Thêm hình ảnh cho dich vu
                      
                        TempData["Messages"] = "Thêm bài viết thành công";
                        return RedirectToAction("Index");
                    }
                    catch (Exception exception)
                    {
                     
                        ViewBag.Messages = "Error: " + exception.Message;
                        return View();
                    }
                }
               
                return View();
            }
        }
        [HttpPost]
        public JsonResult Delete(int id)
        {
            
            using (var db=new MyDbDataContext())
            {
                try
                {
                    FeedBack feedBack = db.FeedBacks.Where(a => a.ID == id).FirstOrDefault();
                    if(feedBack != null)
                    {
                        db.FeedBacks.DeleteOnSubmit(feedBack);
                        db.SubmitChanges();
                        return Json(new { Result = "OK", Message = "Xóa FeedBack thành công" });
                    }
                    return Json(new { Result = "ERROR", Message = "FeedBack này không tồn tại" });

                }
                catch(Exception ex)
                {
                    return Json(new { Result = "ERROR", ex.Message });
                }
               

            }
        }
    }
}

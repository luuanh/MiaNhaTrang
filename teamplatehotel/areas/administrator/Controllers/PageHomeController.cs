using ProjectLibrary.Database;
using ProjectLibrary.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeamplateHotel.Areas.Administrator.ModelShow;

namespace TeamplateHotel.Areas.Administrator.Controllers
{
    public class PageHomeController : Controller
    {
        //
        // GET: /Administrator/PageHome/

        public ActionResult Index()
        {
            ViewBag.Messages = CommentController.Messages(TempData["Messages"]);
            ViewBag.Title = "Trang pagehome";
            return View();
        }
        [HttpPost]
        public JsonResult List(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                using (var db = new MyDbDataContext())
                {
                    var listArticle = db.PageHomes.Where(a => a.LanguageID == Request.Cookies["lang_client"].Value).OrderBy(b => b.Index).Skip(jtStartIndex).Take(jtPageSize).ToList();
                          
                    //List<ShowArticle> records = listArticle.Select(a => new ShowArticle
                    //{
                    //    ID = a.a.ID,
                    //    Title = a.a.Title,
                    //    TitleMenu = a.b.Title,
                    //    Index = a.a.Index,
                    //    Status = a.a.Status,
                    //    Home = a.a.Home,
                    //    Hot = a.a.Hot,
                    //    Top = a.a.Top,
                    //    New = a.a.New
                    //}).OrderBy(a => a.Index).Skip(jtStartIndex).Take(jtPageSize).ToList();
                    //Return result to jTable
                    return Json(new { Result = "OK", Records = listArticle, TotalRecordCount = listArticle.Count() });
                }
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", ex.Message });
            }
        }
        [HttpGet]
        public ActionResult Create()
        {
            //LoadData();
            ViewBag.Title = "Thêm bài viết";
            PageHome pageHome = new PageHome();
            return View(pageHome);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(PageHome model)
        {
            using (var db = new MyDbDataContext())
            {
                if (ModelState.IsValid)
                {
                    if (string.IsNullOrEmpty(model.Alias))
                    {
                        model.Alias = StringHelper.ConvertToAlias(model.Alias);
                    }
                    try
                    {
                        var pageHome = new PageHome
                        {

                            Title = model.Title,
                            Alias = model.Alias,
                            Video = model.Video,
                            Description = model.Description,
                            LanguageID = Request.Cookies["LanguageID"].Value,
                            Index = model.Index,
                            MetaTitle = string.IsNullOrEmpty(model.MetaTitle) ? model.Title : model.MetaTitle,
                            MetaDescription =
                                string.IsNullOrEmpty(model.MetaDescription) ? model.Title : model.Description,
                            Status = model.Status,
                            
                        };

                        db.PageHomes.InsertOnSubmit(pageHome);
                        db.SubmitChanges();
                       
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
        [HttpGet]
        public ActionResult Update(int id)
        {
            ViewBag.Title = "Cập nhật bài viết";
            using (var db = new MyDbDataContext())
            {
                PageHome pageHome = db.PageHomes.FirstOrDefault(a => a.ID == id);

                if (pageHome == null)
                {
                    TempData["Messages"] = "Bài viết không tồn tại";
                    return RedirectToAction("Index");
                }

                var home = new PageHome
                {
                    ID = pageHome.ID,
                  
                    Title = pageHome.Title,
                    Alias = pageHome.Alias,
                    Video = pageHome.Video,
                    Description = pageHome.Description,
                    Index=pageHome.Index,
                    MetaTitle = pageHome.MetaTitle,
                    MetaDescription = pageHome.MetaDescription,
                    Status = pageHome.Status,
                  
                };
              
                return View(home);
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Update(PageHome model)
        {
            using (var db = new MyDbDataContext())
            {
                if (ModelState.IsValid)
                {
                    if (string.IsNullOrEmpty(model.Alias))
                    {
                        model.Alias = StringHelper.ConvertToAlias(model.Alias);
                    }
                    try
                    {
                        PageHome detailPageHome = db.PageHomes.FirstOrDefault(b => b.ID == model.ID);
                        if (detailPageHome != null)
                        {
                           
                            detailPageHome.Title = model.Title;
                            detailPageHome.Alias = model.Alias;
                            detailPageHome.LanguageID = Request.Cookies["LanguageID"].Value;
                            detailPageHome.Video = model.Video;
                            detailPageHome.Description = model.Description;
                            detailPageHome.Index = model.Index;
                            detailPageHome.MetaTitle = string.IsNullOrEmpty(model.MetaTitle) ? model.Title : model.MetaTitle;
                            detailPageHome.MetaDescription = string.IsNullOrEmpty(model.MetaDescription)
                                ? model.Title
                                : model.MetaDescription;
                            detailPageHome.Status = model.Status;
                          

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
                }
              
                return View(model);
            }
        }


        [HttpPost]
        public JsonResult Delete(int id)
        {
            try
            {
                using (var db = new MyDbDataContext())
                {
                    PageHome del = db.PageHomes.FirstOrDefault(c => c.ID == id);
                    if (del != null)
                    {
                        db.PageHomes.DeleteOnSubmit(del);
                        db.SubmitChanges();
                        return Json(new { Result = "OK", Message = "Xóa bài viết thành công" });
                    }
                    return Json(new { Result = "ERROR", Message = "Bài viết không tồn tại" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", ex.Message });
            }
        }
    }
}

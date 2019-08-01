using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ProjectLibrary.Config;
using ProjectLibrary.Database;
using ProjectLibrary.Utility;
using TeamplateHotel.Areas.Administrator.EntityModel;

namespace TeamplateHotel.Areas.Administrator.Controllers
{
    public class ServiceController : BaseController
    {
        // GET: /Administrator/Service/
        public ActionResult Index()
        {
            ViewBag.Messages = CommentController.Messages(TempData["Messages"]);
            ViewBag.Title = "Trang quảng lý dịch vụ";
            return View();
        }

        [HttpPost]
        public ActionResult UpdateIndex()
        {
            using (var db = new MyDbDataContext())
            {
                List<Service> records = db.Services.ToList();
                foreach (Service record in records)
                {
                    string item = Request.Params[string.Format("Sort[{0}].Index", record.ID)];
                    int index;
                    int.TryParse(item, out index);
                    record.Index = index;
                    db.SubmitChanges();
                }
                TempData["Messages"] = "Sắp xếp dịch vụ thành công";
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
                    var list =
                        db.Services.Join(db.Menus.Where(a => a.LanguageID == Request.Cookies["lang_client"].Value),
                            a => a.MenuID, b => b.ID, (a, b) => new
                            {
                                a.ID,
                                a.Title,
                                a.Index,
                                a.Status,
                                a.Home
                            }).OrderBy(a => a.Index).Skip(jtStartIndex).Take(jtPageSize).ToList();
                    return Json(new {Result = "OK", Records = list, TotalRecordCount = list.Count()});
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
            ViewBag.Title = "Thêm dịch vụ";
            var eService = new EService();
            LoadData();
            return View(eService);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(EService model)
        {
            using (var db = new MyDbDataContext())
            {
                if (ModelState.IsValid)
                {
                    if (string.IsNullOrEmpty(model.Alias))
                    {
                        model.Alias = StringHelper.ConvertToAlias(model.Title);
                    }
                    try
                    {
                        var service = new Service
                        {
                            MenuID = model.MenuID,
                            Title = model.Title,
                            Alias = model.Alias,
                            Image = model.Image,
                            Index = 0,
                            Description = model.Description,
                            Content = model.Content,
                            MetaTitle = string.IsNullOrEmpty(model.MetaTitle) ? model.Title : model.MetaTitle,
                            MetaDescription =
                                string.IsNullOrEmpty(model.MetaDescription) ? model.Title : model.MetaDescription,
                            Status = model.Status,
                            Home = model.Home
                        };

                        db.Services.InsertOnSubmit(service);
                        db.SubmitChanges();

                        //Thêm hình ảnh cho dich vu
                        if (model.EGalleryITems != null)
                        {
                            foreach (EGalleryITem itemGallery in model.EGalleryITems)
                            {
                                var serviceGallery = new ServiceGallery
                                {
                                    ImageLarge = itemGallery.Image,
                                    ImageSmall = ReturnSmallImage.GetImageSmall(itemGallery.Image),
                                    ServiceID = service.ID,
                                };
                                db.ServiceGalleries.InsertOnSubmit(serviceGallery);
                            }
                            db.SubmitChanges();
                        }

                        TempData["Messages"] = "Thêm dịch vụ thành công.";
                        return RedirectToAction("Index");
                    }
                    catch (Exception exception)
                    {
                        ViewBag.Messages = "Error: " + exception.Message;
                        LoadData();
                        return View(model);
                    }
                }
                LoadData();
                return View(model);
            }
        }

        [HttpGet]
        public ActionResult Update(int id)
        {
            using (var db = new MyDbDataContext())
            {
                Service service = db.Services.FirstOrDefault(a => a.ID == id);
                if (service == null)
                {
                    TempData["Messages"] = "Dịch vụ không tồn tại";
                    return RedirectToAction("Index");
                }
                ViewBag.Title = "Sửa dịch vụ";
                var eService = new EService
                {
                    ID = service.ID,
                    MenuID = service.MenuID,
                    Title = service.Title,
                    Alias = service.Alias,
                    Image = service.Image,
                    Index = service.Index,
                    Description = service.Description,
                    Content = service.Content,
                    MetaTitle = service.MetaTitle,
                    MetaDescription = service.MetaDescription,
                    Status = service.Status,
                    Home = service.Home
                };
                //lấy danh sách hình ảnh
                eService.EGalleryITems =
                    db.ServiceGalleries.Where(a => a.ServiceID == service.ID).Select(a => new EGalleryITem
                    {
                        Image = a.ImageLarge
                    }).ToList();
                LoadData();
                return View(eService);
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Update(EService model)
        {
            using (var db = new MyDbDataContext())
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        Service service = db.Services.FirstOrDefault(b => b.ID == model.ID);
                        if (service != null)
                        {
                            service.Title = model.Title;
                            service.MenuID = model.MenuID;
                            service.Alias = model.Alias;
                            service.Image = model.Image;
                            service.Description = model.Description;
                            service.Content = model.Content;
                            service.MetaTitle = string.IsNullOrEmpty(model.MetaTitle) ? model.Title : model.MetaTitle;
                            service.MetaDescription = string.IsNullOrEmpty(model.MetaDescription)
                                ? model.Title
                                : model.MetaDescription;
                            service.Status = model.Status;
                            service.Home = model.Home;

                            db.SubmitChanges();

                            //xóa gallery cho phòng
                            db.ServiceGalleries.DeleteAllOnSubmit(
                                db.ServiceGalleries.Where(a => a.ServiceID == service.ID).ToList());
                            //Thêm hình ảnh cho phòng
                            if (model.EGalleryITems != null)
                            {
                                foreach (EGalleryITem itemGallery in model.EGalleryITems)
                                {
                                    var serviceGallery = new ServiceGallery
                                    {
                                        ImageLarge = itemGallery.Image,
                                        ImageSmall = ReturnSmallImage.GetImageSmall(itemGallery.Image),
                                        ServiceID = service.ID,
                                    };
                                    db.ServiceGalleries.InsertOnSubmit(serviceGallery);
                                }
                                db.SubmitChanges();
                            }
                            TempData["Messages"] = "Sửa dịch vụ thành công";
                            return RedirectToAction("Index");
                        }
                    }
                    catch (Exception exception)
                    {
                        ViewBag.Messages = "Error: " + exception.Message;
                        LoadData();
                        return View(model);
                    }
                }
                LoadData();
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
                    Service del = db.Services.FirstOrDefault(c => c.ID == id);
                    if (del != null)
                    {
                        //xóa hết hình ảnh của phòng này
                        db.ServiceGalleries.DeleteAllOnSubmit(
                            db.ServiceGalleries.Where(a => a.ServiceID == del.ID).ToList());

                        db.Services.DeleteOnSubmit(del);
                        db.SubmitChanges();
                        return Json(new {Result = "OK", Message = "Xóa dịch vụ thành công"});
                    }
                    return Json(new {Result = "ERROR", Message = "dịch vụ không tồn tại"});
                }
            }
            catch (Exception ex)
            {
                return Json(new {Result = "ERROR", ex.Message});
            }
        }

        public void LoadData()
        {
            var listMenu = new List<SelectListItem>
            {
                new SelectListItem {Value = "0", Text = "Lựa chọn chuyên mục"}
            };
            var menus = new List<Menu>();

            menus =
                MenuController.GetListMenu(0, Request.Cookies["lang_client"].Value).Where(
                    a =>
                        a.Type == SystemMenuType.Service).ToList();

            foreach (Menu menu in menus)
            {
                string sub = "";
                for (int i = 0; i < menu.Level; i++)
                {
                    sub += "|--";
                }
                menu.Title = sub + menu.Title;
            }

            listMenu.AddRange(menus.OrderBy(a => a.Location).Select(a => new SelectListItem
            {
                Text = a.Title,
                Value = a.ID.ToString()
            }).ToList());
            ViewBag.ListMenu = listMenu;
        }
    }
}
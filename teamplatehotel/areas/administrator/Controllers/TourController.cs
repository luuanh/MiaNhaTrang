using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using ProjectLibrary.Config;
using ProjectLibrary.Database;
using ProjectLibrary.Utility;
using TeamplateHotel.Areas.Administrator.EntityModel;

namespace TeamplateHotel.Areas.Administrator.Controllers
{
    public class TourController : BaseController
    {
        // GET: /Administrator/Tour/
        public ActionResult Index()
        {
            LoadData();
            ViewBag.Messages = CommentController.Messages(TempData["Messages"]);
            ViewBag.Title = "Trang tours";
            return View();
        }

        [HttpPost]
        public ActionResult UpdateIndex()
        {
            using (var db = new MyDbDataContext())
            {
                List<Tour> records = db.Tours.ToList();
                foreach (Tour record in records)
                {
                    string itemTour = Request.Params[string.Format("Sort[{0}].Index", record.ID)];
                    int index;
                    int.TryParse(itemTour, out index);
                    record.Index = index;
                    db.SubmitChanges();
                }
                TempData["Messages"] = "Sắp xếp tour thành công";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public JsonResult List(int menuId = 0, int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                var db = new MyDbDataContext();
                var listTour = menuId == 0
                    ? db.Tours.Join(db.Menus.Where(m => m.LanguageID == Request.Cookies["lang_client"].Value),
                        a => a.MenuID, b => b.ID, (a, b) => new {a, b}).ToList()
                    : db.Tours.Join(
                        db.Menus.Where(m => m.ID == menuId && m.LanguageID == Request.Cookies["lang_client"].Value),
                        a => a.MenuID, b => b.ID, (a, b) => new {a, b}).ToList();

                var records = listTour.Select(a => new
                {
                    a.a.ID,
                    a.a.Title,
                    TitleMenu = a.b.Title,
                    a.a.Index,
                    a.a.Status,
                }).Skip(jtStartIndex).Take(jtPageSize).OrderBy(a => a.Index).ToList();
                //Return result to jTable
                return Json(new {Result = "OK", Records = records, TotalRecordCount = listTour.Count});
            }
            catch (Exception ex)
            {
                return Json(new {Result = "ERROR", ex.Message});
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Title = "Thêm mới tour";
            LoadData();
            var tour = new ETour();
            return View(tour);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(ETour model)
        {
            using (var db = new MyDbDataContext())
            {
                //Kiểm tra xem đã chọn đến chuyên mục con cuối cùng chưa
                if (db.Menus.Any(a => a.ParentID == model.MenuID))
                {
                    ModelState.AddModelError("MenuId", "Phải chọn đến chuyên mục tour con cuối cùng");
                }

                if (ModelState.IsValid)
                {
                    if (string.IsNullOrEmpty(model.Alias))
                    {
                        model.Alias = StringHelper.ConvertToAlias(model.Title);
                    }
                    try
                    {
                        var tour = new Tour
                        {
                            MenuID = model.MenuID,
                            Title = model.Title,
                            Alias = model.Alias,
                            Image = model.Image,
                            Index = 0,
                            Description = model.Description,
                            MetaTitle = string.IsNullOrEmpty(model.MetaTitle) ? model.Title : model.MetaTitle,
                            MetaDescription =
                                string.IsNullOrEmpty(model.MetaDescription) ? model.Title : model.MetaDescription,
                            Status = model.Status,
                            Price = model.Price,
                            NumberDay=model.NumberDay,
                            AddressStart=model.AddressStart,
                            DateStart=model.DateStart,
                        };

                        db.Tours.InsertOnSubmit(tour);
                        db.SubmitChanges();

                        //Thêm hình ảnh cho tour
                        if (model.EGalleryITems != null)
                        {
                            foreach (EGalleryITem itemGallery in model.EGalleryITems)
                            {
                                var gallery = new TourGallery
                                {
                                    LargeImage = itemGallery.Image,
                                    SmallImage = ReturnSmallImage.GetImageSmall(itemGallery.Image),
                                    TourID = tour.ID,
                                };
                                db.TourGalleries.InsertOnSubmit(gallery);
                            }
                            db.SubmitChanges();
                        }
                        //Thêm tabtour
                        if (model.TabTours != null)
                        {
                            foreach (TabTour itemTabTour in model.TabTours)
                            {
                                var tabTour = new TabTour
                                {
                                    TourID = tour.ID,
                                    TitleTab = itemTabTour.TitleTab,
                                    Content = itemTabTour.Content
                                };

                                db.TabTours.InsertOnSubmit(tabTour);
                            }
                            db.SubmitChanges();
                        }


                        TempData["Messages"] = "Thêm mới tour thành công";
                        return RedirectToAction("Index");
                    }
                    catch (Exception exception)
                    {
                        LoadData();
                        ViewBag.Messages = "Error: " + exception.Message;
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
            ViewBag.Title = "Cập nhật tour";
            var db = new MyDbDataContext();
            Tour detailTour = db.Tours.FirstOrDefault(a => a.ID == id);
            if (detailTour == null)
            {
                TempData["Messages"] = "Tour không tồn tại";
                return RedirectToAction("Index");
            }
            LoadData();
            
            var tour = new ETour
            {
                ID = detailTour.ID,
                MenuID = detailTour.MenuID,
                Title = detailTour.Title,
                Alias = detailTour.Alias,
                Image = detailTour.Image,
                Description = detailTour.Description,
                MetaTitle = detailTour.MetaTitle,
                MetaDescription = detailTour.MetaDescription,
                Status = detailTour.Status,
                AddressStart=detailTour.AddressStart,
                DateStart = (DateTime)detailTour.DateStart,

                NumberDay = (int)detailTour.NumberDay,
                Price = (float)detailTour.Price,
            };
            //lấy danh sách hình ảnh
            tour.EGalleryITems = db.TourGalleries.Where(a => a.TourID == detailTour.ID).Select(a => new EGalleryITem
            {
                Image = a.LargeImage
            }).ToList();

            //lấy danh sách tabtour
            List<TabTour> tabTours = db.TabTours.Where(a => a.TourID == detailTour.ID).ToList();
            tour.TabTours = tabTours;

            return View(tour);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Update(ETour model)
        {
            //Kiểm tra xem alias thuộc tour này đã tồn tại chưa
            var db = new MyDbDataContext();

            //Kiểm tra xem đã chọn đến chuyên mục con cuối cùng chưa
            if (db.Menus.Any(a => a.ParentID == model.ID))
            {
                ModelState.AddModelError("MenuId", "Vui lòng chọn đến chuyên mục tour con cuối cùng");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Tour tour = db.Tours.FirstOrDefault(b => b.ID == model.ID);
                    if (tour != null)
                    {
                        tour.MenuID = model.MenuID;
                        tour.Title = model.Title;
                        tour.Alias = model.Alias;
                        tour.Image = model.Image;
                        
                        tour.Description = model.Description;
                        tour.MetaTitle = string.IsNullOrEmpty(model.MetaTitle) ? model.Title : model.MetaTitle;
                        tour.MetaDescription = string.IsNullOrEmpty(model.MetaDescription)
                            ? model.Title
                            : model.MetaDescription;
                        tour.Status = model.Status;
                        tour.DateStart = model.DateStart;
                        tour.AddressStart = model.AddressStart;
                        tour.NumberDay = model.NumberDay;
                        tour.Price = model.Price;
                        db.SubmitChanges();

                        //xóa gallery cho tour
                        db.TourGalleries.DeleteAllOnSubmit(db.TourGalleries.Where(a => a.TourID == tour.ID).ToList());
                        //Thêm hình ảnh cho tour
                        if (model.EGalleryITems != null)
                        {
                            foreach (EGalleryITem itemGallery in model.EGalleryITems)
                            {
                                var gallery = new TourGallery
                                {
                                    LargeImage = itemGallery.Image,
                                    SmallImage = ReturnSmallImage.GetImageSmall(itemGallery.Image),
                                    TourID = tour.ID,
                                };
                                db.TourGalleries.InsertOnSubmit(gallery);
                            }
                            db.SubmitChanges();
                        }
                        //xóa danh sách tabtour
                        db.TabTours.DeleteAllOnSubmit(db.TabTours.Where(a => a.TourID == tour.ID).ToList());

                        //Thêm tabtour
                        if (model.TabTours != null)
                        {
                            foreach (TabTour itemTabTour in model.TabTours)
                            {
                                var tabTour = new TabTour
                                {
                                    TourID = tour.ID,
                                    TitleTab = itemTabTour.TitleTab,
                                    Content = itemTabTour.Content
                                };

                                db.TabTours.InsertOnSubmit(tabTour);
                            }
                            db.SubmitChanges();
                        }

                        TempData["Messages"] = "Sửa tour thành công";
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception exception)
                {
                    LoadData();
                    ViewBag.Messages = "Error: " + exception.Message;
                    return View();
                }
            }
            LoadData();
            return View(model);
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            try
            {
                using (var db = new MyDbDataContext())
                {
                    Tour del = db.Tours.FirstOrDefault(c => c.ID == id);
                    if (del != null)
                    {
                        //xóa hết hình ảnh của tour này
                        db.TourGalleries.DeleteAllOnSubmit(db.TourGalleries.Where(a => a.TourID == del.ID).ToList());
                        //xóa hết tabtour của tour này
                        db.TabTours.DeleteAllOnSubmit(db.TabTours.Where(a => a.TourID == del.ID).ToList());
                        db.Tours.DeleteOnSubmit(del);
                        db.SubmitChanges();
                        return Json(new {Result = "OK", Message = "Xóa tour thành công"});
                    }
                    return Json(new {Result = "ERROR", Message = "Tour này không tồn tại"});
                }
            }
            catch (Exception ex)
            {
                return Json(new {Result = "ERROR", ex.Message});
            }
        }

        public void LoadData()
        {
            var listMenu = new List<SelectListItem>();
            listMenu.Add(new SelectListItem {Value = "0", Text = "---Select a menu---"});
            List<Menu> getListMenu =
                MenuController.GetListMenu(SystemMenuLocation.ListLocationMenu().ToList()[0].LocationId,
                    Request.Cookies["lang_client"].Value)
                    .Where(a => a.Type == SystemMenuType.Tour)
                    .ToList();

            foreach (Menu menu in getListMenu)
            {
                string subTitle = "";
                for (int i = 1; i <= menu.Level; i++)
                {
                    subTitle += "|--";
                }
                menu.Title = subTitle + menu.Title;
            }
            listMenu.AddRange(getListMenu.Select(a => new SelectListItem
            {
                Value =
                    a.ID.ToString(
                        CultureInfo.InvariantCulture),
                Text = a.Title
            }).ToList());
            ViewBag.ListMenu = listMenu;
        }
    }
}
using System;
using System.Linq;
using System.Web.Mvc;
using ProjectLibrary.Database;

namespace TeamplateHotel.Areas.Administrator.Controllers
{
    public class PlugInController : BaseController
    {
        [HttpGet]
        public ActionResult CSS()
        {
            ViewBag.Title = "Chỉnh sửa CSS";
            using (var db = new MyDbDataContext())
            {
                Plugin plugin = db.Plugins.FirstOrDefault();
                return View(plugin);
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CSS(Plugin model)
        {
            using (var db = new MyDbDataContext())
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        Plugin plugin = db.Plugins.FirstOrDefault();
                        plugin.CSS = model.CSS;
                        db.SubmitChanges();
                        ViewBag.Messages = "Chỉnh sửa CSS thành công";
                        return RedirectToAction("CSS");
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

        [HttpGet]
        public ActionResult GoogleAnalytics()
        {
            ViewBag.Title = "Thêm Google analytics, Live chat";
            using (var db = new MyDbDataContext())
            {
                Plugin plugin = db.Plugins.FirstOrDefault();
                return View(plugin);
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult GoogleAnalytics(Plugin model)
        {
            using (var db = new MyDbDataContext())
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        Plugin plugin = db.Plugins.FirstOrDefault();
                        plugin.JS = model.JS;
                        db.SubmitChanges();
                        ViewBag.Messages = "Chỉnh sửa Google Analytics & Live chat thành công";
                        return RedirectToAction("GoogleAnalytics");
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

        [HttpGet]
        public ActionResult SideBar()
        {
            ViewBag.Title = "Thêm sideBar";
            using (var db = new MyDbDataContext())
            {
                Plugin plugin = db.Plugins.FirstOrDefault();
                return View(plugin);
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SideBar(Plugin model)
        {
            using (var db = new MyDbDataContext())
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        Plugin plugin = db.Plugins.FirstOrDefault();
                        plugin.SideBar = model.SideBar;
                        db.SubmitChanges();
                        ViewBag.Messages = "Chỉnh sửa SideBar thành công";
                        return RedirectToAction("SideBar");
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
    }
}
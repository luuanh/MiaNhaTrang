using System;
using System.Linq;
using System.Web.Mvc;
using ProjectLibrary.Database;
using ProjectLibrary.Security;
using TeamplateHotel.Areas.Administrator.EntityModel;

namespace TeamplateHotel.Areas.Administrator.Controllers
{
    public class ConfigEmailController : BaseController
    {
        //
        // GET: /Administrator/ConfigWebsites/
        public ActionResult Index()
        {
            if (TempData["Messages"] != null)
            {
                ViewBag.Messages = TempData["Messages"];
            }
            ViewBag.Title = "Cấu hình Email";
            return View();
        }
        [HttpPost]
        public JsonResult List(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                using (var db = new MyDbDataContext())
                {
                    var list = db.ConfigWebsites.ToList();
                    //Return result to jTable
                    return Json(new { Result = "OK", Records = list, TotalRecordCount = list.Count });
                }
                
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult Create(EConfigEmail model)
        {
            using (var db = new MyDbDataContext())
            {
                if(db.ConfigWebsites.Any())
                {
                    return Json(new { Result = "ERROR", Message = "Website chỉ được phép tồn tại 1 bản cấu hình" });
                }
                if (ModelState.IsValid)
                {
                    try
                    {
                        ConfigWebsite configWebsite = new ConfigWebsite
                        {
                            Host = model.Host,
                            Port = model.Port,
                            Email = model.Email,
                            Password = CryptorEngine.Encrypt(model.Password, true),
                        };
                        db.ConfigWebsites.InsertOnSubmit(configWebsite);
                        db.SubmitChanges();

                        model.ID = configWebsite.ID;
                        return Json(new { Result = "OK", Message = "Thêm thành công", Record = model });
                    }
                    catch (Exception exception)
                    {
                        return Json(new { Result = "ERROR", Message = "Fail. Error:" + exception.Message });
                    }
                }
                return Json(new { Result = "ERROR", Errors = ModelState.Errors(), Message = "Dữ liệu đầu vào không đúng định dạng" });
            }
        }

        [HttpPost]
        public JsonResult Edit(EConfigEmail model)
        {
            using (var db = new MyDbDataContext())
            {
                if (ModelState.IsValid)
                {

                    try
                    {
                        var edit = db.ConfigWebsites.FirstOrDefault(b => b.ID == model.ID);
                        if (edit != null)
                        {
                            edit.Port = model.Port;
                            edit.Email = model.Email;
                            edit.Password = CryptorEngine.Encrypt(model.Password, true);
                            edit.Host = model.Host;

                            db.SubmitChanges();
                            return Json(new {Result = "OK", Message = "Sửa thành công", Record = model});
                        }
                        else
                        {
                            return Json(new {Result = "ERROR", Message = "Bản ghi không tồn tại"});
                        }
                    }
                    catch (Exception exception)
                    {
                        return Json(new { Result = "ERROR", Message = "Fail. Error:" + exception.Message });
                    }
                }
                return Json(new { Result = "ERROR", Errors = ModelState.Errors(), Message = "Dữ liệu đầu vào không đúng định dạng" });

            }
        }


    }
}

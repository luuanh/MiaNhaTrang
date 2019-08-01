using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ProjectLibrary.Database;
using ProjectLibrary.Security;
using TeamplateHotel.Areas.Administrator.EntityModel;

namespace TeamplateHotel.Areas.Administrator.Controllers
{
    public class UserController : BaseController
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Danh sách người dùng";
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
                    List<EUser> records = db.Users.Select(a => new EUser
                    {
                        ID = a.ID,
                        UserName = a.UserName,
                        Password = a.Password,
                        FullName = a.FullName,
                        Email = a.Email,
                        Status = a.Status,
                    }).ToList();

                    //Return result to jTable
                    return Json(new {Result = "OK", Records = records, TotalRecordCount = records.Count});
                }

            }

            catch (Exception ex)
            {
                return Json(new {Result = "ERROR", message = ex.Message});
            }
        }

        [HttpPost]
        public ActionResult Create(EUser model)
        {
            using (var db = new MyDbDataContext())
            {
                User checkUserName = db.Users.FirstOrDefault(a => a.UserName == model.UserName);
                if (checkUserName != null)
                {
                    ModelState.AddModelError("UserName",
                        "Tên đăng nhập này đã tồn tại");
                }
                User checkEmail = db.Users.FirstOrDefault(a => a.Email == model.Email);
                if (checkEmail != null)
                {
                    ModelState.AddModelError("Email",
                        "Email này đã tồn tại, xin vui lòng chọn email khác");
                }
                if (ModelState.IsValid)
                {
                    try
                    {
                        var insert = new User
                        {
                            UserName = model.UserName,
                            Password = Password.HashPassword(model.Password),
                            FullName = model.FullName,
                            Email = model.Email,
                            PasswordOld = CryptorEngine.Encrypt(model.Password, true),
                            Status = model.Status
                        };

                        db.Users.InsertOnSubmit(insert);
                        db.SubmitChanges();
                        model.ID = insert.ID;

                        return Json(new {Result = "OK", Message = "Thêm người dùng thành công", Record = model});
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
                            Result = "Error",
                            Errors = ModelState.Errors(),
                            Message = "Dữ liệu đầu vào không đúng định dạng"
                        });
            }
        }

        [HttpPost]
        public ActionResult Edit(EUser model)
        {
            using (var db = new MyDbDataContext())
            {
                if (ModelState.IsValid)
                {
                    //check username đã tồn tại chưa
                    IQueryable<User> checkUserName = db.Users.Where(a => a.UserName == model.UserName);
                    if (checkUserName.Count() > 1)
                    {
                        ModelState.AddModelError("UserName",
                            "Tên đăng nhập đã tồn tại trong hệ thống");
                    }

                    //check email đã tồn tại chưa
                    IQueryable<User> checkEmail = db.Users.Where(a => a.Email == model.Email);
                    if (checkEmail.Count() > 1)
                    {
                        ModelState.AddModelError("Email",
                            "Email đã tồn tại trong hệ thống");
                    }

                    User edit = db.Users.FirstOrDefault(b => b.ID == model.ID);
                    if (edit == null)
                    {
                        TempData["Messages"] = "Người dùng không tồn tại trong hệ thống";
                        return RedirectToAction("Index");
                    }
                    if (string.IsNullOrEmpty(model.Password))
                    {
                        model.Password = edit.Password;
                        ModelState.Remove("Password");
                    }

                    try
                    {
                        edit.UserName = model.UserName;
                        edit.FullName = model.FullName;
                        edit.Email = model.Email;
                        edit.Status = model.Status;

                        if (model.Password != edit.Password)
                        {
                            edit.Password = Password.HashPassword(model.Password);
                            edit.PasswordOld = CryptorEngine.Encrypt(model.Password, true);
                        }
                        db.SubmitChanges();

                        return Json(new {Result = "OK", Message = "Insert successful"});
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
                            Result = "Error",
                            Errors = ModelState.Errors(),
                            Message = "Dữ liệu đầu vào không đúng định dạng"
                        });
            }
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            try
            {
                using (var db = new MyDbDataContext())
                {
                    User del = db.Users.FirstOrDefault(c => c.ID == id);
                    if (del != null)
                    {
                        //Xóa người dùng này
                        db.Users.DeleteOnSubmit(del);
                        db.SubmitChanges();
                        return Json(new {Result = "OK", Message = "Xóa người dùng thành công"});
                    }
                    return Json(new {Result = "ERROR", Message = "Người dùng không tồn tại trong hệ thống"});
                }
            }
            catch (Exception ex)
            {
                return Json(new {Result = "Error", Message = "Error: " + ex.Message});
            }
        }
    }
}
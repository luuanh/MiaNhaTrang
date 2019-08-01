using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ProjectLibrary.Database;

namespace TeamplateHotel.Areas.Administrator.Controllers
{
    public class BookRoomController : BaseController
    {
        // GET: /Administrator/BookRoom/
        public ActionResult Index()
        {
            ViewBag.Messages = CommentController.Messages(TempData["Messages"]);
            ViewBag.Title = "Trang đặt phòng";
            ViewBag.BookingOnline = true;
            return View();
        }

        [HttpPost]
        public JsonResult List(string checkin ="", string checkout ="", string bookdate="", int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                using (var db = new MyDbDataContext())
                {
                    List<BookRoom> listBookRoom = db.BookRooms.OrderByDescending(a => a.ID).ToList();

                    if (string.IsNullOrEmpty(checkin) == false)
                    {
                        DateTime check_in = Convert.ToDateTime(checkin);
                        listBookRoom =
                            listBookRoom.Where(a => a.CheckIn.Date >= check_in.Date).OrderBy(a => a.CheckIn).ToList();
                    }
                    if (string.IsNullOrEmpty(checkout) == false)
                    {
                        DateTime check_out = Convert.ToDateTime(checkout);
                        listBookRoom =
                            listBookRoom.Where(a => a.CheckOut.Date >= check_out.Date).OrderBy(a => a.CheckOut).ToList();
                    }
                    if (string.IsNullOrEmpty(bookdate) == false)
                    {
                        DateTime book_date = Convert.ToDateTime(bookdate);
                        listBookRoom =
                            listBookRoom.Where(a => a.DateBook.Date >= book_date.Date).OrderBy(a => a.DateBook).ToList();
                    }
                    var records = listBookRoom.Select(a => new
                    {
                        a.ID,
                        a.Code,
                        CheckIn = a.CheckIn.ToString("MMMM, dd, yyyy"),
                        CheckOut = a.CheckOut.ToString("MMMM, dd, yyyy"),
                        a.Adult,
                        a.Child,
                        a.InfoBooking,
                        a.Gender,
                        FullName = a.Gender + ". " + a.FullName, 
                        a.Country,
                        DateBook = a.DateBook.ToString("MMMM, dd, yyyy"),
                    }).Skip(jtStartIndex).Take(jtPageSize).ToList();
                    //Return result to jTable
                    return Json(new {Result = "OK", Records = records, TotalRecordCount = listBookRoom.Count()});
                }
            }
            catch (Exception ex)
            {
                return Json(new {Result = "ERROR", ex.Message});
            }
        }

        [HttpGet]
        public ActionResult Detail(int Id)
        {
            using (var db = new MyDbDataContext())
            {
                BookRoom detailBook = db.BookRooms.FirstOrDefault(a => a.ID == Id);
                if (detailBook == null)
                {
                    return RedirectToAction("Index");
                }
                return View("Detail", detailBook);
            }
        }

        [HttpPost]
        public JsonResult Delete(int Id)
        {
            var db = new MyDbDataContext();
            BookRoom del = db.BookRooms.FirstOrDefault(a => a.ID == Id);
            if (del == null)
            {
                return Json(new {Result = "ERROR", Message = "Đặt phòng không tồn tại."});
            }
            db.BookRooms.DeleteOnSubmit(del);
            db.SubmitChanges();
            return Json(new {Result = "OK", Message = "Xóa thành công."});
        }
    }
}
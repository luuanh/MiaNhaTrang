using System.Net;
using System.Web.Mvc;
using ProjectLibrary.Security;


namespace TeamplateHotel.Areas.Administrator.Controllers
{
    public class ControlPanelController : BaseController
    {
        //
        // GET: /Administrator/ControlPanel/
        public ActionResult Index()
        {
            ViewBag.Title = "Trang quản trị";
            ViewBag.Messages = CommentController.Messages(TempData["Messages"]);
            return View();
        }

    }
}

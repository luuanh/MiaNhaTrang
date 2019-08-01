using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using ProjectLibrary.Database;

namespace TeamplateHotel.Controllers
{
    public class BasicController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Request.Cookies["LanguageID"] == null)
            {
                using (var db = new MyDbDataContext())
                {
                    Language language = db.Languages.FirstOrDefault(a => a.Default);
                    if (language == null)
                    {
                        language = db.Languages.FirstOrDefault();
                    }

                    if(language != null)
                        {
                            HttpCookie langCookie = new HttpCookie("LanguageID");
                            langCookie.Value = language.ID;
                            langCookie.Expires = DateTime.Now.AddDays(10);
                            filterContext.RequestContext.HttpContext.Response.Cookies.Add(langCookie);
                        }
                    else
                    {
                        filterContext.Result =
                                    new RedirectToRouteResult(
                                        new RouteValueDictionary(new { controller = "Home", action = "404" }));
                    }
                }
            }
        }
    }
}
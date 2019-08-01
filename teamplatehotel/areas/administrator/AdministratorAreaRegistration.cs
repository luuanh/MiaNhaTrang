using System.Web.Mvc;

namespace TeamplateHotel.Areas.Administrator
{
    public class AdministratorAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Administrator";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Administrator_default",
                "admin/{controller}/{action}/{id}/{aliasMenu}",
                new { controller = "Login", action = "Index", id = UrlParameter.Optional, aliasMenu=UrlParameter.Optional }
            );
        }
    }
}

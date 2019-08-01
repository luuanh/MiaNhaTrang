using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Threading;

namespace TeamplateHotel.Handler
{
    public class GetLanguage
    {
        public static string Language(string language,string param)
        {
            string english = language;
            var info = new CultureInfo(english);
            Thread.CurrentThread.CurrentCulture = info;
            Thread.CurrentThread.CurrentUICulture = info;
            var rs = new ResourceManager("Resources.Resource", Assembly.Load("App_GlobalResources"));
            return rs.GetString(param);
        }
    }
}
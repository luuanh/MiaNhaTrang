using System.Collections.Generic;

namespace ProjectLibrary.Config
{
    public class SystemMenuLocation
    {
        public const int MainMenu = 1;
        public const int SecondMenu = 2;

        public int LocationId { get; set; }
        public string AliasMenu { get; set; }
        public string TitleMenu { get; set; }
        

        public static  List<SystemMenuLocation> ListLocationMenu()
        {
            var listLocationMenu = new List<SystemMenuLocation>
                                       {
                                           new SystemMenuLocation
                                               {
                                                   LocationId = MainMenu,
                                                   TitleMenu = "Chuyên mục chính",
                                                   AliasMenu = "MainMenu"
                                               },
                                           new SystemMenuLocation
                                               {
                                                   LocationId = SecondMenu,
                                                   TitleMenu = "Chuyên mục phụ",
                                                   AliasMenu = "SecondMenu"
                                               },
                                       };

            return listLocationMenu;
        }
    }

}

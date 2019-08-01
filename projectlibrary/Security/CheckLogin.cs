using System.Linq;
using ProjectLibrary.Database;

namespace ProjectLibrary.Security
{
    public class CheckLogin
    {
        public static int CheckUserLogin(string username, string password, string languageKey)
        {
            using (var db = new MyDbDataContext())
            {
                CurrentSession.ClearAll();
                string pashPassWord = Password.HashPassword(password);
                User checkUser = db.Users.FirstOrDefault(u => u.UserName == username && u.Password == pashPassWord);
                if (checkUser != null)
                {
                    if (checkUser.Status)
                    {
                        //Đăng nhập thành công
                        return 1;
                    }
                    return 2;
                }
            }
            return 3;
        }
    }
}
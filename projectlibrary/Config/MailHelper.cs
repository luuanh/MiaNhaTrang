using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using ProjectLibrary.Database;
using ProjectLibrary.Security;

namespace ProjectLibrary.Config
{
    public class MailHelper
    {
        public static bool SendMail(string toEmail, string subject, string content)
        {
            try
            {
                using (var db = new MyDbDataContext())
                {
                    ConfigWebsite configWebsite = db.ConfigWebsites.FirstOrDefault() ?? new ConfigWebsite();
                    Hotel hotel = db.Hotels.FirstOrDefault() ?? new Hotel();
                    string host = configWebsite.Host;
                    int port = configWebsite.Port;
                    string email = configWebsite.Email;
                    string password = CryptorEngine.Decrypt(configWebsite.Password, true);

                    var smtpClient = new SmtpClient(host, port)
                    {
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(email, password),
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        EnableSsl = true,
                        Timeout = 100000
                    };

                    var mail = new MailMessage
                    {
                        Body = content,
                        Subject = subject,
                        From = new MailAddress(hotel.Email, hotel.Name)
                    };

                    mail.To.Add(new MailAddress(toEmail));
                    mail.BodyEncoding = Encoding.UTF8;
                    mail.IsBodyHtml = true;
                    mail.Priority = MailPriority.High;

                    smtpClient.Send(mail);

                    return true;
                }
            }
            catch (SmtpException smex)
            {
                return false;
            }
        }
    }
}
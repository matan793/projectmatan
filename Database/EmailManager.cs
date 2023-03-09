using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    internal class EmailManager
    {
        public static string GetCode(int length)
        {

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            StringBuilder sb = new StringBuilder();
            Random random = new Random();

            for (int i = 0; i < length; i++)
            {
                sb.Append(chars[random.Next(chars.Length)]);
            }
            return sb.ToString();
        }
        public static void Send(string email, string subject, string body)
        {
            SmtpClient Client = new SmtpClient()
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential()
                {
                    UserName = "fackmirav@gmail.com",
                    Password = "tbhbucijdxkwormm"
                }

            };
            MailAddress FromEmail = new MailAddress("fackmirav@gmail.com", "‪mirav‬‏");
            string ToEmail = email;
            MailMessage Message = new MailMessage()
            {
                From = FromEmail,
                Subject = subject,
                Body = body
            };

            Message.To.Add(ToEmail);

            Client.Send(Message);

        }
        public static bool IsCurrent(string mail)
        {
            if (mail.IndexOf("@") < 0)
                return false;
            try
            {
                MailAddress addr = new MailAddress(mail, "SpaceShooter");
                MailMessage Message = new MailMessage()
                {
                    From = addr,
                };
                Message.To.Add(addr);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
    }
}

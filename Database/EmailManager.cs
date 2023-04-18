using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    /// <summary>
    /// מחלקה המנהלת שליחת מיילים למשתמשים
    /// </summary>
    public class EmailManager
    {
        /// <summary>
        /// פעולה אשר מחזירה קוד רנדומלי באורך length 
        /// </summary>
        /// <param name="length">אורך הקוד</param>
        /// <returns>קוד באורך length</returns>
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
        /// <summary>
        /// פעולה אשר שולחת מייל
        /// </summary>
        /// <param name="email">המייל אליו לשלוח</param>
        /// <param name="subject">כותרת המייל</param>
        /// <param name="body">גוך/תוכן המייל</param>
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
                    UserName = "theworldofpc89@gmail.com",
                    Password = "zmotyyjdzpetkmsi"
                }

            };
            MailAddress FromEmail = new MailAddress("theworldofpc89@gmail.com", "SpaceShooter");
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
        /// <summary>
        /// פעולה אשר בודקת אם המייל תקין
        /// </summary>
        /// <param name="mail">מייל אשר אותו רוצים לבדוק</param>
        /// <returns>אמת אם המייל תקין שקר אם המייל אינו תקין</returns>
        public static bool IsCurrect(string mail)
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

using Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders
{
    /// <summary>
    /// מחלקה סטטית אשר מחילה אובייקט יוזר בכדי לגשת איליו בשאאר המחלקות
    /// </summary>
    internal static class Session
    {
        public static User User { get;  set; }

        /// <summary>
        /// פעולה אשר מאתחלת את המשתמש
        /// </summary>
        public static void Reset() 
        {
            Session.User = null;
           
        }
    }
}

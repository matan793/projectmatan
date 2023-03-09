using Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1
{
    internal class Session
    {
        public static User User { get;  set; }

        public static void Reset()
        {
            Session.User = null;
           
        }
    }
}

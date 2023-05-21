using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Models
{
    /// <summary>
    /// מחלקה המייצגת משתמש
    /// </summary>
    public class User 
    {
        public int Id { get; set; }//מספר מזהה
        public string Username { get; set; }//שם משתמש
        public string Password { get; set; }//סיסמה
        public string Mail { get; set; }//מייל
        public int Score { get; set; }//ניקוד כללי
        public int HighScore { get; set; }//שיא הניקוד
        public Skin Skin { get; set; }//סקין(מראה).ד
        public int ShieldNum { get; set; }//מספר מגנים
    }
    /// <summary>
    /// 
    /// </summary>
    public enum Skin
    {
        Default,
        Ship,
        Supership
    }
}

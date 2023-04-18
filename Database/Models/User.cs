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
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Mail { get; set; }
        public int Score { get; set; }
        public int HighScore { get; set; }
        public Skin Skin { get; set; }
        public int ShieldNum { get; set; }
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

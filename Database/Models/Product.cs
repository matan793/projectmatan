using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Graphics.Printing3D;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace Database.Models
{
    /// <summary>
    /// מחלקה אשר מייצגת מוצר אשר משומש בחנות ובמחסן
    /// </summary>
    public class Product
    {
        public int Uid { get; set; }///מספר המזהה של המשתמש
        private Skin skin;// מזהה הסקין הפרטי
        public Skin Skin { get { return skin; } //מזהה הסקין הפומבי
            set 
            {
                skin = value;
                Image = new Image();
                Image.Width = 100;
                Image.Height = 100;
                Image.Source = new BitmapImage(new Uri($"ms-appx:///Assets/Spaceship1/{value}.png"));
            } 
        }
        public Image Image { get; set; }///תמונת הסקין
        public string Name { get; set; }//שם הסקין
        public int Price { get; set; }//מחיר הסקין
        
    }
}

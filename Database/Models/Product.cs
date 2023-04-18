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
        public int Uid { get; set; }
        private Skin skin;
        public Skin Skin { get { return skin; } set 
            {
                skin = value;
                Image = new Image();
                Image.Width = 100;
                Image.Height = 100;
                Image.Source = new BitmapImage(new Uri($"ms-appx:///Assets/Spaceship1/{value}.png"));
            } 
        }
        public Image Image { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        
    }
}

using Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace SpaceInvaders
{
    public class Skin
    {
        public Image Image { get; private set; }
        public Skins skin { get { return skin; } set { Image.Source = new BitmapImage(new Uri($"ms-appx:///Assets/Spaceship1/{value}.png")); } }
        public Skin(Skins skin) { this.skin = skin; }
    }
}

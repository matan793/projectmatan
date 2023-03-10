using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Devices.Core;
using Windows.Networking.NetworkOperators;
using Windows.Services.Maps;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
namespace SpaceInvaders
{
    internal class GreenEnemy : Enemy
    {
        public GreenEnemy(double placeX, double placeY, int speed,Canvas arena, int size) : base(placeX, placeY, speed,arena, size)
        {
            this.image.Source = new BitmapImage(new Uri("ms-appx:///Assets/greenEnemy.gif"));
            this.lives = 2;
            this.speedX = speed;
        }

        protected override void MoveTimer_Tick(object sender, object e)
        {
            base.MoveTimer_Tick(sender, e);
            
        }
        protected override void MatchGifToState()
        {
            if (this.lives == 1)
                this.image.Source = new BitmapImage(new Uri("ms-appx:///Assets/Enemy.gif"));
        }
        public override void Shoot()
        {
            if (!destroyed)
            {
                Bullet b1 = new Bullet(this.placeX + 25, this.placeY, this.arena, 10, false, 0);
                
                bullets.Add(b1);
                
            }
        }

    }
}

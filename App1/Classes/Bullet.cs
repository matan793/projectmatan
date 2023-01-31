using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace SpaceInvaders
{
    internal class Bullet : MovingItem
    {
        public Bullet(double placeX, double placeY, Canvas arena, int size,bool IsPlayerShooting, int speedx) : base(placeX, placeY, arena, size)
        {
            this.image.Source = new BitmapImage(new Uri("ms-appx:///Assets/Spaceship1/player.png"));

            this.speedX = speedx;
            if (IsPlayerShooting)
                this.SpeedY = -7;
            else
                this.SpeedY = 7;
        }
     
        protected override  void MoveTimer_Tick(object sender, object e)
        {
            if (!destroyed)
            {
                this.PlaceY += this.SpeedY;
                this.placeX += this.speedX;
              Canvas.SetTop(this.image, this.PlaceY);
              Canvas.SetLeft(this.image, this.PlaceX);
              

             
            }
        }
       
    }
}

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
    /// <summary>
    /// מחקלה המייצגת כדור
    /// </summary>
    internal class Bullet : MovingItem
    {
        /// <summary>
        /// פעולה אשר בונה את המחלקה
        /// </summary>
        /// <param name="placeX">המיקום האופי של הכדור</param>
        /// <param name="placeY">המיקום האנכי של הכדור</param>
        /// <param name="arena">זירת המשחק בו הוא מרונדר</param>
        /// <param name="size">גודל הכדור</param>
        /// <param name="IsPlayerShooting">האם השחקן יורה את הכדור</param>
        /// <param name="speedx">מהירות אופקית</param>
        public Bullet(double placeX, double placeY, Canvas arena, int size,bool IsPlayerShooting, int speedx) : base(placeX, placeY, arena, size)
        {
            this.image.Source = new BitmapImage(new Uri("ms-appx:///Assets/Spaceship1/Default.png"));

            this.speedX = speedx;
            if (IsPlayerShooting)
                this.SpeedY = -7;
            else
                this.SpeedY = 7;
        }
     /// <summary>
     /// פונקציה הנקראת כל אלפית שנייה ומטרתה היא להציז את הכדור
     /// </summary>
     /// <param name="sender"></param>
     /// <param name="e"></param>
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

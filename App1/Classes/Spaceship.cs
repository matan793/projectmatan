using System;
using System.Collections.Generic;
using System.Linq;
using Database.Models;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml;

namespace SpaceInvaders
{
    //public enum StateType
    //{
    //    idleLeft, idleRight, runLeft, runRight, jumpLeft, jumpRight, dieLeft, dieRight, attackLeft, attackRight,
    //    glideLeft, glideRight, slideLeft, slideRight, throwLeft, throwRight, climbUp, jumpAttackLeft, jumpAttackRight, jumpThrowLeft, jumpThrowRight
    //}
    internal class Spaceship : MovingItem
    {
        public event EventHandler Touch;
        private List<Bullet> bullets;
        public List<Bullet> Bullets { get { return bullets; } }
        private int defendsec = 0;
        public int Defendsec { get { return defendsec; } }
        DispatcherTimer defendtimer;
        public Spaceship(double placeX, double placeY, Canvas arena, int size, Skin skin) : base(placeX, placeY, arena, size)
        {
            this.image.Source = new BitmapImage(new Uri($"ms-appx:///Assets/Spaceship1/{skin}.png"));
            this.bullets = new List<Bullet>();
        }


        protected override void MoveTimer_Tick(object sender, object e)
        {
            
            this.placeX += this.speedX;
            Canvas.SetLeft(this.image, this.placeX);
            if (this.placeX <= 0)
            {
                this.placeX += 10;
                Canvas.SetLeft(this.image, this.placeX);
                this.speedX = 0;
            }
            if (this.placeX + this.image.Width >= arena.ActualWidth)
            {
                this.placeX -= 10;
                Canvas.SetLeft(this.image, this.placeX);
                this.speedX = 0;
            }
            for (int i = 0; i < this.bullets.Count; i++)
            {
                if (bullets[i].PlaceY < 0)
                {
                    arena.Children.Remove(this.bullets[i].Image);
                    this.bullets.RemoveAt(i);
                }
            }
            if (this.Touch != null)
                this.Touch(this, null);
            

        }
        public  void defence()
        {
            defendsec += 10;

            this.defendtimer?.Stop();
            this.defendtimer = new DispatcherTimer();
            defendtimer.Interval = TimeSpan.FromMilliseconds(1000);
            defendtimer.Start();
            defendtimer.Tick += Defendtimer_Tick; ;


        }

        private void Defendtimer_Tick(object sender, object e)
        {
            defendsec--;
            if (defendsec == 0)
                defendtimer.Stop();

        }

        internal void Shoot()
        {
            Bullet b = new Bullet(this.placeX+50, this.placeY, arena, 10, true, 0);
            bullets.Add(b);
        }
    }
}
/// <summary>
/// הפעולה תתבצע אלף פעמים בשנייה אחת
/// </summary>
/// <summary>
/// הפעולה  מתאמת בין מצב הדמות לבין המראה שלה
/// </summary>
//private void MatchGifToState()
//{
//    switch (this.state)
//    {
//        case StateType.idleRight: this.image.Source = new BitmapImage(new Uri("ms-appx:///Assets/Ninja/NinjaIdleRight.gif")); break;
//        case StateType.idleLeft: this.image.Source = new BitmapImage(new Uri("ms-appx:///Assets/Ninja/NinjaIdleLeft.gif")); break;
//        case StateType.runRight: this.image.Source = new BitmapImage(new Uri("ms-appx:///Assets/Ninja/NinjaRunRight.gif")); break;
//        case StateType.runLeft: this.image.Source = new BitmapImage(new Uri("ms-appx:///Assets/Ninja/NinjaRunLeft.gif")); break;
//        case StateType.attackRight: this.image.Source = new BitmapImage(new Uri("ms-appx:///Assets/Ninja/NinjaAttackRight.gif")); break;
//        case StateType.attackLeft: this.image.Source = new BitmapImage(new Uri("ms-appx:///Assets/Ninja/NinjaAttackLeft.gif")); break;
//        case StateType.jumpRight: this.image.Source = new BitmapImage(new Uri("ms-appx:///Assets/Ninja/NinjaJumpRight.gif")); break;
//        case StateType.jumpLeft: this.image.Source = new BitmapImage(new Uri("ms-appx:///Assets/Ninja/NinjaJumpLeft.gif")); break;
//        case StateType.throwRight: this.image.Source = new BitmapImage(new Uri("ms-appx:///Assets/Ninja/NinjaThrowRight.gif")); break;
//        case StateType.throwLeft: this.image.Source = new BitmapImage(new Uri("ms-appx:///Assets/Ninja/NinjaThrowLeft.gif")); break;
//        case StateType.dieRight: this.image.Source = new BitmapImage(new Uri("ms-appx:///Assets/Ninja/NinjaDieRight.gif")); break;
//        case StateType.dieLeft: this.image.Source = new BitmapImage(new Uri("ms-appx:///Assets/Ninja/NinjaDieLeft.gif")); break;
//        case StateType.climbUp: this.image.Source = new BitmapImage(new Uri("ms-appx:///Assets/Ninja/NinjaClimb.gif")); break;
//        case StateType.glideRight: this.image.Source = new BitmapImage(new Uri("ms-appx:///Assets/Ninja/NinjaGlideRight.gif")); break;
//        case StateType.glideLeft: this.image.Source = new BitmapImage(new Uri("ms-appx:///Assets/Ninja/NinjaGlideLeft.gif")); break;
//    }
//}
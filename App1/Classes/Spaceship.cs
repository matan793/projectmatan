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
    /// <summary>
    /// מחלקה המייצגת חללית(שחקן).  
    /// </summary>
    internal class Spaceship : MovingItem
    {
        public event EventHandler Touch;
        private List<Bullet> bullets;
        public List<Bullet> Bullets { get { return bullets; } }
        private int defendsec = 0;
        public int Defendsec { get { return defendsec; } }
        DispatcherTimer defendtimer;
        /// <summary>
        /// פעולה בונה
        /// </summary>
        /// <param name="placeX">מיקום אופקי</param>
        /// <param name="placeY">מיקום אנכי</param>
        /// <param name="arena">זירת המשחק</param>
        /// <param name="size">גודל</param>
        /// <param name="skin">סקין(הנראות של השחקן)כ</param>
        public Spaceship(double placeX, double placeY, Canvas arena, int size, Skin skin) : base(placeX, placeY, arena, size)
        {
            this.image.Source = new BitmapImage(new Uri($"ms-appx:///Assets/Spaceship1/{skin}.png"));
            this.bullets = new List<Bullet>();
        }
        /// <summary>
        /// פעולה אשר נקראת כל אלפית שנייה ואחראית על לבדוק את גבולות השחקן והכדורים
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// <summary>
        /// פעולה אשר גורמת לשחקן להיות חסין ב10 שניות יותר
        /// </summary>
        public  void defence()
        {
            defendsec += 10;

            this.defendtimer?.Stop();
            this.defendtimer = new DispatcherTimer();
            defendtimer.Interval = TimeSpan.FromMilliseconds(1000);
            defendtimer.Start();
            defendtimer.Tick += Defendtimer_Tick; ;


        }
        /// <summary>
        /// פעולה אשר נקראת כל שנייה ומורידה שנייה מהזמן של החסינות
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Defendtimer_Tick(object sender, object e)
        {
            defendsec--;
            if (defendsec == 0)
                defendtimer.Stop();

        }
        /// <summary>
        /// פעולה אשר גורמת לשחקן לירות
        /// </summary>
        internal void Shoot()
        {
            Bullet b = new Bullet(this.placeX+50, this.placeY, arena, 10, true, 0);
            bullets.Add(b);
        }
    }
}

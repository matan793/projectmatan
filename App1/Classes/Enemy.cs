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
    /// <summary>
    /// מחלקה המייצגת אוייב בסיסי 
    /// </summary>
    internal class Enemy : MovingItem
    {
        public new event EventHandler Touch;
        protected List<Bullet> bullets;
        public List<Bullet> Bullets { get { return bullets; } }
        protected int lives;
        public int Lives { get { return lives; } set { this.lives = value; MatchGifToState(); } }
        /// <summary>
        /// פעולה בונה של המחלקה
        /// </summary>
        /// <param name="placeX">המיקום האופקי</param>
        /// <param name="placeY">המיקום האנכי</param>
        /// <param name="speed">המירות</param>
        /// <param name="arena">זירת המשחק</param>
        /// <param name="size">גודל</param>
        public Enemy(double placeX, double placeY, int speed,Canvas arena, int size) : base(placeX, placeY, arena, size)
        {
            this.image.Source = new BitmapImage(new Uri("ms-appx:///Assets/Enemy.gif"));
            this.lives = 1;
            this.speedX = speed;
            this.bullets = new List<Bullet>();
            
        }
        /// <summary>
        /// פעולה שנקראת כל אלפית שנייה. הפעולה מבצעת את הפעולה הקדומה שלה ובנוסף מזמנת את האירוע של נגיע
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void MoveTimer_Tick(object sender, object e)
        {
            if (!destroyed)
            {
                base.MoveTimer_Tick(sender, e);
                if (this.Touch != null)
                    this.Touch(this, null);
            }
        }
        /// <summary>
        /// פעולה אשר מתאימה את התמונה של האוייב למצב הנוככי שלו
        /// </summary>
        protected virtual void MatchGifToState()
        {
            return;
        }
        /// <summary>
        /// פעולה אשר גורמת לאוייב לירות ירייה
        /// </summary>
        public virtual void Shoot()
        {
            if (!destroyed)
            {
                Bullet b = new Bullet(this.placeX + 25, this.placeY, this.arena, 10, false, 0);
                bullets.Add(b);
            }
        }
    }
}

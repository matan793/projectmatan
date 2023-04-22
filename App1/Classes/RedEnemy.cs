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
    /// מחלקה המייצת אוייב ברמה השלישית
    /// </summary>
    class RedEnemy : Enemy
    {
        /// <summary>
        /// פעולה בונה של המחלקה
        /// </summary>
        /// <param name="placeX">המיקום האופקי</param>
        /// <param name="placeY">המיקום האנכי</param>
        /// <param name="speed">המירות</param>
        /// <param name="arena">זירת המשחק</param>
        /// <param name="size">גודל</param>
        public RedEnemy(double placeX, double placeY, int speed, Canvas arena, int size) : base(placeX, placeY, speed, arena, size)
        {
            this.image.Source = new BitmapImage(new Uri("ms-appx:///Assets/redEnemy.gif"));
            this.lives = 3;
            this.speedX = speed;
        }
        /// <summary>
        /// פעולה הנקראת כל אלפית שנייה. הפעולה מזמנת את הפעולה הקדומה לה
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void MoveTimer_Tick(object sender, object e)
        {
            base.MoveTimer_Tick(sender, e);

        }
        /// <summary>
        /// פעולה אשר מעדכנת את התמונה של האוייב על פי מספר החיים שלו
        /// </summary>
        protected override void MatchGifToState()
        {
            if(lives == 2)
                this.image.Source = new BitmapImage(new Uri("ms-appx:///Assets/greenEnemy.gif"));

            if (this.lives == 1)
                this.image.Source = new BitmapImage(new Uri("ms-appx:///Assets/Enemy.gif"));
        }
        /// <summary>
        /// פעולה אשר גורמת לאוייב לירות שלוש יריות לקיוונים שונים
        /// </summary>
        public override void Shoot()
        {
            if (!destroyed)
            {
                Bullet b1 = new Bullet(this.placeX + 25, this.placeY, this.arena, 10, false, 0);
                Bullet b2 = new Bullet(this.placeX + 25, this.placeY, this.arena, 10, false, 7);
                Bullet b3 = new Bullet(this.placeX + 25, this.placeY, this.arena, 10, false, -7);
                bullets.Add(b1);
                bullets.Add(b2);
                bullets.Add(b3);
            }
        }
    }
}

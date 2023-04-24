using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml;
using Windows.Foundation;

namespace SpaceInvaders
{
    /// <summary>
    /// מחלקה המייצגת אובייקט שזז
    /// </summary>
    internal class MovingItem
    {
        protected double placeX; //מיקום אופקי
        protected double placeY;//מיקום אנכי
        protected Image image;//מראה הדמות
        protected Canvas arena;//זירת המשחק
        protected double speedX;//מהירות אופקית
        protected double speedY;//מהירות אנכית
        protected bool destroyed;
        protected DispatcherTimer moveTimer;//הטיימר שאחראי על תנועת הדמות
        public event EventHandler Touch;


        public double PlaceX { get { return placeX; } set { placeX = value; } }
        public double PlaceY { get { return placeY; } set { placeY = value; } }
        public double SpeedX { get { return speedX; } set { speedX = value; } }
        public double SpeedY { get { return speedY; } set { speedY = value; } }
        public Image Image { get { return image; } set { image = value; } }
        public bool Destroyed { get { return destroyed; } set { destroyed = value; } }

        /// <summary>
        /// פעולה בונה
        /// </summary>
        /// <param name="placeX">מיקום אופקי</param>
        /// <param name="placeY">מיקום אנכי</param>
        /// <param name="arena">זירת המשחק</param>
        /// <param name="size">גודל</param>
        public MovingItem(double placeX, double placeY, Canvas arena, int size)
        {
            this.placeX = placeX;
            this.placeY = placeY;
            this.arena = arena;
            this.image = new Image();

            this.image.Width = size;
            this.image.Height = size;
            this.image.Source = new BitmapImage(new Uri("ms-appx:///Assets/StoreLogo.png"));
            //MatchGifToState();
            Canvas.SetLeft(this.image, this.placeX);
            Canvas.SetTop(this.image, this.placeY);
            this.arena.Children.Add(this.image);
            this.speedX = 0;

            this.moveTimer = new DispatcherTimer();
            this.moveTimer.Interval = TimeSpan.FromMilliseconds(1);
            this.moveTimer.Start();
            this.moveTimer.Tick += MoveTimer_Tick;
        }
        /// <summary>
        /// פעולה אשר מחזירה אובייקט מסוג מלבן שמייצג את היקפו של האובייקט
        /// </summary>
        /// <returns></returns>
        public virtual Rect GetRectangle() => new Rect((int)this.placeX + 2, (int)this.placeY, (int)this.Image.Height, (int)this.Image.Width);
        /// <summary>
        /// פעולה אשר נקראת כל אלפית שנייה ואחראית על לבדוק את גבולות האובייקט
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void MoveTimer_Tick(object sender, object e)
        {
            
            if (!destroyed)
            {
                this.placeX += this.speedX;
                Canvas.SetLeft(this.image, this.placeX);
                if (this.placeX <= 0)
                {

                    Canvas.SetLeft(this.image, this.placeX);
                    this.speedX = 0;
                }
                if (this.placeX + this.image.Width >= arena.ActualWidth)
                {

                    Canvas.SetLeft(this.image, this.placeX);
                    this.speedX = 0;
                }
                if (this.Touch != null)
                    this.Touch(this, null);
            }

        }
        /// <summary>
        /// פעולה אשר גורמת לאובייקט לזוז ימינה
        /// </summary>
        public void GoRight()
        {
            if (!destroyed)

                //MatchGifToState();
                this.speedX = 10;


        }
        /// <summary>
        /// פעולה אשר גורמת לאובייקט לזוז שמאלה
        /// </summary>
        public void GoLeft()
        {
            if (!destroyed)
                //MatchGifToState();
                this.speedX = -10;


        }
        /// <summary>
        /// פעולה אשר גורמת לאובייקט לעזור
        /// </summary>
        public virtual void Stop()
        {


            //MatchGifToState();
           
                this.speedX = 0;

        }
    }
}

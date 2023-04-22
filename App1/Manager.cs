using DataBase;
using Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Shapes;
using Windows.System;


namespace SpaceInvaders
{
    /// <summary>
    /// מחלקה אשר מנהלת את המשחק
    /// </summary>
    internal class Manager
    {
        private Spaceship spaceship;
        public event EventHandler removelifie;//מחקית לב מן השחקן
        public event EventHandler addpoints;//הוספת נק
        public event EventHandler updateDefendTime;//הוספת נק
        private Enemies enemies;
        Canvas Arena;
        private int points;
        private int lives;
        private int level;
        private uint cooldown;
        protected DispatcherTimer GameTimer;//הטיימר שאחראי על תנועת הדמות
        private int speed;
        
        public int DefendTime { get; private set; }
        /// <summary>
        /// פעולה בונה
        /// </summary>
        /// <param name="Arena">זירת המשחק</param>
        public Manager(Canvas Arena)
        {
            this.level = 1;
            this.speed = 3;
            this.cooldown = 0;
            this.spaceship = new Spaceship(50, 900, Arena, 100, Session.User.Skin);
            
            this.enemies = new Enemies(3, 10, speed,Arena, level);
            for (int i = 0; i < enemies.Enemiess.GetLength(0); i++)
            {
                for (int j = 0; j < enemies.Enemiess.GetLength(1); j++)
                {
                    enemies.Enemiess[i, j].Touch += Enemy_Touch;
                }
            }

            this.GameTimer = new DispatcherTimer();
            this.GameTimer.Interval = TimeSpan.FromMilliseconds(1);
            this.GameTimer.Start();
            this.GameTimer.Tick += Game_Tick;

            this.Arena = Arena;
            this.lives = 3;
            this.points = 0;
        }
        /// <summary>
        /// פעולה אשר נקראת כל אלפית שנייה ואחראית על מעבר שלב, זמן יריית הכדור ועוד
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Game_Tick(object sender, object e)
        {
            if(cooldown < uint.MaxValue)
                cooldown++;
            this.DefendTime = spaceship.Defendsec;
            this.updateDefendTime?.Invoke(this, null);
            if(enemies.Count == 0)
            {
                level++;
                this.speed += 3;
                enemies = new Enemies(3, 10, speed,Arena, level);
                for (int i = 0; i < enemies.Enemiess.GetLength(0); i++)
                {
                    for (int j = 0; j < enemies.Enemiess.GetLength(1); j++)
                    {
                        enemies.Enemiess[i, j].Touch += Enemy_Touch;
                    }
                }
            }
        }
        /// <summary>
        /// פעולה אשר נקראת על אלפית שנייה ובודרת אם האוייב פגע בשחקן, ירייה של האוייב פגעה בשחקן ואם ירייה של השחקן פעעה באוייב
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Enemy_Touch(object sender, EventArgs e)
        {
            Enemy enemy = (Enemy)sender;
            Rect enemyrectangle = enemy.GetRectangle();
            Rect shiprectangle = this.spaceship.GetRectangle();
            Rect touchrectangle = RectHelper.Intersect(shiprectangle, enemyrectangle);

            if (touchrectangle.Width > 0 && touchrectangle.Height > 0)//enemy touch spaceship
            {
                this.Arena.Children.Remove(enemy.Image);
                CoreApplication.Exit();
                enemy.Touch -= Enemy_Touch;
            }

            List<Bullet> bullets = this.spaceship.Bullets;
            for (int i = 0; i < bullets.Count & enemy != null; i++)//spaceship bullet touch enemy bullet
            {
                Rect bulletrectangle = bullets[i].GetRectangle();
                touchrectangle = RectHelper.Intersect(bulletrectangle, enemyrectangle);
                if (touchrectangle.Width > 0 || touchrectangle.Height > 0)
                {
                    enemy.Lives--;
                    this.Arena.Children.Remove(bullets[i].Image);
                    bullets.RemoveAt(i);
                    if (enemy.Lives == 0)
                    {
                        enemy.Destroyed = true;
                        enemies.Count--;
                        this.Arena.Children.Remove(enemy.Image);

                        this.enemies.Remove(ref enemy);
                        this.points += 1;
                        if (addpoints != null)
                            addpoints(points, null);
                    }
                    
                }
            }
            
            for (int i = 0; i < enemies.Enemiess.GetLength(0); i++)//enemy bullet touch spaceship
            {
                for (int j = 0; j < enemies.Enemiess.GetLength(1); j++)
                {
                    List<Bullet> enemiesbullets = this.enemies.Enemiess[i, j].Bullets;
                    for (int k = 0; k < enemiesbullets.Count; k++)
                    {
                        Rect bulletrectangle = enemiesbullets[k].GetRectangle();
                        touchrectangle = RectHelper.Intersect(bulletrectangle, shiprectangle);
                        if (touchrectangle.Width > 0 || touchrectangle.Height > 0)
                        {
                           
                            enemiesbullets[k].Destroyed = true;
                            this.Arena.Children.Remove(enemiesbullets[k].Image);
                            enemiesbullets[k].Touch -= Enemy_Touch;
                            enemiesbullets.RemoveAt(k);

                            if (this.spaceship.Defendsec == 0 && removelifie != null)
                            {
                                removelifie(lives, null);
                                lives--;
                                
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// פעולה אשר נקראת כל פעם שהמשתמש לחץ על המקלדת שלו
        /// </summary>
        /// <param name="virtualKey"></param>
        internal  void MoveCharacter(VirtualKey virtualKey)
        {
           

                switch (virtualKey)
                {
                    case VirtualKey.Left:
                        spaceship.GoLeft();
                        break;

                    case VirtualKey.A:
                        if (this.spaceship != null)
                            this.spaceship.GoLeft();
                        break;

                    case VirtualKey.Right:

                        this.spaceship.GoRight();
                        break;
                    case VirtualKey.D:

                        this.spaceship.GoRight();
                        break;
                    case VirtualKey.Space:
                        if(cooldown > 10)
                    {
                        this.spaceship.Shoot();
                        cooldown= 0;
                    }
                    break;
            }
          
            
        }
        /// <summary>
        /// פעולה אשר מוסיפה 10 לזמן החסינות
        /// </summary>
        /// <returns></returns>
        public bool defend()
        {
            if(Session.User.ShieldNum > 0)
            {
                this.spaceship.defence();
                Session.User.ShieldNum--;
                SqlHelper.SubtractShield(Session.User.Id);
                return true;
            }
            return false;
        }
        /// <summary>
        /// פעולה אשר עוצרת את המשחק
        /// </summary>
        public void StopGame()
        {
            StopCharacter();
            this.spaceship.Destroyed = true;
            enemies.Stop();
            Session.User.Score += points;
            Session.User.HighScore = points > Session.User.HighScore ? points : Session.User.HighScore;

            SqlHelper.UpdateScore(Session.User.Id, Session.User.Score, Session.User.HighScore);
        }
        /// <summary>
        /// פעולה אשר עוצרת את השחקן
        /// </summary>
        internal void StopCharacter()

        {
            if(this.spaceship != null)
                this.spaceship.Stop();
           
        }
    }
}

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
using App1;

namespace SpaceInvaders
{
    internal class Manager
    {
        private Spaceship1 spaceship1;
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
        public Manager(Canvas Arena)
        {
            this.level = 1;
            this.speed = 3;
            this.cooldown = 0;
            this.spaceship1 = new Spaceship1(50, 900, Arena, 100);
            
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

        private void Game_Tick(object sender, object e)
        {
            if(cooldown < uint.MaxValue)
                cooldown++;
            this.DefendTime = spaceship1.Defendsec;
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

        private void Enemy_Touch(object sender, EventArgs e)
        {
            Enemy enemy = (Enemy)sender;
            Rect enemyrectangle = enemy.GetRectangle();
            Rect shiprectangle = this.spaceship1.GetRectangle();
            Rect touchrectangle = RectHelper.Intersect(shiprectangle, enemyrectangle);

            if (touchrectangle.Width > 0 && touchrectangle.Height > 0)//enemy touch spaceship
            {
                this.Arena.Children.Remove(enemy.Image);
                CoreApplication.Exit();
                enemy.Touch -= Enemy_Touch;
            }

            List<Bullet> bullets = this.spaceship1.Bullets;
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

                            if (this.spaceship1.Defendsec == 0 && removelifie != null)
                            {
                                removelifie(lives, null);
                                lives--;
                                
                            }
                        }
                    }
                }
            }
        }
        internal  void MoveCharacter(VirtualKey virtualKey)
        {
           

                switch (virtualKey)
                {
                    case VirtualKey.Left:
                        spaceship1.GoLeft();
                        break;

                    case VirtualKey.A:
                        if (this.spaceship1 != null)
                            this.spaceship1.GoLeft();
                        break;

                    case VirtualKey.Right:

                        this.spaceship1.GoRight();
                        break;
                    case VirtualKey.D:

                        this.spaceship1.GoRight();
                        break;
                    case VirtualKey.Space:
                        if(cooldown > 10)
                    {
                        this.spaceship1.Shoot();
                        cooldown= 0;
                    }
                    break;
                case VirtualKey.F: 
                        this.spaceship1.defence();
                    break;
                        
                 
                    

                }
          
            
        }
        public void defend()
        {
            this.spaceship1.defence();
        }
        public void StopGame()
        {
            StopCharacter();
            enemies.Stop();

            Session.User.Score += points;
            Session.User.HighScore = points > Session.User.HighScore ? points : Session.User.HighScore;

            SqlHelper.AddScore(Session.User.Id, Session.User.Score, Session.User.HighScore);
        }
        internal void StopCharacter()
        {
            if(this.spaceship1 != null)
                this.spaceship1.Stop();
           
        }
    }
}

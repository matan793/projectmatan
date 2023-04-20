using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.Networking.Sockets;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SpaceInvaders
{
    internal class Enemies
    {
        private int speed;
        private int level;
        Enemy[,] enemies;
        
        private int count;

        public int Count
        {
            get { return count; }
            set { count = value; }
        }

        public Enemy[,] Enemiess { get { return enemies; } }
        protected Canvas arena;//זירת המשחק
        protected DispatcherTimer moveTimer;//הטיימר שאחראי על תנועת הדמות
        protected  DispatcherTimer ShootTimer;//הטיימר שאחראי על תנועת הדמות

        /// <summary>
        /// 
        /// </summary>
        /// <param name="numrow"></param>
        /// <param name="numcollum"></param>
        /// <param name="speed"></param>
        /// <param name="arena"></param>
        /// <param name="level"></param>
        public Enemies(int numrow, int numcollum, int speed, Canvas arena, int level)
        {

            this.speed = speed;
            this.level = level;

            this.moveTimer = new DispatcherTimer();
            this.moveTimer.Interval = TimeSpan.FromMilliseconds(1);
            this.moveTimer.Start();
            this.moveTimer.Tick += MoveTimer_Tick;

            this.ShootTimer = new DispatcherTimer();
            this.ShootTimer.Interval = TimeSpan.FromMilliseconds(2000);
            this.ShootTimer.Start();
            this.ShootTimer.Tick += ShootTimer_Tick;

            this.arena = arena;
            enemies = new Enemy[numrow, numcollum];
           
            GenerateEnemiesByLevels(level);
           
            this.count = enemies.Length;
        }

        private void GenerateEnemiesByLevels(int level)
        {
            Random rnd = new Random();
            int ex = 300;
            int ey = 200;
            List<Point> greenpoints = new List<Point>(capacity: level);

            for (int y = 0; y < enemies.GetLength(0); y++)
            {
                for (int x = 0; x < enemies.GetLength(1); x++)
                {
                    
                    enemies[y, x] = new Enemy(ex, ey, speed, arena, 50);
                    
                    ex += 55;
                }
                ey += 55;
                ex = 300;
            }
            if (level > 1)
            {
                for (int i = 0; i < greenpoints.Capacity; i++)
                {
                    greenpoints.Add(new Point(rnd.Next(0, enemies.GetLength(1) - 1), rnd.Next(0, enemies.GetLength(0) - 1)));
                }
                for (int i = 0; i < greenpoints.Count-1; i++)
                {
                    double x = enemies[greenpoints[i].y, greenpoints[i].x].PlaceX, y = enemies[greenpoints[i].y, greenpoints[i].x].PlaceY;
                    arena.Children.Remove(enemies[greenpoints[i].y, greenpoints[i].x].Image);
                    enemies[greenpoints[i].y, greenpoints[i].x].Destroyed = true;
                    enemies[greenpoints[i].y, greenpoints[i].x] = new GreenEnemy(x, y, speed, arena, 50);
                }
            }
            if (level > 4)
            {
                List<Point> redpoints = new List<Point>(capacity: level - 4);

                for (int i = 0; i < redpoints.Capacity; i++)
                {
                    redpoints.Add(new Point(rnd.Next(0, enemies.GetLength(1) - 1), rnd.Next(0, enemies.GetLength(0) - 1)));
                }
                for (int i = 0; i < redpoints.Count - 1; i++)
                {
                    double x = enemies[redpoints[i].y, redpoints[i].x].PlaceX, y = enemies[redpoints[i].y, redpoints[i].x].PlaceY;
                    arena.Children.Remove(enemies[redpoints[i].y, redpoints[i].x].Image);
                    enemies[redpoints[i].y, redpoints[i].x].Destroyed = true;
                    enemies[redpoints[i].y, redpoints[i].x] = new RedEnemy(x, y, speed, arena, 50);
                }
            }
        }

        public void Remove(ref Enemy e)
        {
            
            e = null;
        }
        private void ShootTimer_Tick(object sender, object e)
        {
            Random rnd = new Random();
            int i = rnd.Next(0, enemies.GetLength(0));
            int j = rnd.Next(0, enemies.GetLength(1));
            enemies[i, j].Shoot();
        }

        private void MoveTimer_Tick(object sender, object e)
        {
            for (int i = 0; i < enemies.GetLength(0); i++)
            {
                for (int j = 0; j < enemies.GetLength(1); j++)
                {
                    if (enemies[i,j].PlaceX <= 0)
                    {
                        SetSpeed(this.speed);
                        for (int k = 0; k < enemies.GetLength(0); k++)
                        {
                            for (int l = 0; l < enemies.GetLength(1); l++)
                            {
                                enemies[k, l].PlaceY = enemies[k, l].PlaceY + 20;
                                Canvas.SetTop(enemies[k, l].Image, enemies[k, l].PlaceY);
                            }
                        }
                    }
                    if (enemies[i, j].PlaceX + enemies[i, j].Image.Width >= arena.ActualWidth)
                    {
                        SetSpeed(-this.speed);
                        for (int k = 0; k < enemies.GetLength(0); k++)
                        {
                            for (int l = 0; l < enemies.GetLength(1); l++)
                            {
                                enemies[k, l].PlaceY = enemies[k, l].PlaceY + 20;
                                Canvas.SetTop(enemies[k, l].Image, enemies[k, l].PlaceY);
                            }
                        }
                    }

                }
            }
           // CalcCount();
        }
        //private void CalcCount()
        //{
        //    for (int i = 0; i < enemies.GetLength(0); i++)
        //    {
        //        for (int j = 0; j < enemies.GetLength(1); j++)
        //        {
        //            if (!enemies[i, j].Destroyed)
        //                Count++;
        //        }
        //    }
            
        //}
        private void SetSpeed(double speed)
        {
            for (int i = 0; i < enemies.GetLength(0); i++)
            {
                for (int j = 0; j < enemies.GetLength(1); j++)
                {
                    enemies[i, j].SpeedX = speed;
                }
            }
        }

        public void Stop()
        {
            ShootTimer.Stop();
            for (int i = 0; i < enemies.GetLength(0); i++)
            {
                for (int j = 0; j < enemies.GetLength(1); j++)
                {
                    enemies[i, j].Stop();
                }
            }
        }

        internal void Add(int r, int c)
        {
            enemies = new GreenEnemy[r, c];
            int ex = 300;
            int ey = 200;
            for (int y = 0; y < r; y++)
            {
                for (int x = 0; x < c; x++)
                {
                    enemies[y, x] = new GreenEnemy(ex, ey, 20,arena, 50);
                    ex += 55;
                }
                ey += 55;
                ex = 300;
            }
            this.count = enemies.Length;
        }
    }
    struct Point
    {
        public int x;
        public int y;
        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}

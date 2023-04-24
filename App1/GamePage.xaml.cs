
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SpaceInvaders
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>

    public sealed partial class GamePage : Page
    {
        private Manager myManager;
        private int points;   
        /// <summary>
        /// פעולה בונה
        /// </summary>
        public GamePage()
        {
            this.InitializeComponent();

            this.points = 0;
        }
        /// <summary>
        /// פעולה אשר מוסיפה נקודות לטקטס של הנקודות
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddPoints(object sender, EventArgs e)
        {
            int points = (int)sender;
            if (points < 10)
            {
                scoreBlock.Text = "00" + points;
            }
            else if (points < 100)
            {
                scoreBlock.Text = "0" + points;

            }
            else if (points > 100)
            {
                scoreBlock.Text = "" + points;

            }
            this.points = points;
        }
        /// <summary>
        /// פונקציית נקראת כאשר דף נטען והפונקציה אחראית על שיוך פונקציות לאירועים של מנהל המשחק
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
           
            this.myManager = new Manager(canvas);
            Window.Current.CoreWindow.KeyDown += CoreWindow_KeyDown;
            Window.Current.CoreWindow.KeyDown +=  DefendKeyDown;
            Window.Current.CoreWindow.KeyUp += CoreWindow_KeyUp;
            this.myManager.addpoints += AddPoints;
            this.myManager.removelifie += RemoveLife;
            this.myManager.updateDefendTime += MyManager_updateDefendTime;
            

            NumOfShieldTxt.Text = Session.User.ShieldNum.ToString();
        }
        /// <summary>
        /// פעולה אשר נקראת כאשר המשתמש לוחץ על המקלדת ובודקת אם הוא לחץ על הכפתור f
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void DefendKeyDown(CoreWindow sender, KeyEventArgs args)
        {
            if(args.VirtualKey == Windows.System.VirtualKey.F)
            {
                if (myManager.defend())
                    NumOfShieldTxt.Text = Session.User.ShieldNum.ToString();
            }
        }
        /// <summary>
        /// פעולה אשר מעדכנת את הטקסט של זמן החסינות
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MyManager_updateDefendTime(object sender, EventArgs e)
        {
            defendtimetext.Text = "shield time: " + this.myManager.DefendTime.ToString();
        }
        /// <summary>
        /// פעולה אשר מעדכנת את התטקסט של מספר החייים
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RemoveLife(object sender, EventArgs e)
        {
            int lives = (int)sender;
            if(lives == 3)
            {
                this.life3.Source = null;
                
               
            }
            else if(lives == 2)
            {
                this.life2.Source = null;
                

            }
           else if (lives == 1)
            {
                this.life1.Source = null;

                gameovergrid.Visibility = Visibility.Visible;
                overscore.Text = "Your score is: " + this.points;
                myManager.StopGame();
                Window.Current.CoreWindow.KeyDown -= CoreWindow_KeyDown;
                Window.Current.CoreWindow.KeyDown -= DefendKeyDown;
                Window.Current.CoreWindow.KeyUp -= CoreWindow_KeyUp;
                this.myManager.addpoints -= AddPoints;
                this.myManager.removelifie -= RemoveLife;
                this.myManager.updateDefendTime -= MyManager_updateDefendTime;
            }
           
        }
        /// <summary>
        /// פונקצייה אשרר נקראת כאשר המשתמש עוצב את מקשי המקלדת
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void CoreWindow_KeyUp(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.KeyEventArgs args)
        {
            this.myManager.StopCharacter();
        }

        /// <summary>
        /// פעולה אשר נקראת כאשר המשתמש לוחץ על המקלדת
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void CoreWindow_KeyDown(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.KeyEventArgs args)
        {
          
                this.myManager.MoveCharacter(args.VirtualKey);
          
        }
        /// <summary>
        /// פעולה אשר נקראת כאשר המשתמש לוחץ על הכפתור חזרה לבית
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }
        /// <summary>
        /// פעולה אשר נקראת כאשר המשתמש לוחץ על הכפתור נסה שוב
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(GamePage));
        }
    }
}

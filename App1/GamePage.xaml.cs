
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
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
       

        public GamePage()
        {
            this.InitializeComponent();

            this.points = 0;
        }

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


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
           
            this.myManager = new Manager(canvas);
            Window.Current.CoreWindow.KeyDown += CoreWindow_KeyDown;
            Window.Current.CoreWindow.KeyUp += CoreWindow_KeyUp;
            this.myManager.addpoints += AddPoints;
            this.myManager.removelifie += RemoveLife;
            this.myManager.updateDefendTime += MyManager_updateDefendTime;
        }

        private void MyManager_updateDefendTime(object sender, EventArgs e)
        {
            defendtimetext.Text = this.myManager.DefendTime.ToString();
        }

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
            }
           
        }

        private void CoreWindow_KeyUp(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.KeyEventArgs args)
        {
            this.myManager.StopCharacter();
        }

        private void CoreWindow_KeyDown(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.KeyEventArgs args)
        {

           
          
                this.myManager.MoveCharacter(args.VirtualKey);
          
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(GamePage));
        }
    }
}

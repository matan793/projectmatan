using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace SpaceInvaders
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        /// <summary>
        /// פעולה בונה
        /// </summary>
        public MainPage()
        {
            this.InitializeComponent();
            ApplicationView.GetForCurrentView().TryEnterFullScreenMode();
        }
        /// <summary>
        /// פעולה אשר נקראת כאשר המשתמש לוחץ על הכפתור משחק חדש
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void playBtn_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(GamePage));
            
        }
        /// <summary>
        /// פעולה אשר נקראת כאשר המשתמש לוחץ על כפתור היציאה
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Quit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();
        }
        /// <summary>
        /// פעולה אשר נקראת כאשר המשתמש לוחץ על כפתור השיאים
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void highScoreBtn_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate (typeof(HighScores));
        }
        /// <summary>
        /// פעולה אשר נקראת כאשר המשתמש לוחץ על כפתור המידע
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void informationBtn_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(InformationPage));
        }
        /// <summary>
        /// פעולה אשר נקראת כאשר המשתמש לוחץ על כפתור היציאה מהמשתמש
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void logout_Click(object sender, RoutedEventArgs e)
        {
            Session.Reset();
            Frame.Navigate(typeof(Login));
        }
        /// <summary>
        /// פעולה אשר נקראת בכל פעם שהעכבר נכנס לתכום של הכפתור
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void playBtn_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Hand, 0);
        }
        /// <summary>
        /// פעולה אשר נקראת בכל פעם שהעכבר יוצא מתכום של תיבת הכפתור
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void playBtn_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 0);
        }
        /// <summary>
        /// פעולה אשר נקראת כאשר המשתמש לוחץ על כפתור המחסן
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lockerpbtn_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(LockerPage));
        }
        /// <summary>
        /// פעולה אשר נקראת כאשר המשתמש לוחץ על כפתור החנות
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void shoprpbtn_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ShopPage));
        }
    }
}

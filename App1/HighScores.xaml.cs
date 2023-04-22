using Database.Models;
using DataBase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
    public sealed partial class HighScores : Page
    {
        public List<User> Users;

        /// <summary>
        /// פעולה בונה
        /// </summary>
        public HighScores()
        {
            this.InitializeComponent();
            this.Users = SqlHelper.GetScores();
          
            Leaderboard.ItemsSource = Users;
        }

        /// <summary>
        /// פעולה אשר נקראת כאשר השחקן לוחץ על הכפתור חזרה הביתה
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void scoresBackBtn_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        /// <summary>
        /// פעולה אשר נקרא כאשר השחקן לוחץ על הכפתור משחק חדש
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void playBtn_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(GamePage));
        }

        //private void Order(object sender, SelectionChangedEventArgs e)
        //{
        //    Leaderboard.Items.Clear();
        //}

       
    }
}

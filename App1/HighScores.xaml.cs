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
        public ObservableCollection<User> Users;
        private int count = 0;

        public HighScores()
        {
            this.InitializeComponent();
            this.Users = SqlHelper.GetUsers();
            for (int i = 0; i < Users.Count - 1; i++)
            {
                for (int j = 0; j < Users.Count - i - 1; j++)
                {
                    if (Users[j].HighScore < Users[j + 1].HighScore)
                    {
                        User temp = Users[j];
                        Users[j] = Users[j + 1];
                        Users[j + 1] = temp;
                    }
                }
            }

            Leaderboard.ItemsSource = Users;
        }

        private void scoresBackBtn_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private void playBtn_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(GamePage));
        }

        private void Order(object sender, SelectionChangedEventArgs e)
        {
            if(count == 0)
            {
                count++;
                return;
            }
            this.Users = SqlHelper.GetUsers();
            if (((ComboBoxItem)OrderBy.SelectedItem).Content.ToString() == "High Score")
            {
                for (int i = 0; i < Users.Count - 1; i++)
                {
                    for (int j = 0; j < Users.Count - i - 1; j++)
                    {
                        switch (((ComboBoxItem)WayOfOrder.SelectedItem).Content.ToString())
                        {
                            case "Descending":
                                if (Users[j].HighScore < Users[j + 1].HighScore)
                                {
                                    User temp = Users[j];
                                    Users[j] = Users[j + 1];
                                    Users[j + 1] = temp;
                                }
                                break;
                            case "Ascending":
                                if (Users[j].HighScore > Users[j + 1].HighScore)
                                {
                                    User temp = Users[j];
                                    Users[j] = Users[j + 1];
                                    Users[j + 1] = temp;
                                }
                                break;
                            default:
                                break;
                        }
                        if (Users[j].HighScore < Users[j + 1].HighScore)
                        {
                            User temp = Users[j];
                            Users[j] = Users[j + 1];
                            Users[j + 1] = temp;
                        }
                    }
                    Leaderboard.ItemsSource = Users;
                }
            }
            if (((ComboBoxItem)OrderBy.SelectedItem).Content.ToString() == "Overall Score")
            {
                for (int i = 0; i < Users.Count - 1; i++)
                {
                    for (int j = 0; j < Users.Count - i - 1; j++)
                    {
                        switch (((ComboBoxItem)WayOfOrder.SelectedItem).Content.ToString())
                        {
                            case "Descending":
                                if (Users[j].Score < Users[j + 1].Score)
                                {
                                    User temp = Users[j];
                                    Users[j] = Users[j + 1];
                                    Users[j + 1] = temp;
                                }
                                break;
                            case "Ascending":
                                if (Users[j].Score > Users[j + 1].Score)
                                {
                                    User temp = Users[j];
                                    Users[j] = Users[j + 1];
                                    Users[j + 1] = temp;
                                }
                                break;
                            default:
                                break;
                        }
                        if (Users[j].HighScore < Users[j + 1].HighScore)
                        {
                            User temp = Users[j];
                            Users[j] = Users[j + 1];
                            Users[j + 1] = temp;
                        }
                    }
                }
                Leaderboard.ItemsSource = Users;


            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            OrderBy.SelectedIndex= 0;
            WayOfOrder.SelectedIndex= 0;
        }
    }
}

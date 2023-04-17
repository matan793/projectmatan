using Database.Models;
using DataBase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlTypes;
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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SpaceInvaders
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ShopPage : Page
    {
        
        public ShopPage()
        {
            this.InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            
            ObservableCollection<Product> skins = SqlHelper.GetShop(Session.User.Id);
            foreach (Product skin in skins)
            {
                string str = $"ms-appx:///Assets/Spaceship1/{skin.Skin}.png";

                mylist.Items.Add(skin);
                //mylist.Items.Add(skin.Image);
            }
            Image powerUpimg = new Image();
            powerUpimg.Width = 100;
            powerUpimg.Height = 100;
            powerUpimg.Source = new BitmapImage(new Uri("ms-appx:///Assets/shield.png"));
            powerUpList.Items.Add(powerUpimg);
            
        }

        private void backbtn_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private void buyBtn_Click(object sender, RoutedEventArgs e)
        {
            if (mylist.SelectedItem != null && Session.User.Score >= ((Product)mylist.SelectedItem).Price)
            {
                Session.User.Score -= ((Product)mylist.SelectedItem).Price;
                SqlHelper.AddProduct(Session.User.Id, (int)((Product)mylist.SelectedItem).Skin);
                SqlHelper.AddScore(Session.User.Id, Session.User.Score, Session.User.HighScore);
                mylist.Items.Remove(mylist.SelectedItem);
                buyerr.Visibility = Visibility.Collapsed;

            }
            else if(mylist.SelectedItem == null)
            {
                buyerr.Text = "nothing was selectee, try again.";
                buyerr.Visibility = Visibility.Visible;
            }
            else
            {
                buyerr.Text = "you don't have enough score.";
                buyerr.Visibility = Visibility.Visible;
            }
        }

        private void skinBtn_Click(object sender, RoutedEventArgs e)
        {
            mylist.Visibility = Visibility.Visible;
            powerUpList.Visibility= Visibility.Collapsed;

            powerUpList.SelectedItem = null;

            RequestedTheme = ElementTheme.Light;
            skinBtn.RequestedTheme= ElementTheme.Dark;
            powerUpBtn.RequestedTheme= ElementTheme.Light;

            buyBtn.Visibility = Visibility.Visible;
            buyshieldBtn.Visibility = Visibility.Collapsed;
        }

        private void powerUpBtn_Click(object sender, RoutedEventArgs e)
        {

            mylist.Visibility = Visibility.Collapsed;
            powerUpList.Visibility = Visibility.Visible;

            mylist.SelectedItem = null;

            skinBtn.RequestedTheme = ElementTheme.Light;
            powerUpBtn.RequestedTheme = ElementTheme.Dark;

            buyBtn.Visibility= Visibility.Collapsed;
            buyshieldBtn.Visibility= Visibility.Visible;
        }

        private void buyshieldBtn_Click(object sender, RoutedEventArgs e)
        {
            if (powerUpList.SelectedItem != null && Session.User.Score >= 100)
            {
                Session.User.Score -= 100;
                SqlHelper.AddShield(Session.User.Id);
                SqlHelper.AddScore(Session.User.Id, Session.User.Score, Session.User.HighScore);
                Session.User.ShieldNum++;
                buyerr.Visibility = Visibility.Collapsed;

            }
            else if (powerUpList.SelectedItem == null)
            {
                buyerr.Text = "nothing was selectee, try again.";
                buyerr.Visibility = Visibility.Visible;
            }
            else
            {
                buyerr.Text = "you don't have enough score.";
                buyerr.Visibility = Visibility.Visible;
            }
           
        }
    }
}

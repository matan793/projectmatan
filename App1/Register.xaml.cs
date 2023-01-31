using System;
using System.Collections.Generic;
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
using DataBase;
using Database.Models;
// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SpaceInvaders
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Register : Page
    {
        bool us = false, ps = false, em = false, cps = false;

        private void username_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (username.Text != "")
                us = true;
            else
                us = false;
            if (us && ps & em & cps)
                loginbtn.IsEnabled = true;
            else
                loginbtn.IsEnabled = false;
        }

      
        private void email_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (email.Text != "")
                em = true;
            else
                em = false;
            if (us && ps & em & cps)
                loginbtn.IsEnabled = true;
            else
                loginbtn.IsEnabled = false;
        }

     

        private void Quitbtn_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Exit();
        }

        private void TextBlock_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Hand, 0);
        }

        private void TextBlock_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 0);
        }

        private void password_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (password.Password != "")
                ps = true;
            else
                ps = false;
            if (us && ps & em & cps)
                loginbtn.IsEnabled = true;
            else
                loginbtn.IsEnabled = false;
        }

        private void cpassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (cpassword.Password != "")
                cps = true;
            else
                cps = false;
            if (us && ps & em & cps)
                loginbtn.IsEnabled = true;
            else
                loginbtn.IsEnabled = false;
        }

        public Register()
        {
            this.InitializeComponent();
        }

        private void TextBlock_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(Login));
        }


        private void playBtn_Click(object sender, RoutedEventArgs e)
        {
            User user = SqlHelper.AddUser(username.Text, password.Password, email.Text);

            if (password.Password != cpassword.Password)
            {
                regerr.Text = "the passwords don'nt match. try again";
                regerr.Visibility = Visibility.Visible;
            }
            else {

                if (user == null)
                {
                    regerr.Text = "User already exists. try again";
                    regerr.Visibility = Visibility.Visible;
                }
                else
                    Frame.Navigate(typeof(Login));
            }
            
        }
    }
}

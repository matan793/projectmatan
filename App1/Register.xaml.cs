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

using Database.Models;
using Database;
using Windows.Storage.Provider;
using DataBase;
using Windows.ApplicationModel.UserDataTasks;
// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SpaceInvaders
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Register : Page
    {
        private bool us = false, ps = false, em = false, cps = false;
        private string varcode = "";

        private void username_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (username.Text != "")
                us = true;
            else
                us = false;
            if (us && ps & em & cps)
                Regbtn.IsEnabled = true;
            else
                Regbtn.IsEnabled = false;
        }

      
        private void email_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (email.Text != "")
                em = true;
            else
                em = false;
            if (us && ps & em & cps)
                Regbtn.IsEnabled = true;
            else
                Regbtn.IsEnabled = false;
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
                Regbtn.IsEnabled = true;
            else
                Regbtn.IsEnabled = false;
        }

        private void cpassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (cpassword.Password != "")
                cps = true;
            else
                cps = false;
            if (us && ps & em & cps)
                Regbtn.IsEnabled = true;
            else
                Regbtn.IsEnabled = false;
        }

        public Register()
        {
            this.InitializeComponent();
        }

        private void TextBlock_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(Login));
        }
        public bool CheackPassword(out string exception)
        {
            exception = "";
            if (password.Password.Length < 8)
            {
                exception = "The password must include at least 8 chars.";
                return false;
            }
            if (password.Password.Any(ch => !char.IsLetterOrDigit(ch)))
            {
                exception = "The password must include letters or digits.";
                return false;
            }
            if (password.Password.All(Char.IsDigit))
            {
                exception = "The password must include letters.";
                return false;
            }
            if (password.Password.All(Char.IsLetter))
            {
                exception = "The password must include digits.";
                return false;
            }
            if (!password.Password.Any(Char.IsUpper))
            {
                exception = "the password must include one or more capitale latter in it.";
                return false;
            }
            return true;
        }

        private void varcodetxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(varcodetxt.Text == varcode)
            {
                User user = SqlHelper.AddUser(username.Text, password.Password, email.Text);
                SqlHelper.AddProduct(user.Id, 0);
             
                Frame.Navigate(typeof(Login));
            }
        }

        private void backbtn_Click(object sender, RoutedEventArgs e)
        {
            verifygrid.Visibility = Visibility.Collapsed;
            EnableAll();
        }

        private void DisableAll()
        {
            password.IsEnabled= false;
            cpassword.IsEnabled= false;
            email.IsEnabled= false;
            username.IsEnabled= false;

            Regbtn.IsEnabled= false;
            Regbtn.Visibility= Visibility.Collapsed;
        }
        private void EnableAll()
        {
            password.IsEnabled = true;
            cpassword.IsEnabled = true;
            email.IsEnabled = true;
            username.IsEnabled = true;

            Regbtn.IsEnabled = true;
            Regbtn.Visibility = Visibility.Visible;

        }
        private void RegBtn_Click(object sender, RoutedEventArgs e)
        {
            string exp = "";
            if (password.Password != cpassword.Password)
            {
                regerr.Text = "the passwords don'nt match. try again";
                regerr.Visibility = Visibility.Visible;
            }
            else if (!EmailManager.IsCurrect(email.Text))
            {
                regerr.Text = "Mail is in curect pleas try again.";
                regerr.Visibility = Visibility.Visible;
            }
            else if (!CheackPassword(out exp))
            {
                regerr.Text = exp;
                regerr.Visibility = Visibility.Visible;
            }
            else if (SqlHelper.IsExists(SqlHelper.UserRowType.Username, username.Text))
            {
                regerr.Text = "Username already exist.";
                regerr.Visibility = Visibility.Visible;
            }
            else if (SqlHelper.IsExists(SqlHelper.UserRowType.Mail, email.Text))
            {
                regerr.Text = "Mail already exist.";
                regerr.Visibility = Visibility.Visible;
            }
            else
            {
                varcode = EmailManager.GetCode(8);
                EmailManager.Send(email.Text, "varefication code", $"Hello {username.Text}, your varefication code is: {varcode}");
                verifygrid.Visibility = Visibility.Visible;
                DisableAll();
            }
            
        }
    }
}

using Database.Models;
using DataBase;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SpaceInvaders
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ResetPasswordPage : Page
    {
        private string email = "";
        public ResetPasswordPage()
        {
            this.InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            email = (string)e.Parameter;
        }
        private void Quitbtn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();

        }

        private void resetbtn_Click(object sender, RoutedEventArgs e)
        {
            string exp = "";
            if(newpass.Password != confirmpass.Password)
            {
                reseterr.Text = "passwords are not matching.";
                reseterr.Visibility= Visibility.Visible;
            }
            if(!CheackPassword(out exp))
            {
                reseterr.Text = exp;
                reseterr.Visibility= Visibility.Visible;
            }
            else
            {
                User tempuser = SqlHelper.GetUserByRow(SqlHelper.UserRowType.Mail, email);
                SqlHelper.UpdatePassword(tempuser.Id, newpass.Password);
                Frame.Navigate(typeof(Login));
            }
        }

        private void newpass_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if(newpass.Password != "" && confirmpass.Password != "")
                resetbtn.IsEnabled = true;
            else 
                resetbtn.IsEnabled = false;
        }
        public bool CheackPassword(out string exception)
        {
            exception = "";
            if (newpass.Password.Length < 8)
            {
                exception = "The password must include at least 8 chars.";
                return false;
            }
            if (newpass.Password.Any(ch => !char.IsLetterOrDigit(ch)))
            {
                exception = "The password must include letters or digits.";
                return false;
            }
            if (newpass.Password.All(Char.IsDigit))
            {
                exception = "The password must include letters.";
                return false;
            }
            if (newpass.Password.All(Char.IsLetter))
            {
                exception = "The password must include digits.";
                return false;
            }
            if (!newpass.Password.Any(Char.IsUpper))
            {
                exception = "the password must include one or more capitale latter in it.";
                return false;
            }
            return true;
        }
    }
}

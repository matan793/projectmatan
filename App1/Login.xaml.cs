using Database;
using Database.Models;
using DataBase;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Chat;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.ViewManagement;
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
    public sealed partial class Login : Page
    {
        bool us = false;
        bool ps = false;
        private string varcode;
        /// <summary>
        /// פעולה בונה
        /// </summary>
        public Login()
        {
            this.InitializeComponent();
        }
        /// <summary>
        /// פעולה אשר נקראת כאשר המשתמש לוחץ על כפתור הכניבה למשתמש
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            User user = SqlHelper.GetUser(username.Text, password.Password);
            if(user == null)
            {
                logerr.Visibility = Visibility.Visible; return;
            }
            Session.User = user;
            Frame.Navigate(typeof(MainPage));
        }
        /// <summary>
        /// פעולה אשר נקראת כל פעם שתיבת הטקסט של השם משתמש משתנה
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void username_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(username.Text != "")
                us = true;
            else
            us = false;
            if(us && ps)
                loginbtn.IsEnabled = true;
            else
                loginbtn.IsEnabled = false;
        }    
        /// <summary>
        /// פעולה אשר נקראת כאשר המשתמש לוחץ על כפתור היציאה
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Quitbtn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();
        }
        /// <summary>
        /// פעולה אשר נקראת כאשר המשתמש לוחץ על תיבת הטקסט של הרשמה
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBlock_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(Register));
        }
        /// <summary>
        /// פעולה אשר נקראת בכל פעם שהעכבר יוצא מתכום של תיבת טקסט
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBlock_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Hand, 0);
        }
        /// <summary>
        /// פעולה אשר נקראת בכל פעם שהעכבר יוצא מתכום של תיבת טקסט
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBlock_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 0);
        }

        /// <summary>
        /// פעולה אשר נקראת כל פעם שתיבת הסיסמא משתנה
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void password_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (password.Password != "")
                ps = true;
            else
                ps = false;
            if (us && ps)
                loginbtn.IsEnabled = true;
            else
                loginbtn.IsEnabled = false;
        }
        /// <summary>
        /// פעולה אשר נקראת כאשר הדף טען
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Windows.UI.ViewManagement.ApplicationView.GetForCurrentView().TryEnterFullScreenMode();
        }
        /// <summary>
        /// פעולה אשר נקראת כאשר המשתמש לוחץ על תיבת הטקבט של שכחתי סיסמא
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ForgotPass_Tapped(object sender, TappedRoutedEventArgs e)
        {
           emailgrid.Visibility = Visibility.Visible;
           fpassbtn.Visibility = Visibility.Collapsed;
        }
        /// <summary>
        /// פעולה אשר נקראת עאשר המשתמש לוחץ על העפתור חזרה הביתה
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backbtn_Click(object sender, RoutedEventArgs e)
        {
            emailgrid.Visibility = Visibility.Collapsed;
            fpassbtn.Visibility = Visibility.Visible;
        }
        /// <summary>
        /// פעולה אשר נקראת כאשר המשתמש לוחץ על הלחצן המשך
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void continuebtn_Click(object sender, RoutedEventArgs e)
        {
            if (SqlHelper.IsExists(SqlHelper.UserRowType.Mail, emailbox.Text))
            {
                //varcode = EmailManager.GetCode(8);
                //EmailManager.Send(emailbox.Text, "varefication code", $"Hello {username.Text}, your varefication code is: {varcode}");
                //emailgrid.Visibility = Visibility.Collapsed;
                //verifygrid.Visibility= Visibility.Visible;
                Frame.Navigate(typeof(ResetPasswordPage), emailbox.Text);
            }
            else
            {
                emailerr.Text = "there is no user registered on this email plz try again";
            }
        }
        /// <summary>
        /// פעולה אשר נקראת כל פעם שתיבת הטקסט של האישור קוד משתנה
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void verifybox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(verifybox.Text == varcode)
            {
               Frame.Navigate(typeof(ResetPasswordPage), emailbox.Text);
            }
        }
        /// <summary>
        /// פעולה אשר נקראת עאשר המשתמש לוחץ על העפתור חזרה השני הביתה
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backbtn2_Click(object sender, RoutedEventArgs e)
        {
            verifygrid.Visibility = Visibility.Collapsed;
            emailgrid.Visibility = Visibility.Visible;
        }

       
    }
}

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
    public sealed partial class InformationPage : Page
    {
        public InformationPage()
        {
            this.InitializeComponent();
        }

     

        private void howBtn_Click(object sender, RoutedEventArgs e)
        {
            settingsGrid.Visibility = Visibility.Collapsed;
            about_name.Visibility = Visibility.Collapsed;
            about_me.Visibility = Visibility.Collapsed;
            howGridView.Visibility = Visibility.Visible;

            RequestedTheme = ElementTheme.Light;
            aboutBtn.RequestedTheme = ElementTheme.Light;
            howBtn.RequestedTheme = ElementTheme.Dark;
        }

        private void aboutBtn_Click(object sender, RoutedEventArgs e)
        {
            settingsGrid.Visibility = Visibility.Collapsed;
            about_name.Visibility = Visibility.Visible;
            about_me.Visibility = Visibility.Visible;
            howGridView.Visibility = Visibility.Collapsed;

            aboutBtn.RequestedTheme = ElementTheme.Dark;
            howBtn.RequestedTheme = ElementTheme.Light;
        }

        private void settingsBackBtn_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private void playBtn_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(GamePage));
        }
    }
}

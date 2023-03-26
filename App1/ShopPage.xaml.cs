﻿using Database.Models;
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
    public sealed partial class ShopPage : Page
    {
        
        public ShopPage()
        {
            this.InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            List<Product> skins = SqlHelper.GetShop(Session.User.Id);
            foreach (Product skin in skins)
            {
                string str = $"ms-appx:///Assets/Spaceship1/{skin.Skin}.png";

                mylist.Items.Add(skin);
                //mylist.Items.Add(skin.Image);
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

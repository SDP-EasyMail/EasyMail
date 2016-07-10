﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace MidtermProject
{
    public sealed partial class Account : Page
    {
        ApplicationDataContainer localseetings = Windows.Storage.ApplicationData.Current.LocalSettings;
        public Account()
        {
            this.InitializeComponent();
            //var viewTitleBar = Windows.UI.ViewManagement.ApplicationView.GetForCurrentView().TitleBar;
            //viewTitleBar.BackgroundColor = Windows.UI.Colors.CornflowerBlue;
            //viewTitleBar.ButtonBackgroundColor = Windows.UI.Colors.CornflowerBlue;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;

            if (rootFrame.CanGoBack)
            {
                // Show UI in title bar if opted-in and in-app backstack is not empty.
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
                    AppViewBackButtonVisibility.Visible;
            }
            else
            {
                // Remove the UI from the title bar if in-app back stack is empty.
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
                    AppViewBackButtonVisibility.Collapsed;
            }
        }
        
        //修改用户账号信息
        async private void Register_Click(object sender, RoutedEventArgs e)
        {

            if (Username.Password == "")
            {
                var i = new MessageDialog("Please enter the username!").ShowAsync();
                return;
            }

            if (newPassword.Password != Configure.Password || newPassword.Password == "")
            {
                var i = new MessageDialog("Please check the password!").ShowAsync();
                return;
            }

            if (Agree.IsChecked == false) return;

            string data =  localseetings.Values["user"].ToString() + '\t' + Username.Password + '\t' + newPassword.Password;
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response = await httpClient.PostAsync("http://sunzhongyang.com:7000/change", new StringContent(data));
            string receive = await response.Content.ReadAsStringAsync();
            var c = new MessageDialog(receive).ShowAsync();
            if (receive == "change success") Frame.Navigate(typeof(MainPage), "");
        }

    }

}

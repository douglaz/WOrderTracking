﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.ApplicationSettings;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using WOrderTracking.Model;
using WOrderTracking.Persistence;

namespace WOrderTracking
{
    public sealed partial class Settings : UserControl
    {
        private static AppSettingsDAO settingsDAO = new AppSettingsDAO();

        public Settings()
        {
            this.InitializeComponent();
            var settings = settingsDAO.Get();
            NotificationsSwitch.DataContext = settings;
            NotificationsIntervalSlider.DataContext = settings;
        }


        private void MySettingsBackClicked(object sender, RoutedEventArgs e)
        {
            if (this.Parent.GetType() == typeof(Popup))
            {
                ((Popup)this.Parent).IsOpen = false;
            }
            SettingsPane.Show();
        }

        private void ToggleSwitch_Toggled_1(object sender, RoutedEventArgs e)
        {
            if (NotificationsSwitch != null)
                settingsDAO.Save(NotificationsSwitch.DataContext as AppSettings);
        }

        private void Slider_ValueChanged_1(object sender, RangeBaseValueChangedEventArgs e)
        {
            if (NotificationsSwitch != null)
                settingsDAO.Save(NotificationsSwitch.DataContext as AppSettings);
        }


    }
}
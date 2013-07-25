using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using WOrderTracking.Model;

namespace WOrderTracking.Persistence
{
    public class AppSettingsDAO
    {
        private static Windows.Storage.ApplicationDataContainer roamingSettings =
                Windows.Storage.ApplicationData.Current.RoamingSettings;

        public AppSettings Get()
        {
            var settingsValue = roamingSettings.Values["settings"] as ApplicationDataCompositeValue;
            if (settingsValue != null)
                return new AppSettings() { EnableNotifications = (bool)settingsValue["enableNotifications"], NotificationsInterval = (int)settingsValue["notificationsInterval"] };
            else
                return new AppSettings();
        }

        public void Save(AppSettings settings)
        {
            var settingsValue = new ApplicationDataCompositeValue();
            settingsValue["enableNotifications"] = settings.EnableNotifications;
            settingsValue["notificationsInterval"] = settings.NotificationsInterval;
            roamingSettings.Values["settings"] = settingsValue;
        }
    }
}

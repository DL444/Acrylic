using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.UI;
using Windows.UI.ViewManagement;

namespace AcrylicGlass
{
    static class Utilities
    {
        public static void ExtendAcrylicIntoTitleBar()
        {
            CoreApplication.GetCurrentView().TitleBar.ExtendViewIntoTitleBar = true;
            ApplicationViewTitleBar titleBar = ApplicationView.GetForCurrentView().TitleBar;
            titleBar.ButtonBackgroundColor = Colors.Transparent;
            titleBar.ButtonInactiveBackgroundColor = Colors.Transparent;
        }

        public static void SaveSetting(string key, string value)
        {
            Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            localSettings.Values[key] = value;
        }
        public static bool LoadSetting(string key, out string value)
        {
            Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            string val = localSettings.Values[key] as string;
            if (val == null) { value = ""; return false; }
            else { value = val; return true; }
        }

        public static Color GetColor(string hex)
        {
            hex = hex.Replace("#", string.Empty);
            if(hex.Length < 8)
            {
                throw new ArgumentException("Input string is not a valid hexidecimal color value.");
            }
            try
            {
                byte a = (byte)(Convert.ToUInt32(hex.Substring(0, 2), 16));
                byte r = (byte)(Convert.ToUInt32(hex.Substring(2, 2), 16));
                byte g = (byte)(Convert.ToUInt32(hex.Substring(4, 2), 16));
                byte b = (byte)(Convert.ToUInt32(hex.Substring(6, 2), 16));
                return Color.FromArgb(a, r, g, b);
            }
            catch (FormatException)
            {
                throw new ArgumentException("Input string is not a valid hexidecimal color value.");
            }
        }
    }

}

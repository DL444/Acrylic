using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace AcrylicGlass
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        bool settingsShown = false;

        public MainPage()
        {
            this.InitializeComponent();

            if (Utilities.LoadSetting("FirstRun", out string str) == false || str == "true")
            {
                ContentDialog firstRunDialog = new ContentDialog();
                firstRunDialog.Width = 400;
                firstRunDialog.Title = "Hello!";
                firstRunDialog.Content = "Right-click on the window, press Ctrl key, or press and hold \nwith finger to change color and other things!\n\nHope you enjoy this!";
                firstRunDialog.CloseButtonText = "Let's Go!";
                var t = firstRunDialog.ShowAsync();
                Utilities.SaveSetting("FirstRun", "false");
            }

            CoreWindow.GetForCurrentThread().KeyDown += MainPage_KeyDown;
            if(Utilities.LoadSetting("Fallback", out string fallbackStr) == true)
            {
                var vm = this.DataContext as GlassViewModel;
                vm.Fallback = fallbackStr == "True" ? true : false;
                Utilities.LoadSetting("TintColor", out string tintColorStr);
                vm.TintColor = Utilities.GetColor(tintColorStr);
                Utilities.LoadSetting("TintOpacity", out string opacityStr);
                vm.TintOpacity = double.Parse(opacityStr);
                Utilities.LoadSetting("FallbackOverride", out string fallbackOverrideStr);
                vm.FallbackColorOverride = fallbackOverrideStr == "True" ? true : false;
                Utilities.LoadSetting("FallbackColor", out string fallbackColorStr);
                vm.FallbackColor = Utilities.GetColor(fallbackColorStr);
            }
        }

        private void MainPage_KeyDown(CoreWindow sender, KeyEventArgs args)
        {
            if(args.VirtualKey == Windows.System.VirtualKey.Control)
            {
                if (SettingsGrid.Visibility == Visibility.Collapsed)
                {
                    SettingsGrid.Visibility = Visibility.Visible;
                }
                else
                {
                    SettingsGrid.Visibility = Visibility.Collapsed;
                }
            }
        }

        private async void Grid_RightTapped(object sender, RightTappedRoutedEventArgs e)
        {
            if(SettingsGrid.Visibility == Visibility.Collapsed)
            {
                SettingsGrid.Visibility = Visibility.Visible;
                await System.Threading.Tasks.Task.Delay(100);
                settingsShown = true;
            }
            else
            {
                SettingsGrid.Visibility = Visibility.Collapsed;
            }
        }

        private async void OverrideFallbackToggle_Toggled(object sender, RoutedEventArgs e)
        {
            if((sender as ToggleSwitch).IsOn == true && settingsShown == true)
            {
                await System.Threading.Tasks.Task.Delay(500);
                SettingsScroll.ChangeView(0, SettingsScroll.ScrollableHeight, 1);
            }
        }
    }
}

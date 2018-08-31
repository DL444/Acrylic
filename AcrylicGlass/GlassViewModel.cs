using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace AcrylicGlass
{
    class GlassViewModel : INotifyPropertyChanged
    {
        bool _fallbackColorOverride = false;

        public AcrylicBrush Brush { get; } = new AcrylicBrush();

        public Color TintColor
        {
            get => Brush.TintColor;
            set
            {
                Brush.TintColor = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TintColor"));
                if(!FallbackColorOverride)
                {
                    FallbackColor = value;
                }
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Brush"));
            }
        }
        public double TintOpacity
        {
            get => Brush.TintOpacity;
            set
            {
                Brush.TintOpacity = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TintOpacity"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Brush"));
            }
        }
        public Color FallbackColor
        {
            get => Brush.FallbackColor;
            set
            {
                Brush.FallbackColor = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("FallbackColor"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Brush"));
            }
        }
        public bool Fallback
        {
            get => Brush.AlwaysUseFallback;
            set
            {
                Brush.AlwaysUseFallback = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Fallback"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Brush"));
            }
        }

        public bool FallbackColorOverride
        {
            get => _fallbackColorOverride;
            set
            {
                _fallbackColorOverride = value;
                if(value == false)
                {
                    FallbackColor = TintColor;
                }
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("FallbackColorOverride"));
            }
        }

        public ICommand Reset => new MicroMvvm.RelayCommand(ResetEx);

        public GlassViewModel()
        {
            Brush.BackgroundSource = AcrylicBackgroundSource.HostBackdrop;
            Brush.FallbackColor = Colors.White;
            Brush.AlwaysUseFallback = false;
            Brush.TintColor = Colors.White;
            Brush.TintOpacity = 0.6;
        }

        void ResetEx()
        {
            FallbackColorOverride = false;
            FallbackColor = Colors.White;
            Fallback = false;
            TintColor = Colors.White;
            TintOpacity = 0.6;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

    class BoolInvertConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return !((bool)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return !((bool)value);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Lrc.Model;

namespace Lrc.ViewModel
{
    public class GameSettingsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string retVal = string.Empty;
            var setting = value as GameSettings;
            if (setting != null)
            {
               retVal = $"{setting.Players} {nameof(setting.Players) } x {setting.Games} {nameof(setting.Games)}";
            }

            return retVal;
            
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

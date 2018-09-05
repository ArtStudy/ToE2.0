using Assets.Core.Game.Data;
using Assets.Core.LevelsStructureInterfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace GameСreator.Converters
{
    public class DataItemToValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ViewModel.MainViewModel.GDStatic.Bosses.Find((item)=> item.Value == value );
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }

            return ((dynamic)value).Value;
        }
    }
}

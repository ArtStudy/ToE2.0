using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Core.BindingData.Converters
{
    public interface IValueConverter
    {
         object Convert(object value, Type targetType, object parameter,  System.Globalization.CultureInfo culture);
         object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture);
    }
}

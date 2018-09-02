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
    public class ListIntToListTypeValue : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {


            return new  ObservableCollection<TypeValue<int>>(((List<int>)value).ConvertAll((item)=> new  TypeValue<int>(item))) ;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {


            return ((ObservableCollection<TypeValue<int>>)value).ToList().ConvertAll((item)=> item.Value);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UI;

namespace Assets.Core.BindingData
{
    public partial class DependencyProperty
    {
        public static readonly DependencyProperty textonTextProperty = DependencyProperty.Register("text", typeof(string), typeof(Text), textonTextPropertyChanged);

        public static readonly DependencyProperty textonInputFieldtProperty = DependencyProperty.Register("text", typeof(string), typeof(InputField), textonInputFieldPropertyChanged);


        private static void textonTextPropertyChanged(object d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != e.OldValue)
                ((Text)d).text = (string)e.NewValue;
        }
        private static void textonInputFieldPropertyChanged(object d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != e.OldValue)
                ((InputField)d).text = (string)e.NewValue;
        }
    }
}

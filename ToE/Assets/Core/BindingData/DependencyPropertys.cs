using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Core.BindingData
{
    public partial class DependencyProperty
    {
        public static readonly DependencyProperty textonTextProperty = DependencyProperty.Register("text", typeof(string), typeof(Text), textonTextPropertyChanged);

        public static readonly DependencyProperty textonInputFieldtProperty = DependencyProperty.Register("text", typeof(string), typeof(InputField), textonInputFieldPropertyChanged);


        public static readonly DependencyProperty enabledonMonoBehaviourProperty = DependencyProperty.Register("enabled", typeof(bool), typeof(Behaviour), enabledonMonoBehaviourPropertyChanged);

        public static readonly DependencyProperty activedonGameObjectProperty = DependencyProperty.Register("active", typeof(bool), typeof(GameObject), activedonGameObjectPropertyChanged);

        private static void activedonGameObjectPropertyChanged(object d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != e.OldValue)
                ((GameObject)d).SetActive( (bool)e.NewValue);
        }

        private static void enabledonMonoBehaviourPropertyChanged(object d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != e.OldValue)
                ((Behaviour)d).enabled = (bool)e.NewValue;
        }

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

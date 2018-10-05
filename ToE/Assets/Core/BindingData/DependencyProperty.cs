using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Core.BindingData
{
    public partial class DependencyProperty
    {
        public string Name { get; private set; }
        Type Valuetype;
       public     PropertyChangedCallback propertyChangedCallback { get; private set; }
        public static Dictionary<Type, DependencyProperty> DependencyPropertyData { get; private set; }


        public static DependencyProperty Register(string name, Type valuetype, Type objecttype, PropertyChangedCallback propertyChangedCallback )
        {
            var dp = new DependencyProperty { Name = name, propertyChangedCallback = propertyChangedCallback, Valuetype = valuetype };
            if(DependencyPropertyData == null)
                DependencyPropertyData  = new Dictionary<Type, DependencyProperty>();
            DependencyPropertyData[objecttype] = dp;
            return dp;
        }

    }
    public delegate void PropertyChangedCallback(object d, DependencyPropertyChangedEventArgs e);

    public class DependencyPropertyChangedEventArgs
    {
        public object NewValue;
        public object OldValue;
    }

}

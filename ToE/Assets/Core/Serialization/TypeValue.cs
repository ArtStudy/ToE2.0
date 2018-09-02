using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Core.Serialization
{
    public class TypeValue<T> :  INotifyPropertyChanged
    {
        public TypeValue()
        { 
        }
        public TypeValue(T s)
        {
            _value = s;
        }
        public T Value { get { return _value; } set { _value = value; OnPropertyChanged("Value"); } }
        T _value;

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

    
    }
}

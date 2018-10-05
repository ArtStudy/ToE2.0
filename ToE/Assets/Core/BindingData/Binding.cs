using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Core.BindingData
{
    public class Binding
    {
        bool istwoway = false;
         DependencyProperty dp;
        string path;
        object UIobj;
        INotifyPropertyChanged obj;
        public static List<Binding> DependencyPropertyData { get; } = new List<Binding>();
        public static Binding Register(object UIobj, DependencyProperty dp, INotifyPropertyChanged obj, String Path)
        {
          
            Binding binding = new Binding(UIobj, obj);
          
            binding.dp = dp;
            binding.path = Path;
          
  
            binding.obj = obj;
            DependencyPropertyData.Add(binding);
            return binding;
        }

        Binding(object UIobj, INotifyPropertyChanged obj)
        {
            obj.PropertyChanged += Obj_PropertyChanged;

            this.istwoway = (UIobj.GetType().GetProperty("onValueChanged") != null);
            this.UIobj = UIobj;
            if (this.istwoway)
            {
                var uiobjtype = UIobj.GetType();
                var onValueChangedProperty = uiobjtype.GetProperty("onValueChanged");
                var AddListenerMethod = onValueChangedProperty.PropertyType.GetMethod("AddListener");
        
                var onValueChangedPropertyvalue = onValueChangedProperty.GetValue(UIobj);
                AddListenerMethod.Invoke(onValueChangedPropertyvalue, new object[] { (object)new UnityAction<string>((item) => ValueChangeCheck(item)) });
            }
            
        }
        public void ValueChangeCheck(string s)
        {
            //  Debug.Log(s);
            // Debug.Log(this.UIobj.GetType().GetProperty(dp.Name).GetValue(this.UIobj));
            this.obj.GetType().GetProperty(this.path).SetValue(this.obj, s);
        }

        private  void Obj_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {

               this.dp.propertyChangedCallback(this.UIobj, new DependencyPropertyChangedEventArgs { OldValue = this.UIobj.GetType().GetProperty(this.dp.Name).GetValue(this.UIobj),
                    NewValue = sender.GetType().GetProperty(e.PropertyName).GetValue(sender) }); 
            
        }
    }
}

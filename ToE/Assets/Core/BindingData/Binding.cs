using Assets.Core.BindingData.Converters;
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
        object obj;

        IValueConverter vc;

        public static List<Binding> DependencyPropertyData { get; } = new List<Binding>();
        public static Binding Register(object UIobj, DependencyProperty dp, object obj, String Path, IValueConverter vc = null)
        {

            Binding binding = null;
            if (obj.GetType().GetInterfaces().Contains(typeof(INotifyPropertyChanged)))
            {
                binding = new Binding(UIobj, (INotifyPropertyChanged) obj);
            }
            else
            {
                binding = new Binding(UIobj);
            }

            binding.dp = dp;
            binding.path = Path;
            binding.vc = vc;


            binding.obj = obj;
            DependencyPropertyData.Add(binding);
            binding.UpdateOnModelBinding();
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
        Binding(object UIobj)
        {

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

            System.Object res = s;
            if (vc != null)
            {
                res = vc.ConvertBack(res, res.GetType(), null, null);
            }

            this.obj.GetType().GetProperty(this.path).SetValue(this.obj, res);

        }

        private void Obj_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            object newv1 = sender;
            Type currentType = sender.GetType();
            var r = path.Split('.');
            if (r.Contains(e.PropertyName))
            {


                foreach (string propertyName in r)
                {

                    //   newv1 = sender.GetType().GetProperty(propertyName).GetValue(sender);

                    PropertyInfo property = currentType.GetProperty(propertyName);
                    newv1 = property.GetValue(newv1);
                    currentType = property.PropertyType;

                }

                if (vc != null)
                {
                    newv1 = vc.Convert(newv1, newv1.GetType(), null, null);
                }
                var propert = this.UIobj.GetType().GetProperty(this.dp.Name);
                this.dp.propertyChangedCallback(this.UIobj, new DependencyPropertyChangedEventArgs
                {
                    OldValue = propert.GetValue(this.UIobj),
                    NewValue = newv1
                });
            }
        }
        public void UpdateOnModelBinding()
        {
            var r = this.path.Split('.');
            Obj_PropertyChanged(this.obj, new PropertyChangedEventArgs(r[0]));
        }
    }

}
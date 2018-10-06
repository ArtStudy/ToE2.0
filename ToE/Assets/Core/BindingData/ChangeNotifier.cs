using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Core.BindingData
{
    public class ChangeNotifier : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = "")
        {
             field = value;
            OnPropertyChanged(propertyName);
        }

        protected void SetProperty(Action setaction, [CallerMemberName] string propertyName = "")
        {
            setaction.Invoke();
            OnPropertyChanged(propertyName);
        }

        private void OnPropertyChanged(String propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

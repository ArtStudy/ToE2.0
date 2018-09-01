using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using GameСreator.ToePac;
using System;
using System.ComponentModel;
using System.Windows.Input;

namespace GameСreator.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class AddResourseViewModel : ViewModelBase
    {
        public Item ThisItem { get; } = new Item();
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public AddResourseViewModel()
        {
            this.SaveResources = new RelayCommand(SaveResourcesAction, SaveResourcesCanEx);
            this.CancelСreation = new RelayCommand(CancelСreationAction, CancelСreationCanEx);

            ThisItem.PropertyChanged += ThisItem_PropertyChanged;
            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}
        }

        private void ThisItem_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Name":
                    this.ThisItem.Identifier = (UInt64) this.ThisItem.Name.GetUInt64HashCode();
                    break;
            }
        }

        private bool CancelСreationCanEx() => true;
        private void CancelСreationAction()
        {
            Messenger.Default.Send<NavigatorPageMessege>(new NavigatorPageMessege("AddResourseWindowClose"));
        }
    

        private bool SaveResourcesCanEx() => ThisItem.Name.Length > 0;
       

        private void SaveResourcesAction()
        {
            Messenger.Default.Send<Item>(this.ThisItem);

            Messenger.Default.Send<NavigatorPageMessege>(new NavigatorPageMessege("AddResourseWindowClose"));
        }
    





        /// <summary>
        /// Сохранить ресурс
        /// </summary>
        public ICommand SaveResources { get; }

        /// <summary>
        /// Отменить создание
        /// </summary>
        public ICommand CancelСreation { get; }

      
    }
}
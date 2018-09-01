using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using GameСreator.API.Boss;
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
    public class AddLevelViewModel : ViewModelBase
    {
        public LevelV1 Level { get; } = new LevelV1();
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public AddLevelViewModel()
        {
            this.Save= new RelayCommand(SaveAction, SaveCanEx);
            this.CancelСreation = new RelayCommand(CancelСreationAction, CancelСreationCanEx);

           // Level.PropertyChanged += Boss_PropertyChanged; ;
            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}
        }

        private void Boss_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
           // throw new NotImplementedException();
        }



        private bool CancelСreationCanEx() => true;
        private void CancelСreationAction()
        {
            Messenger.Default.Send<NavigatorPageMessege>(new NavigatorPageMessege("AddLevelWindowClose"));
        }
    

        private bool SaveCanEx() => Level.NameLavel?.Length > 0 && Level.IDLevel > 0 ;
       

        private void SaveAction()
        {
            Messenger.Default.Send<MessegeResourse<LevelV1>>(new MessegeResourse<LevelV1>(Level, ActionItem.Add));

            Messenger.Default.Send<NavigatorPageMessege>(new NavigatorPageMessege("AddLevelWindowClose"));
        }
    





        /// <summary>
        /// Сохранить босса
        /// </summary>
        public ICommand Save { get; }

        /// <summary>
        /// Отменить создание
        /// </summary>
        public ICommand CancelСreation { get; }

      
    }
}
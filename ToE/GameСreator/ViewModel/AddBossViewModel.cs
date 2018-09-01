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
    public class AddBossViewModel : ViewModelBase
    {
        public BossV1 Boss { get; } = new BossV1() {};
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public AddBossViewModel()
        {
            this.SaveBoss= new RelayCommand(SaveBossAction, SaveBossCanEx);
            this.CancelСreation = new RelayCommand(CancelСreationAction, CancelСreationCanEx);

            Boss.PropertyChanged += Boss_PropertyChanged; ;
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
            Messenger.Default.Send<NavigatorPageMessege>(new NavigatorPageMessege("AddBossWindowClose"));
        }
    

        private bool SaveBossCanEx() => Boss.Name?.Length > 0 && Boss.ID > 0 ;
       

        private void SaveBossAction()
        {
            Messenger.Default.Send<MessegeResourse<BossV1>>(new MessegeResourse<BossV1>(Boss, ActionItem.Add));

            Messenger.Default.Send<NavigatorPageMessege>(new NavigatorPageMessege("AddBossWindowClose"));
        }
    





        /// <summary>
        /// Сохранить босса
        /// </summary>
        public ICommand SaveBoss { get; }

        /// <summary>
        /// Отменить создание
        /// </summary>
        public ICommand CancelСreation { get; }

      
    }
}
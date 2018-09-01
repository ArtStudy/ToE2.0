/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:GameСreator"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;


namespace GameСreator.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            ////if (ViewModelBase.IsInDesignModeStatic)
            ////{
            ////    // Create design time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DesignDataService>();
            ////}
            ////else
            ////{
            ////    // Create run time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DataService>();
            ////}

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<AddResourseViewModel>();
            SimpleIoc.Default.Register<AddBossViewModel>();
            SimpleIoc.Default.Register<AddLevelViewModel>();
        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>("Main");
            }
        }
              public AddResourseViewModel AddResourseViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AddResourseViewModel>(System.Guid.NewGuid().ToString());
            }
        }
        public AddBossViewModel AddBosseViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AddBossViewModel>(System.Guid.NewGuid().ToString());
            }
        }
        public AddLevelViewModel AddLevelViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AddLevelViewModel>(System.Guid.NewGuid().ToString());
            }
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}
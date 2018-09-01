using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using GameСreator.API.Boss;
using GameСreator.ToePac;
using System;
using System.Collections.ObjectModel;
using System.IO;
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
    public class MainViewModel : ViewModelBase
    {
        string CurrentPage = string.Empty;
        FileStream currentFile;
        private ObservableCollection<BossV1> _bosses;
        private ObservableCollection<LevelV1> _levels;

        public PAC ThisPac { get; private set; }
        public ListResourse Items { get => ThisPac.Items; }
        public ObservableCollection<BossV1> Bosses { get => _bosses; set { _bosses = value; this.RaisePropertyChanged("Bosses"); }  }
        public ObservableCollection<LevelV1> Levels { get => _levels; set { _levels = value; this.RaisePropertyChanged("Levels"); } }
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            Messenger.Default.Register<Item>(this, AddNewResourse);
            Messenger.Default.Register<MessegeResourse<BossV1>>(this, AddNewBoss);
            Messenger.Default.Register<MessegeResourse<LevelV1>>(this, AddNewLevel);

            this.CreateNewPackage = new RelayCommand(CreateNewPackageAction, CreateNewPackageCanEx);
            this.CreateNewBoss = new RelayCommand(CreateNewBossAction, CreateNewBossCanEx);
            this.OpenResources = new RelayCommand(OpenResourcesAction, OpenResourcesCanEx);
            this.OpenBosses = new RelayCommand(OpenBossesAction, OpenBossesCanEx);
            this.OpenPackage = new RelayCommand(OpenPackageAction, OpenPackageCanEx);
            this.OpenLevels = new RelayCommand(OpenLevelsAction, OpenLevelsCanEx);
            this.CreateNewResourse = new RelayCommand(CreateNewResourseAction, CreateNewResourseCanEx);
            this.CreateNewLevel = new RelayCommand(CreateNewLevelAction, CreateNewLevelCanEx);
            this.SavePackage = new RelayCommand(SavePackageAction, SavePackageCanEx);
            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}
        }



        /// <summary>
        /// Сохранить пакет
        /// </summary>
        public ICommand CreateNewLevel { get; }


        /// <summary>
        /// Сохранить пакет
        /// </summary>
        public ICommand SavePackage { get; }
        /// <summary>
        /// Открыть уровни пакет
        /// </summary>
        public ICommand OpenLevels { get; }


        /// <summary>
        /// Открыть пакет
        /// </summary>
        public ICommand OpenPackage { get; }

        /// <summary>
        /// Открыть поле русурсов 
        /// </summary>
        public ICommand OpenResources { get; }

        /// <summary>
        /// Открыть поле русурсов 
        /// </summary>
        public ICommand OpenBosses { get; }

        /// <summary>
        /// Создать новый паке
        /// </summary>
        public ICommand CreateNewPackage { get; }
        /// <summary>
        /// Создать новый ресурс
        /// </summary>
        public ICommand CreateNewResourse { get; }
        /// <summary>
        /// Создать новый ресурс
        /// </summary>
        public ICommand CreateNewBoss { get; }

        private void CreateNewPackageAction()
        {
            if (this.ThisPac == null)
            {
                this.ThisPac = new PAC();
            }


            //  this.OpenResources.CanExecute(null);
        }
        private bool CreateNewPackageCanEx() => true;
        /// <summary>
        /// Открыть поле русурсов действие
        /// </summary>
        private void OpenResourcesAction()
        {
            CurrentPage = "ResourcesPage";
            Messenger.Default.Send<NavigatorPageMessege>(new NavigatorPageMessege("ResourcesPage"));

            //   Messenger.Default.Send<PAC>(this.ThisPac);

        }
        private bool OpenResourcesCanEx() => this.ThisPac != null;



        private bool CreateNewResourseCanEx() => this.ThisPac != null && this.CurrentPage == "ResourcesPage";
        private void CreateNewResourseAction()
        {
            Messenger.Default.Send<NavigatorPageMessege>(new NavigatorPageMessege("AddResourseWindowOpen"));
        }


        private void OpenBossesAction()
        {
            UpdateListBosses();
            CurrentPage = "BossesPage";
            Messenger.Default.Send<NavigatorPageMessege>(new NavigatorPageMessege("BossesPage"));

        }

        private bool OpenBossesCanEx() => this.ThisPac != null;


        private bool CreateNewBossCanEx() => this.ThisPac != null && this.CurrentPage == "BossesPage";

        private void CreateNewBossAction()
        {
            Messenger.Default.Send<NavigatorPageMessege>(new NavigatorPageMessege("AddBossWindowOpen"));
        }


        private void SavePackageAction()
        {
            if (currentFile == null)
            {
                Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
                dlg.FileName = "Document"; // Default file name
                dlg.DefaultExt = ".ToePackage"; // Default file extension
                dlg.Filter = "ToePackage documents (*.ToePackage)|*.ToePackage"; // Filter files by extension

                // Show save file dialog box
                Nullable<bool> result = dlg.ShowDialog();

                // Process save file dialog box results
                if (result == true)
                {
                    // Save document
                    string filename = dlg.FileName;
                    currentFile = new FileStream(filename, FileMode.Create);
                    ThisPac.Serialization().WriteTo(currentFile);
                    currentFile.Flush(true);

                }
            }
            else
                currentFile.Position = 0;
            ThisPac.Serialization().WriteTo(currentFile);
            currentFile.Flush(true);

        }

        private bool SavePackageCanEx() => this.ThisPac != null;


        private bool OpenPackageCanEx() => true;

        private void OpenPackageAction()
        {

            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.FileName = "Document"; // Default file name
            dlg.DefaultExt = ".ToePackage"; // Default file extension
            dlg.Filter = "ToePackage documents (*.ToePackage)|*.ToePackage"; // Filter files by extension

            // Show save file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process save file dialog box results
            if (result == true)
            {
                // Save document
                string filename = dlg.FileName;
                currentFile?.Dispose();
                currentFile = new FileStream(filename, FileMode.Open);
                MemoryStream ms = new MemoryStream();
                ///  currentFile.CopyTo(ms);
                this.ThisPac = new PAC(currentFile);
            }
            UpdateListBosses();
            UpdateListLevels();
        }

        private void AddNewBoss(MessegeResourse<BossV1> obj)
        {
            switch (obj.Action)
            {
                case ActionItem.Add:
                    ThisPac.Items.AddRange(obj.Value.ToListResourse());
                    UpdateListBosses();
                    break;
            }
        }

        private void AddNewResourse(Item obj)
        {
            Items.Add(obj);
            this.RaisePropertyChanged("Items");
        }
        private void AddNewLevel(MessegeResourse<LevelV1> obj)
        {
            switch (obj.Action)
            {
                case ActionItem.Add:
                    ThisPac.Items.AddRange(obj.Value.ToListResourse());
                    UpdateListLevels();
                    break;
            }
        }


        private void UpdateListBosses()
        {
            Bosses = new ObservableCollection<BossV1>();
            var listtemp = ThisPac.Items.GetResourcesByType(FileTypes.Boss);
            listtemp.ForEach((item) => { Bosses.Add(new BossV1(item, ThisPac.Items)); });
        }

        private bool OpenLevelsCanEx() => this.ThisPac != null;
        private void OpenLevelsAction()
        {
            UpdateListLevels();
            CurrentPage = "LevelPage";
            Messenger.Default.Send<NavigatorPageMessege>(new NavigatorPageMessege("LevelPage"));
        }


        private void UpdateListLevels()
        {
            Levels = new ObservableCollection<LevelV1>();
            var listtemp = ThisPac.Items.GetResourcesByType(FileTypes.Lavel);
            listtemp.ForEach((item) => { Levels.Add(new LevelV1(item, ThisPac.Items)); });
        }

        private bool CreateNewLevelCanEx() => this.ThisPac != null && this.CurrentPage == "LevelPage";

        private void CreateNewLevelAction()
        {
            Messenger.Default.Send<NavigatorPageMessege>(new NavigatorPageMessege("AddLevelWindowOpen"));
        }




    }
}
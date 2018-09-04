using Assets.Core.Game.Ages_and_Graphs;
using Assets.Core.Game.Data;
using Assets.Core.ToePac;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using GameСreator.API.Boss;

using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
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
        string Current2Page = string.Empty;
        FileStream currentFile;

        private DataItem<Level> _currentLevel;
        private DataItem<Boss> _currentBoss;
        private ListDataLevel _levels;
        private ListDataBoss _bosses;

        public PAC ThisPac { get; private set; }
        public static GameData GDStatic { get; set; }
        public GameData GD { get => GDStatic; set => GDStatic = value; }
        public ListResourse Items { get => ThisPac.Items; }
        public ListDataLevel Levels { get => _levels; private set => this.Set("Levels", ref _levels, value, true); }
        public ListDataBoss Bosses { get => _bosses; private set => this.Set("Bosses", ref _bosses, value, true); }
        public DataItem<Level> CurrentNewParentLevel { get; set; }
        public Level CurrentParentLevel { get; set; }

        public DataItem<Level> CurrentLevel
        {
            get => _currentLevel;

            set
            {


                if (value != null)
                {
                    if (_currentLevel != null)
                    {
                        GD.Levels.ReLoad(_currentLevel, GD.Bosses);
                    }
                    Current2Page = "LevelEditPage";

                    Messenger.Default.Send<NavigatorPageMessege>(new NavigatorPageMessege("LevelEditPage"));
                }
                else
                {
                    Current2Page = string.Empty;
                    Messenger.Default.Send<NavigatorPageMessege>(new NavigatorPageMessege("NotEditPage"));
                }
                _currentLevel = value;
                this.RaisePropertyChanged("CurrentLevel");

            }
        }
        public DataItem<Boss> CurrentBoss
        {
            get => _currentBoss;

            set
            {


                if (value != null)
                {
                    if (_currentLevel != null)
                    {
                        GD.Levels.ReLoad(_currentLevel, GD.Bosses);
                    }
                    Current2Page = "BossEditPage";

                    Messenger.Default.Send<NavigatorPageMessege>(new NavigatorPageMessege("BossEditPage"));
                }
                else
                {
                    Current2Page = string.Empty;
                    Messenger.Default.Send<NavigatorPageMessege>(new NavigatorPageMessege("NotEditPage"));
                }
                _currentBoss = value;
                this.RaisePropertyChanged("CurrentBoss");

            }
        }
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            Messenger.Default.Register<Item>(this, AddNewResourse);

            this.CreateNewPackage = new RelayCommand(CreateNewPackageAction, CreateNewPackageCanEx);
            this.CreateNewBoss = new RelayCommand(CreateNewBossAction, CreateNewBossCanEx);
            this.OpenResources = new RelayCommand(OpenResourcesAction, OpenResourcesCanEx);
            this.OpenBosses = new RelayCommand(OpenBossesAction, OpenBossesCanEx);
            this.OpenPackage = new RelayCommand(OpenPackageAction, OpenPackageCanEx);
            this.OpenLevels = new RelayCommand(OpenLevelsAction, OpenLevelsCanEx);
            this.CreateNewResourse = new RelayCommand(CreateNewResourseAction, CreateNewResourseCanEx);
            this.CreateNewLevel = new RelayCommand(CreateNewLevelAction, CreateNewLevelCanEx);
            this.SavePackage = new RelayCommand(SavePackageAction, SavePackageCanEx);
            this.SaveValue = new RelayCommand(SaveValueAction, SaveValueCanEx);
            this.CancelValue = new RelayCommand(CancelValueAction, CancelValueCanEx);
            this.AddParentToLevel = new RelayCommand(AddParentToLevelAction, AddParentToLevelCanEx);
            this.RermoveParentToLevel = new RelayCommand(RermoveParentToLevelAction, RermoveParentToLevelCanEx);

            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}
        }

        private bool RermoveParentToLevelCanEx() => CurrentParentLevel != null;
        private void RermoveParentToLevelAction()
        {
            CurrentLevel.Value.Parents.Remove(CurrentParentLevel);
            var temp = this.CurrentLevel;
            this.CurrentLevel = null;
            this.CurrentLevel = temp;
        }

        private bool AddParentToLevelCanEx() => CurrentNewParentLevel != null;
        private void AddParentToLevelAction()
        {
            if (CurrentLevel != CurrentNewParentLevel)
            {
                if (!CurrentNewParentLevel.Value.Parents.Contains(CurrentLevel.Value))
                    if (!CurrentLevel.Value.Parents.Contains(CurrentNewParentLevel.Value))
                    {
                        CurrentLevel.Value.Parents.Add(CurrentNewParentLevel.Value);
                    }

            }
            var temp = this.CurrentLevel;
            this.CurrentLevel = null;
            this.CurrentLevel = temp;

        }
        /// <summary>
        /// Добавить родителя уровня
        /// </summary>
        public ICommand RermoveParentToLevel { get; }
        /// <summary>
        /// Добавить родителя уровня
        /// </summary>
        public ICommand AddParentToLevel { get; }
        /// <summary>
        /// Сохранить пакет
        /// </summary>
        public ICommand CreateNewLevel { get; }

        public ICommand CancelValue { get; }

        /// <summary>
        /// Сохранить пакет
        /// </summary>
        public ICommand SavePackage { get; }
        public ICommand SaveValue { get; }

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
                this.GD = new GameData(this.ThisPac);
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
            this.CurrentBoss = new DataItem<Boss>();
            this.CurrentBoss.Value = new Boss();
        }


        private void SavePackageAction()
        {
            this.ThisPac = GD.SaveToPAC();
            GD = new GameData(this.ThisPac);
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
                this.GD = new GameData(this.ThisPac);
            }

            UpdateListLevels();
        }


        private void AddNewResourse(Item obj)
        {
            Items.Add(obj);
            this.RaisePropertyChanged("Items");
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

          this.Levels = null;
         //   this.RaisePropertyChanged("Levels");
            this.Levels = GD.Levels;
//this.RaisePropertyChanged("Levels");

        }
        private void UpdateListBosses()
        {

            //    this.Bosses = null;
            //    this.RaisePropertyChanged("Bosses");
            this.Bosses = null;
            this.Bosses = GD.Bosses;
        //    this.RaisePropertyChanged("Bosses");

        }

        private bool CreateNewLevelCanEx() => this.ThisPac != null && this.CurrentPage == "LevelPage";

        private void CreateNewLevelAction()
        {
            // Messenger.Default.Send<NavigatorPageMessege>(new NavigatorPageMessege("AddLevelWindowOpen"));
            this.CurrentLevel = new DataItem<Level>();
            this.CurrentLevel.Value = new Level();
        }
        private bool SaveValueCanEx()
        {
            if (Current2Page == "LevelEditPage")
            {
                return CurrentLevel.Value.Name?.Length > 0 && CurrentLevel.Value.ID > 0 && CurrentLevel.Value.Boss != null ;
            }
            if (Current2Page == "BossEditPage")
            {
                return CurrentBoss.Value.Name?.Length > 0 && CurrentBoss.Value.ID > 0 ;
            }
            return false;
        }

        private void SaveValueAction()
        {
            if (Current2Page == "LevelEditPage")
            {
                if (!this.GD.Levels.Contains(this.CurrentLevel))
                    this.GD.Levels.Add(this.CurrentLevel);
                this.GD.Levels.Save(this.CurrentLevel);
                UpdateListLevels();
            }
            else if (Current2Page == "BossEditPage")
            {
                if (!this.GD.Bosses.Contains(this.CurrentBoss))
                    this.GD.Bosses.Add(this.CurrentBoss);
                this.GD.Bosses.Save(this.CurrentBoss);
                UpdateListBosses();
            }

        }


        private bool CancelValueCanEx() => true;

        private void CancelValueAction()
        {
            if (Current2Page == "LevelEditPage")
            {
                if (this.GD.Levels.Contains(CurrentLevel))
                    this.GD.Levels.ReLoad(CurrentLevel, GD.Bosses);
                CurrentLevel = null;
            }
            else if (Current2Page == "BossEditPage")
            {
                if (!this.GD.Bosses.Contains(this.CurrentBoss))
                    this.GD.Bosses.ReLoad(this.CurrentBoss);
                CurrentLevel = null;
            }
        }






    }
}
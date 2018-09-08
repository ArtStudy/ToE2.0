using Assets.Core.Data.Question;
using Assets.Core.Game.Ages_and_Graphs;
using Assets.Core.Game.Data;
using Assets.Core.Game.Data.Cultures;
using Assets.Core.Game.Data.Question;
using Assets.Core.LevelsStructureInterfaces;
using Assets.Core.ToePac;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using GameСreator.API.Boss;

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Data;
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


        public CultureInfo[] Cultures => CultureInfo.GetCultures(CultureTypes.AllCultures);

        private DataItem<Level> _currentLevel;
        private DataItem<Boss> _currentBoss;
        private DataItem<IQuestion> _currentQuestion;
        private ListDataLevel _levels;
        private ListDataBoss _bosses;
        private ListDataQuestion _questions;
        private ListDataLanguagePack _languagePacks;
        private DataItem<LanguagePack> _currentLanguagePack;
        private IMulticulturalData _currentMulticulturalData;
        private DataItem<LanguagePack> _currentObjectLanguagePack;
        private FileStream _currentFile;
   //     private ListDataBase<IBase> _baseList;

        public PAC ThisPac { get; private set; }
        public static GameData GDStatic { get; set; }
        public GameData GD
        {
            get => GDStatic; set
            {
                GDStatic = value;
                this.RaisePropertyChanged("GD");
                this.RaisePropertyChanged("GDStatic");
            }
        }
     //   public ListDataBase<IBase> CurrentBaseList { get => _baseList; set  => Set("BaseList", ref _baseList,  value); }
        public FileStream CurrentFile { get => _currentFile; set => Set("CurrentFile", ref _currentFile, value); }
        public ListResourse Items { get => ThisPac.Items; }
        public ListDataLevel Levels { get => _levels; private set => this.Set("Levels", ref _levels, value, true); }
        public ListDataBoss Bosses { get => _bosses; private set => this.Set("Bosses", ref _bosses, value, true); }
        public ListDataQuestion Questions { get => _questions; private set => this.Set("Questions", ref _questions, value, true); }
        public ListDataLanguagePack LanguagePacks { get => _languagePacks; private set => this.Set("LanguagePacks", ref _languagePacks, value, true); }
        public DataItem<Level> CurrentNewParentLevel { get; set; }
        public Level CurrentParentLevel { get; set; }
        public IMulticulturalData CurrentMulticulturalData { get => _currentMulticulturalData; set => Set("CurrentMulticulturalData", ref _currentMulticulturalData, value); }


        public DataItem<LanguagePack> CurrentObjectLanguagePack
        {
            get => _currentObjectLanguagePack; set
            {

                if (_currentObjectLanguagePack != null)
                    this.GD.LanguagePacks.Save(_currentObjectLanguagePack);

                if (value != null)
                {
                    for (int i = 0; i < CurrentMulticulturalData.BasicLocalizationFields.Length; i++)
                    {
                        string strvalue = $"{CurrentMulticulturalData.TranslationIdentifier}.{ CurrentMulticulturalData.BasicLocalizationFields[i]}";
                        if (value.Value.LanguageData.Find((item) => item.Key == strvalue) == null)
                        {
                            value.Value.LanguageData[strvalue] = string.Empty;
                        }
                    }

                }


                Set("CurrentObjectLanguagePack", ref _currentObjectLanguagePack, value);


            }
        }

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
                    CurrentMulticulturalData = value.Value;
                }
                else
                {
                    CurrentMulticulturalData = null;
                    Current2Page = string.Empty;
                    Messenger.Default.Send<NavigatorPageMessege>(new NavigatorPageMessege("NotEditPage"));
                }
                this.Set("CurrentLevel", ref _currentLevel, value);
                CurrentObjectLanguagePack = null;



            }
        }
        public DataItem<Boss> CurrentBoss
        {
            get => _currentBoss;

            set
            {


                if (value != null)
                {
                    if (_currentBoss != null)
                    {
                        GD.Bosses.ReLoad(_currentBoss);
                    }
                    Current2Page = "BossEditPage";

                    Messenger.Default.Send<NavigatorPageMessege>(new NavigatorPageMessege("BossEditPage"));
                    CurrentMulticulturalData = value.Value;
                }
                else
                {
                    CurrentMulticulturalData = null;
                    Current2Page = string.Empty;
                    Messenger.Default.Send<NavigatorPageMessege>(new NavigatorPageMessege("NotEditPage"));
                }
                _currentBoss = value;

                this.RaisePropertyChanged("CurrentBoss");

            }
        }
        public DataItem<LanguagePack> CurrentLanguagePack
        {
            get => _currentLanguagePack;

            set
            {


                if (value != null)
                {
                    if (_currentLanguagePack != null)
                    {
                        GD.LanguagePacks.ReLoad(_currentLanguagePack);
                    }
                    Current2Page = "LanguagePackEditPage";

                    Messenger.Default.Send<NavigatorPageMessege>(new NavigatorPageMessege("LanguagePackEditPage"));
                }
                else
                {
                    Current2Page = string.Empty;
                    Messenger.Default.Send<NavigatorPageMessege>(new NavigatorPageMessege("NotEditPage"));
                }
                CurrentMulticulturalData = null;
                _currentLanguagePack = value;
                this.RaisePropertyChanged("CurrentLanguagePack");

            }
        }

        public DataItem<IQuestion> CurrentQuestion
        {
            get => _currentQuestion;

            set
            {


                if (value != null)
                {
                    if (_currentQuestion != null)
                    {
                        GD.Questions.ReLoad(_currentQuestion);
                    }
                    if (value.Value.TypeQuestion == TypeQuestionEnum.SelectOne)
                    {
                        Current2Page = "EditQuestionSelectOnePage";
                    }

                    Messenger.Default.Send<NavigatorPageMessege>(new NavigatorPageMessege(Current2Page));
                    CurrentMulticulturalData = value.Value;
                }
                else
                {
                    CurrentMulticulturalData = null;
                    Current2Page = string.Empty;
                    Messenger.Default.Send<NavigatorPageMessege>(new NavigatorPageMessege("NotEditPage"));
                }
                _currentQuestion = value;

                this.RaisePropertyChanged("CurrentQuestion");

            }
        }
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
  

            this.CreateNewPackage = new RelayCommand(CreateNewPackageAction, () => true);
            this.CreateNewBoss = new RelayCommand(CreateNewBossAction,                                () => this.ThisPac != null && this.CurrentPage == "BossesPage");
            this.CreateNewLevel = new RelayCommand(CreateNewLevelAction,                              () => this.ThisPac != null && this.CurrentPage == "LevelPage");
            this.CreateNewLanguagePack = new RelayCommand(CreateNewLanguagePacks,                     () => this.ThisPac != null && this.CurrentPage == "LanguagePacksPage");
            this.CreateNewQuestionSelectOne = new RelayCommand(CreateNewQuestionSelectOnePacks,       () => this.ThisPac != null && this.CurrentPage == "QuestionsPage");


            this.OpenResources = new RelayCommand(()=> OpenPageOne("ResourcesPage", null), ()=> this.ThisPac != null);
            this.OpenBosses = new RelayCommand(()=> OpenPageOne("BossesPage", UpdateAll), () => this.ThisPac != null);
            this.OpenPackage = new RelayCommand(OpenPackageAction, OpenPackageCanEx);
            this.OpenLevels = new RelayCommand(()=> OpenPageOne("LevelPage", UpdateAll), () => this.ThisPac != null);
            this.OpenLanguagePacks = new RelayCommand(() => OpenPageOne("LanguagePacksPage", UpdateAll), () => this.ThisPac != null);

            this.OpenQuestions = new RelayCommand(() => OpenPageOne("QuestionsPage", UpdateAll), () => this.ThisPac != null);


            this.CreateNewResourse = new RelayCommand(CreateNewResourseAction, CreateNewResourseCanEx);
          
            this.SavePackage = new RelayCommand(SavePackageAction, SavePackageCanEx);
            this.SaveValue = new RelayCommand(SaveValueAction, SaveValueCanEx);
            this.CancelValue = new RelayCommand(CancelValueAction, CancelValueCanEx);
            this.AddParentToLevel = new RelayCommand(AddParentToLevelAction, AddParentToLevelCanEx);
            this.RermoveParentToLevel = new RelayCommand(RermoveParentToLevelAction, RermoveParentToLevelCanEx);
            this.RermoveItem = new RelayCommand(RermoveItemAction, RermoveItemCanEx);
            this.SortingByID = new RelayCommand(SortingByIDAction, SortingByIDCanEx);
            //   this.AddLocalizationString = new RelayCommand(AddLocalizationStringAction, AddLocalizationStringCanEx);
            //     this.RemoveLocalizationString = new RelayCommand(RemoveLocalizationStringAction, RemoveLocalizationStringCanEx);

            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}
        }

        private void CreateNewQuestionSelectOnePacks()
        {
            this.CurrentQuestion = new DataItem<IQuestion>() { Value = new QuestionSelectOne() { ID = this.Questions.GenNewID() } };

        
        }

        private bool SortingByIDCanEx() => !string.IsNullOrWhiteSpace(CurrentPage);

        private void SortingByIDAction()
        {
            if (CurrentPage == "LanguagePacksPage")
            {
                this.GD.LanguagePacks.SortingByID();

            }
            else if (CurrentPage == "LevelPage")
            {

                this.GD.Levels.SortingByID();

            }
            else if (CurrentPage == "BossesPage")
            {

                this.GD.Bosses.SortingByID();

            }
            UpdateAll();
        }

        private bool RermoveItemCanEx => !string.IsNullOrWhiteSpace(CurrentPage);
        private void RermoveItemAction()
        {
            if (CurrentPage == "LanguagePacksPage")
            {
                this.GD.LanguagePacks.Remove(this.CurrentLanguagePack);

            }
            else if (CurrentPage == "LevelPage")
            {
                this.GD.Levels.Remove(this.CurrentLevel);

            }
            else if (CurrentPage == "BossesPage")
            {
                this.GD.Bosses.Remove(this.CurrentBoss);

            }
            UpdateAll();
        }

 
        private void CreateNewLanguagePacks()
        {
            this.CurrentLanguagePack = new DataItem<LanguagePack>();
            this.CurrentLanguagePack.Value = new LanguagePack();
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
        public ICommand SortingByID { get; }
        /// <summary>
        /// Добавить родителя уровня
        /// </summary>
        public ICommand RermoveParentToLevel { get; }
        /// <summary>
        /// Добавить родителя уровня
        /// </summary>
        public ICommand AddParentToLevel { get; }
        public ICommand CreateNewLanguagePack { get; }
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
        /// Открыть пакет
        /// </summary>
        public ICommand OpenQuestions { get; }

        /// <summary>
        /// Открыть поле русурсов 
        /// </summary>
        public ICommand OpenResources { get; }

        /// <summary>
        /// Открыть список босов
        /// </summary>
        public ICommand OpenBosses { get; }
        public ICommand RermoveItem { get; }
        /// <summary>
        /// Открыть список языковой пакет
        /// </summary>
        public ICommand OpenLanguagePacks { get; }

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
        /// <summary>
        /// Создать новый ресурс
        /// </summary>
        public ICommand CreateNewQuestionSelectOne { get; }
        /// <summary>
        /// Добавить новую стоку локализации
        /// </summary>
        public ICommand AddLocalizationString { get; }
        /// <summary>
        /// Удалить строку локализации
        /// </summary>
        public ICommand RemoveLocalizationString { get; }

        private void CreateNewPackageAction()
        {
            if (this.ThisPac == null)
            {
                this.ThisPac = new PAC();
                this.GD = new GameData(this.ThisPac);
            }


            //  this.OpenResources.CanExecute(null);
        }




        private bool CreateNewResourseCanEx() => this.ThisPac != null && this.CurrentPage == "ResourcesPage";
        private void CreateNewResourseAction()
        {
            Messenger.Default.Send<NavigatorPageMessege>(new NavigatorPageMessege("AddResourseWindowOpen"));
        }


        private void OpenPageOne(string NamePage, Action ac)
        {
            CurrentPage = NamePage;
            Messenger.Default.Send<NavigatorPageMessege>(new NavigatorPageMessege(NamePage));
            ac?.Invoke();
        }


        private void CreateNewBossAction()
        {
            this.CurrentBoss = new DataItem<Boss>();
            this.CurrentBoss.Value = new Boss();
            this.CurrentBoss.Value.ID = this.Bosses.GenNewID();
        }


        private void SavePackageAction()
        {
            this.ThisPac = GD.SaveToPAC();
            GD = new GameData(this.ThisPac);
            if (CurrentFile == null)
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
                    CurrentFile = new FileStream(filename, FileMode.Create);
                    ThisPac.Serialization().WriteTo(CurrentFile);
                    CurrentFile.Flush(true);

                }
            }
            else
                CurrentFile.Position = 0;
            ThisPac.Serialization().WriteTo(CurrentFile);
            CurrentFile.Flush(true);

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
                CurrentFile?.Dispose();
                CurrentFile = new FileStream(filename, FileMode.Open);
                MemoryStream ms = new MemoryStream();
                ///  currentFile.CopyTo(ms);
                this.ThisPac = new PAC(CurrentFile);
                this.GD = new GameData(this.ThisPac);

            }
            UpdateAll();
        }





        
        private void UpdateAll()
        {

            this.Levels = null;
            this.Levels = GD.Levels;

            this.Bosses = null;
            this.Bosses = GD.Bosses;


            this.LanguagePacks = null;
            this.LanguagePacks = GD.LanguagePacks;

            this.Questions = null;
            this.Questions = GD.Questions;

        }


        private void CreateNewLevelAction()
        {
        
            this.CurrentLevel = new DataItem<Level>();
            this.CurrentLevel.Value = new Level();
            this.CurrentLevel.Value.ID = this.Levels.GenNewID();
        }
        private bool SaveValueCanEx()
        {
            if (Current2Page == "LevelEditPage")
            {
                return CurrentLevel.Value.Name?.Length > 0 && CurrentLevel.Value.ID > 0 && CurrentLevel.Value.Boss != null;
            }
            else if (Current2Page == "BossEditPage")
            {
                return CurrentBoss.Value.Name?.Length > 0 && CurrentBoss.Value.ID > 0;
            }
            else if (Current2Page == "LanguagePackEditPage")
            {
                return CurrentLanguagePack.Value.Culture != null;
            }
            else if (Current2Page == "EditQuestionSelectOnePage")
            {
                var currentQuestion = (QuestionSelectOne)CurrentQuestion.Value;
                return currentQuestion.Name?.Length > 0 && currentQuestion.ID > 0 && currentQuestion.NumberAnswer > 1 && currentQuestion.RightAnswer > 0;
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
                this.CurrentLevel = null;
            }
            else if (Current2Page == "BossEditPage")
            {
                if (!this.GD.Bosses.Contains(this.CurrentBoss))
                    this.GD.Bosses.Add(this.CurrentBoss);
                this.GD.Bosses.Save(this.CurrentBoss);
                this.CurrentBoss = null;
            }
            else if (Current2Page == "LanguagePackEditPage")
            {
                if (!this.GD.LanguagePacks.Contains(this.CurrentLanguagePack))
                    this.GD.LanguagePacks.Add(this.CurrentLanguagePack);
                this.GD.LanguagePacks.Save(this.CurrentLanguagePack);
                this.CurrentLanguagePack = null;
            }
            else if (Current2Page == "EditQuestionSelectOnePage")
            {
                if (!this.GD.Questions.Contains(this.CurrentQuestion))
                    this.GD.Questions.Add(this.CurrentQuestion);
                this.GD.Questions.Save(this.CurrentQuestion);
                this.CurrentQuestion = null;
            }
            UpdateAll();
        }


        private bool CancelValueCanEx() => true;

        private void CancelValueAction()
        {
            if (Current2Page == "LevelEditPage")
            {
                if (this.GD.Levels.Contains(CurrentLevel))
                    this.GD.Levels.ReLoad(CurrentLevel, GD.Bosses);
                this.CurrentLevel = null;
            }
            else if (Current2Page == "BossEditPage")
            {
                if (!this.GD.Bosses.Contains(this.CurrentBoss))
                    this.GD.Bosses.ReLoad(this.CurrentBoss);
                this.CurrentBoss = null;
            }
            else if (Current2Page == "LanguagePackEditPage")
            {
                if (!this.GD.LanguagePacks.Contains(this.CurrentLanguagePack))
                    this.GD.LanguagePacks.ReLoad(this.CurrentLanguagePack);
                this.CurrentLanguagePack = null;
            }
            else if (Current2Page == "EditQuestionSelectOnePage")
            {
                if (!this.GD.Questions.Contains(this.CurrentQuestion))
                    this.GD.Questions.ReLoad(this.CurrentQuestion);
                this.CurrentQuestion = null;
            }
        }






    }
}
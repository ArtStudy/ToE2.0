
using Assets.Core.Game.Ages_and_Graphs;
using Assets.Core.Game.Data;
using Assets.Core.Game.Data.Age;
using Assets.Core.Game.Data.Boss;
using Assets.Core.Game.Data.Cultures;
using Assets.Core.Game.Data.Level;
using Assets.Core.Game.Data.Question;
using Assets.Core.Levels;
using Assets.Core.LevelsStructureInterfaces;
using Assets.Core.ToePac;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;


using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
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

        private string Current2Page { get => _current2Page; set
            {
                _current2Page = value;
                Messenger.Default.Send<NavigatorPageMessege>(new NavigatorPageMessege(_current2Page));
            }
        }


        public CultureInfo[] Cultures => CultureInfo.GetCultures(CultureTypes.AllCultures);

        private IBase _currentItem;
   
        private IMulticulturalData _currentMulticulturalData;
        private LanguagePack _currentObjectLanguagePack;
   
        private string _selectedPath;
        private List<FilePackage> _files = new List<FilePackage>();
        private FilePackage _selectedFile;
        private List<IBase> _currentItems;
        private string _current2Page;
        private FilePackage NewCurrentFileObject;

        public List<FilePackage> Files
        {
            get => _files; set
            {
                Set("Files", ref _files, value);
            }
        }

        //     private ListDataBase<IBase> _baseList;



        //   public ListDataBase<IBase> CurrentBaseList { get => _baseList; set  => Set("BaseList", ref _baseList,  value); }

        public List<IBase> CurrentItems { get => _currentItems; set => Set("CurrentItems", ref _currentItems, value); }

        public List<IBoss> BossesList { get => GetListOfType(FileTypes.Boss).OfType<IBoss>().ToList(); }
        public List<IQuestion> QuestionsList { get => GetListOfType(FileTypes.Question).OfType<IQuestion>().ToList(); }
        public List<ILevel> LevelsList { get=> GetListOfType( FileTypes.Level).OfType<ILevel>().ToList();}
        public List<ILanguagePack> LanguagesList { get => GetListOfType(FileTypes.Language).OfType<ILanguagePack>().ToList(); }
        public List<IAge> AgeList { get => GetListOfType(FileTypes.Age).OfType<IAge>().ToList(); }

        public Level CurrentNewParentLevel { get; set; }
        public IQuestion CurrentNewQuestionLevel { get; set; }
        public Level CurrentNewLevelAge { get; set; }

        public Level CurrentParentLevel { get; set; }
        public IQuestion CurrentQuestionLevel { get; set; }
        public Level CurrentLevelAge { get; set; }

       

        public IMulticulturalData CurrentMulticulturalData { get => _currentMulticulturalData; set => Set("CurrentMulticulturalData", ref _currentMulticulturalData, value); }

        public string SelectedPath { get => _selectedPath; set => Set("SelectedPath", ref _selectedPath, value); }

        public FilePackage SelectedFile
        {
            get => _selectedFile; set
            {

                if (value != null)
                {

                    Current2Page = "ResourcesPage";

                    Messenger.Default.Send<NavigatorPageMessege>(new NavigatorPageMessege("ResourcesPage"));
                    //CurrentMulticulturalData = value.Value;
                }
                else
                {
                    CurrentMulticulturalData = null;
                    Current2Page = string.Empty;
                    Messenger.Default.Send<NavigatorPageMessege>(new NavigatorPageMessege("NotEditPage"));
                }
                this.Set("SelectedFile", ref _selectedFile, value);



            }
        }

        public FilePackage CurrentFileObject
        {
            get
            {
                if(this.CurrentItem == null)
                    return null;
                for (int i = 0; i < Files?.Count; i++)
                {
                    foreach (var item in Files[i].ResourceFileData.Data)
                    {
                        if (item.Value.ContainsKey(this.CurrentItem))
                            return Files[i];
                    }
                }
                return null;
            }
            set => NewCurrentFileObject = value;
        }
                
               


        public LanguagePack CurrentObjectLanguagePack
        {
            get => _currentObjectLanguagePack; set
            {

              


                if (value != null)
                {
                    for (int i = 0; i < CurrentMulticulturalData.BasicLocalizationFields.Length; i++)
                    {
                        string strvalue = $"{CurrentMulticulturalData.TranslationIdentifier}.{ CurrentMulticulturalData.BasicLocalizationFields[i]}";
                        if (value.LanguageData.Find((item) => item.Key == strvalue) == null)
                        {
                            value.LanguageData[strvalue] = string.Empty;
                        }
                    }

                }


                Set("CurrentObjectLanguagePack", ref _currentObjectLanguagePack, value);


            }
        }

        public IBase CurrentItem
        {
            get => _currentItem;

            set
            {


                if (value != null)
                {
                    if (_currentItem != null)
                    {
                        //GD.Levels.ReLoad(_currentItem, GD.Bosses, GD.Questions);
                    }
           
                    var typevalue = value.GetType();
                    if (typeof(ILevel).IsAssignableFrom(typevalue))
                    {
                        Current2Page = "LevelEditPage";
                        CurrentMulticulturalData = (ILevel)value;
                    }
                    else if (typeof(IAge).IsAssignableFrom(typevalue))
                    {
                        Current2Page = "AgeEditPage";
                        CurrentMulticulturalData = (IAge)value;
                    }
                    else if (typeof(IBoss).IsAssignableFrom(typevalue))
                    {
                        Current2Page = "BossEditPage";
                        CurrentMulticulturalData = (IBoss)value;
                    }
                    else if (typeof(IQuestion).IsAssignableFrom(typevalue))
                    {
                        if (((IQuestion)value).TypeQuestion == TypeQuestionEnum.SelectOne)
                        {

                            Current2Page = "EditQuestionSelectOnePage";
                        }
                        CurrentMulticulturalData = (IQuestion)value;
                    }
                   else if (typeof(ILanguagePack).IsAssignableFrom(typevalue))
                    {

                        Current2Page = "LanguagePackEditPage";
                        //CurrentMulticulturalData = (ILanguagePack)value;
                    }

                }
                else
                {
                    CurrentMulticulturalData = null;
                    Current2Page = string.Empty;
                    Messenger.Default.Send<NavigatorPageMessege>(new NavigatorPageMessege("NotEditPage"));
                }
                this.Set("CurrentItem", ref _currentItem, value);
               
                CurrentObjectLanguagePack = null;


                this.RaisePropertyChanged("CurrentFileObject");
                this.RaisePropertyChanged("LanguagesList");


            }
        }
    

       
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            this.PropertyChanged += MainViewModel_PropertyChanged;

            this.CreateNewPackage = new RelayCommand(CreateNewPackageAction, () => true);
            this.CreateNewBoss = new RelayCommand(CreateNewBossAction, () => this.Files?.Count > 0 && this.CurrentPage == "BossesPage");
            this.CreateNewAge = new RelayCommand(CreateNewAgeAction, () => this.Files?.Count > 0 && this.CurrentPage == "AgesPage");
            this.CreateNewLevel = new RelayCommand(CreateNewLevelAction, () => this.Files?.Count > 0 && this.CurrentPage == "LevelPage");
            this.CreateNewLanguagePack = new RelayCommand(CreateNewLanguagePacks, () => this.Files?.Count > 0 && this.CurrentPage == "LanguagePacksPage");
            this.CreateNewQuestionSelectOne = new RelayCommand(CreateNewQuestionSelectOnePacks, () => this.Files?.Count > 0 && this.CurrentPage == "QuestionsPage");

            this.OpenFilesPage = new RelayCommand(() => OpenPageOne("FilesPage", null), OpenPageCanEx);
            this.OpenAges = new RelayCommand(() => OpenPageOne("AgesPage", () => UpdateAll(FileTypes.Age)), OpenPageCanEx);
            this.OpenBosses = new RelayCommand(() => OpenPageOne("BossesPage", () => UpdateAll(FileTypes.Boss)), OpenPageCanEx);
            this.OpenPackage = new RelayCommand(OpenPackageAction, OpenPackageCanEx);
            this.OpenLevels = new RelayCommand(() => OpenPageOne("LevelPage", () => UpdateAll( FileTypes.Level)), OpenPageCanEx);
            this.OpenLanguagePacks = new RelayCommand(() => OpenPageOne("LanguagePacksPage", () => UpdateAll(FileTypes.Language)), OpenPageCanEx);
            this.OpenDir = new RelayCommand(OpenDirAction, OpenDirCanEx);
            this.OpenQuestions = new RelayCommand(() => OpenPageOne("QuestionsPage", () => UpdateAll(FileTypes.Question)), OpenPageCanEx);


       

            this.SavePackage = new RelayCommand(SavePackageAction, SavePackageCanEx);
            this.SaveValue = new RelayCommand(SaveValueAction, SaveValueCanEx);
            this.CancelValue = new RelayCommand(CancelValueAction, CancelValueCanEx);
          
            this.RermoveItem = new RelayCommand(RermoveItemAction, RermoveItemCanEx);
 

            this.AddQuestionToLevel = new RelayCommand(AddQuestionToLevelAction, AddQuestionToLevelCanEx);
            this.RermoveQuestionToLevel = new RelayCommand(RermoveQuestionAction, RermoveQuestionCanEx);
            this.AddParentToLevel = new RelayCommand(AddParentToLevelAction, AddParentToLevelCanEx);
            this.RermoveParentToLevel = new RelayCommand(RemoveParentToLevelAction, RemoveParentToLevelCanEx);
            this.AddLevelToAge = new RelayCommand(AddLevelToAgeAction, AddLevelToAgeCanEx);
            this.RermoveLevelToAge = new RelayCommand(RermoveLevelToAgeAction, RermoveLevelToAgeCanEx);

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

        private bool RermoveLevelToAgeCanEx() => this.CurrentLevelAge != null;


        private void RermoveLevelToAgeAction()
        {
            ((IAge)this.CurrentItem).Levels.Remove(CurrentLevelAge);


            var temp = this.CurrentItem;
            this.CurrentItem = null;
            this.CurrentItem = temp;
        }

        private bool AddLevelToAgeCanEx() => this.CurrentNewLevelAge != null;

        private void AddLevelToAgeAction()
        {
            if (!((IAge)this.CurrentItem).Levels.Contains(this.CurrentNewLevelAge))
            {
                ((IAge)this.CurrentItem).Levels.Add(CurrentNewLevelAge);
            }

            var temp = this.CurrentItem;
            this.CurrentItem = null;
            this.CurrentItem = temp;
        }

        private void MainViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            
        }

        private bool OpenPageCanEx()
        {
            this.RaisePropertyChanged("BossesList");
            this.RaisePropertyChanged("QuestionsList");
            this.RaisePropertyChanged("LevelsList");
            this.RaisePropertyChanged("LanguagesList");
            this.RaisePropertyChanged("AgeList");
            return  this.Files?.Count > 0;
        }

        private bool OpenDirCanEx() => true;
        private void OpenDirAction()
        {
            if (Files.Count > 0 && MessageBox.Show("Закрыть текущие файлы?", "Файлы", MessageBoxButton.YesNo)== MessageBoxResult.Yes)
            {
                if (MessageBox.Show("Сохранить текущие файлы?", "Файлы", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    for(int i = 0; i < Files.Count; i++)
                    {
                        Files[i].Save();
                    }
                }
     
                    Files.Clear();
         
            }

            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    SelectedPath = dialog.SelectedPath;
                }
                else
                {
                    SelectedPath = string.Empty;
                }
            }
            if (!String.IsNullOrWhiteSpace(this.SelectedPath))
            {
                var strfiles = Directory.GetFiles(this.SelectedPath, "*.ToePackage");

                var filestemp = new List<FilePackage>(Files);
                for (int i = 0; i < strfiles.Length; i++)
                {
                    FilePackage newitem = new FilePackage(strfiles[i]);
                    filestemp[i] = newitem;
                }
                this.Files = filestemp;

            }

        }


        private bool AddParentToLevelCanEx() => CurrentNewParentLevel != null;


        private void AddParentToLevelAction()
        {
            if (!((ILevel)this.CurrentItem).Parents.Contains(this.CurrentNewParentLevel)&& !this.CurrentNewParentLevel.Parents.Contains((ILevel)this.CurrentItem) && this.CurrentItem != this.CurrentNewParentLevel)
            {
                ((ILevel)this.CurrentItem).Parents.Add(CurrentNewParentLevel);
            }

            var temp = this.CurrentItem;
            this.CurrentItem = null;
            this.CurrentItem = temp;
        }

        private bool RemoveParentToLevelCanEx() => CurrentParentLevel != null;

        private void RemoveParentToLevelAction()
        {
            ((ILevel)this.CurrentItem).Parents.Remove(CurrentParentLevel);


            var temp = this.CurrentItem;
            this.CurrentItem = null;
            this.CurrentItem = temp;
        }

        private bool AddQuestionToLevelCanEx() => CurrentNewQuestionLevel != null;
        private void AddQuestionToLevelAction()
        {

            if (!((ILevel)this.CurrentItem).QuestionsLevel.Contains(this.CurrentNewQuestionLevel))
            {
                ((ILevel)this.CurrentItem).QuestionsLevel.Add(CurrentNewQuestionLevel);
            }

            var temp = this.CurrentItem;
            this.CurrentItem = null;
            this.CurrentItem = temp;


        }
        private bool RermoveQuestionCanEx() => this.CurrentQuestionLevel != null;

        private void RermoveQuestionAction()
        {

            ((ILevel)this.CurrentItem).QuestionsLevel.Remove(CurrentQuestionLevel);


            var temp = this.CurrentItem;
            this.CurrentItem = null;
            this.CurrentItem = temp;
        }




        private bool RermoveItemCanEx => !string.IsNullOrWhiteSpace(CurrentPage);
        private void RermoveItemAction()
        {
            if(this.CurrentPage == "FilesPage")
            {
                if(MessageBox.Show("Сохранить изменения в файле?", "Файлы", MessageBoxButton.YesNo)== MessageBoxResult.Yes)
                {
                    this.SelectedFile.Save();
                    this.Files.Remove(this.SelectedFile);
                }

                return;
            }

            var typevalue = this.CurrentItem.GetType();

            foreach (var item in this.Files)
            {

                if (item.IsActive)
                {
                    if (typeof(ILevel).IsAssignableFrom(typevalue))
                    {
                        item.ResourceFileData.Data[FileTypes.Level].Remove(this.CurrentItem);
                    }
                    else if (typeof(IBoss).IsAssignableFrom(typevalue))
                    {
                        item.ResourceFileData.Data[FileTypes.Boss].Remove(this.CurrentItem);
                    }
                    else if (typeof(IQuestion).IsAssignableFrom(typevalue))
                    {

                        item.ResourceFileData.Data[FileTypes.Question].Remove(this.CurrentItem);
                     
                    }
                    else if (typeof(IAge).IsAssignableFrom(typevalue))
                    {

                        item.ResourceFileData.Data[FileTypes.Age].Remove(this.CurrentItem);

                    }
                    else if (typeof(ILanguagePack).IsAssignableFrom(typevalue))
                    {
                        item.ResourceFileData.Data[FileTypes.Language].Remove(this.CurrentItem);
                    }
                }
            }


        }


        private void CreateNewLanguagePacks()
        {
            this.CurrentItem = new LanguagePack();
     
        }
        private void CreateNewQuestionSelectOnePacks()
        {
            this.CurrentItem = new QuestionSelectOne();



        }
        private void CreateNewAgeAction()
        {
            this.CurrentItem = new Age();
        }

        private void CreateNewBossAction()
        {
            this.CurrentItem = new Boss();
          
        }
        private void CreateNewLevelAction()
        {
            this.CurrentItem = new Level();

        }




      
        /// <summary>
        /// Добавить родителя уровня
        /// </summary>
        public ICommand OpenFilesPage { get; }

        /// <summary>
        /// Добавить родителя уровня
        /// </summary>
        public ICommand RermoveParentToLevel { get; }
        /// <summary>
        /// Добавить родителя уровня
        /// </summary>
        public ICommand AddParentToLevel { get; }

        /// <summary>
        /// Добавить родителя уровня
        /// </summary>
        public ICommand RermoveQuestionToLevel { get; }
        /// <summary>
        /// Добавить родителя уровня
        /// </summary>
        public ICommand AddQuestionToLevel { get; }


        public ICommand RermoveLevelToAge { get; }

        public ICommand AddLevelToAge { get; }
        public ICommand CreateNewLanguagePack { get; }
        /// <summary>
        /// Сохранить пакет
        /// </summary>
        public ICommand CreateNewLevel { get; }

        public ICommand CreateNewAge { get; }

        public ICommand CancelValue { get; }

        /// <summary>
        /// Сохранить пакет
        /// </summary>
        public ICommand SavePackage { get; }
        public ICommand SaveValue { get; }
        public ICommand OpenDir { get; }
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
        public ICommand OpenAges { get; }

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
                this.Files.Add(new FilePackage(filename, true));
            

            }
        }
        



     


        private void OpenPageOne(string NamePage, Action ac)
        {
            CurrentPage = NamePage;
            Messenger.Default.Send<NavigatorPageMessege>(new NavigatorPageMessege(NamePage));
            ac?.Invoke();
        }


   
        private void SavePackageAction()
        {
            this.Files.ForEach((file) => file.Save());
        }

        private bool SavePackageCanEx() => this.Files?.Count > 0;


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
                this.Files.Add(new FilePackage(dlg.FileName) );

            }
          
        }






        private void UpdateAll(FileTypes type)
        {



            this.CurrentItems = GetListOfType(type);

        }
        private List<IBase> GetListOfType(FileTypes type)
        {
          


            var items = new List<IBase>();
            if (this.Files.Count == 0)
                return items;
            foreach (var item in this.Files)
            {

                if (item.IsActive)
                {
                    if (item.ResourceFileData.Data.ContainsKey(type))
                        items.AddRange(item.ResourceFileData.Data[type].Keys);

                }
            }
            return items;

        }


        private bool SaveValueCanEx()
        {
            if (this.CurrentItem == null)
                return false;
            var typevalue = this.CurrentItem.GetType();
            if (typeof(ILevel).IsAssignableFrom(typevalue))
            {
                return CurrentItem.Name?.Length > 0 && (this.CurrentFileObject != null || (CurrentItem.ID == 0 && this.NewCurrentFileObject != null)) && ((ILevel)CurrentItem).Boss != null;
            }
            else if (typeof(IBoss).IsAssignableFrom(typevalue))
            {
                return CurrentItem.Name?.Length > 0 && (this.CurrentFileObject != null || (CurrentItem.ID == 0 && this.NewCurrentFileObject != null));
            }
            else if (typeof(IQuestion).IsAssignableFrom(typevalue))
            {
                var currentQuestion = (QuestionSelectOne)CurrentItem;
                return CurrentItem.Name?.Length > 0 && (this.CurrentFileObject != null || (CurrentItem.ID == 0 && this.NewCurrentFileObject != null)) && currentQuestion.NumberAnswer > 1 && currentQuestion.RightAnswer > 0;
                

            }
          else  if (typeof(ILanguagePack).IsAssignableFrom(typevalue))
            {
                return ((ILanguagePack)CurrentItem).Culture != null && (this.CurrentFileObject != null || (CurrentItem.ID == 0 && this.NewCurrentFileObject != null));
            }
            else if (typeof(IAge).IsAssignableFrom(typevalue))
            {
                return CurrentItem.Name?.Length > 0 && (this.CurrentFileObject != null || (CurrentItem.ID == 0 && this.NewCurrentFileObject != null));
            }
            return false;
        }

        private void SaveValueAction()
        {
            if (this.CurrentItem.ID == 0)
                this.CurrentItem.ID = (this.CurrentItem.Name + DateTime.Now).GetUInt64HashCode();
            FilePackage SavePac = null;
            if (this.CurrentFileObject == null)
            {
                SavePac = NewCurrentFileObject;
            }
            else
            {
                if (NewCurrentFileObject == this.CurrentFileObject || NewCurrentFileObject == null)
                {
                    SavePac = this.CurrentFileObject;
                }
                else
                {
                    SavePac = NewCurrentFileObject;
                    RermoveItemAction();
                }
            }

            SavePac.ResourceFileData.Save(this.CurrentItem);

            for(int i =0; i< this.LanguagesList.Count; i++)
            {
                for (int j = 0; j < Files?.Count; j++)
                {
                    foreach (var item in Files[j].ResourceFileData.Data)
                    {
                        if (item.Value.ContainsKey(this.LanguagesList[i]))
                            Files[j].ResourceFileData.Save(this.LanguagesList[i]);

              
                    }
                }
         
           
            }

            CurrentItem = null;

            switch (this.CurrentPage)
            {
                case "QuestionsPage":
                    this.UpdateAll(FileTypes.Question);
                    break;

                case "BossesPage":
                    this.UpdateAll(FileTypes.Boss);
                    break;
                case "LevelPage":
                    this.UpdateAll(FileTypes.Level);
                    break;
                case "LanguagePacksPage":
                    this.UpdateAll(FileTypes.Language);
                    break;
                case "AgesPage":
                    this.UpdateAll(FileTypes.Age);
                    break;

            }

        }


        private bool CancelValueCanEx() => true;

        private void CancelValueAction()
        {
            if (this.CurrentItem.ID >0)
            {
                this.Files.ForEach((file) => file.ResourceFileData.ReLoad(this.CurrentItem));
                this.CurrentItem = null;

                for (int i = 0; i < this.LanguagesList.Count; i++)
                {
                    for (int j = 0; j < Files?.Count; j++)
                    {
                        foreach (var item in Files[j].ResourceFileData.Data)
                        {
                            if (item.Value.ContainsKey(this.LanguagesList[i]))
                                Files[j].ResourceFileData.ReLoad(this.LanguagesList[i]);


                        }
                    }


                }
            }
            this.CurrentItem = null;

            switch(this.CurrentPage)
            {
                case "QuestionsPage":
                    this.UpdateAll(FileTypes.Question);
                    break;

                case "BossesPage":
                    this.UpdateAll(FileTypes.Boss);
                    break;
                case "LevelPage":
                    this.UpdateAll(FileTypes.Level);
                    break;
                case "LanguagePacksPage":
                    this.UpdateAll(FileTypes.Language);
                    break;
                case "AgesPage":
                    this.UpdateAll(FileTypes.Age);
                    break;

            }
          
        }






    }
}
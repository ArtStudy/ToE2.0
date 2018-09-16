using Assets.Core.Game.Data.Cultures;
using Assets.Core.ToePac;
using GalaSoft.MvvmLight.Messaging;

using GameСreator.Pages;
using GameСreator.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GameСreator
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
     

        public MainWindow()
        {
            InitializeComponent();

            Messenger.Default.Register<NavigatorPageMessege>(this, NavigatorPageAction);



        }
        private void NavigatorPageAction(NavigatorPageMessege obj)
        {
            ((Page)SPage.Content)?.ClearValue(Page.DataContextProperty);
            ClearHistory();
            switch (obj.NamePage)
            {
                case "ResourcesPage":

                    SPage.Navigate(new ResourcesPage());
                    break;

                case "BossEditPage":
                    SPage.Navigate(new EditBossPage());
                    break;
                case "LanguagePackEditPage":
                    SPage.Navigate(new EditLanguagePackPage());
                    break;
                case "EditQuestionSelectOnePage":
                    SPage.Navigate(new EditQuestionSelectOnePage());

                    break;
                case "BossesPage":
                    MainPage.Source = new Uri("Pages\\List\\BossPage.xaml", UriKind.Relative);
                    break;
                case "LevelPage":
                    MainPage.Source = new Uri("Pages\\List\\LevelPage.xaml", UriKind.Relative);
                    break;
                case "LanguagePacksPage":
                    MainPage.Source = new Uri("Pages\\List\\LanguagePacksPage.xaml", UriKind.Relative);
                    break;
                case "QuestionsPage":
                    MainPage.Source = new Uri("Pages\\List\\QuestionPage.xaml", UriKind.Relative);
                    break;
                case "FilesPage":
                    MainPage.Source = new Uri("Pages\\List\\FilesPage.xaml", UriKind.Relative);
                    break;
                case "LevelEditPage":
                    SPage.Navigate(new EditLevelPage());
                    break;

                   
     
                case "NotEditPage":
                    SPage.Navigate(null);
                    break;
            }

        }
        public void ClearHistory()
        {
            if (!SPage.CanGoBack && !SPage.CanGoForward)
            {
                return;
            }

            var entry = SPage.RemoveBackEntry();
            while (entry != null)
            {
                entry = SPage.RemoveBackEntry();
            }

          
        }
        private void CollectionViewSource_Filter(object sender, FilterEventArgs e)
        {
            LocalizationKeyValuePair item = (LocalizationKeyValuePair)e.Item;
            MainViewModel VM = this.DataContext as MainViewModel;
            if (item.Key != null)
                e.Accepted = item.Key.StartsWith(VM.CurrentMulticulturalData.TranslationIdentifier);
            else
                e.Accepted = false;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.Datag.ItemsSource != null)
                CollectionViewSource.GetDefaultView(this.Datag.ItemsSource).Refresh();
        }


    }
}

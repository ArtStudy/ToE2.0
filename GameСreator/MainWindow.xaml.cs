﻿using Assets.Core.Game.Data.Cultures;
using Assets.Core.ToePac;
using GalaSoft.MvvmLight.Messaging;
using GameСreator.DWIndows;
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
        AddResourse AddResourseWindow;
  

        public MainWindow()
        {
            InitializeComponent();

            Messenger.Default.Register<NavigatorPageMessege>(this, NavigatorPageAction);



        }
        private void NavigatorPageAction(NavigatorPageMessege obj)
        {
            switch (obj.NamePage)
            {
                case "ResourcesPage":
                    MainPage.Source = new Uri( "Pages\\ResourcesPage.xaml",  UriKind.Relative);
                    break;
                case "AddResourseWindowOpen":
                    AddResourseWindow?.Close();
                    AddResourseWindow = new AddResourse();
                    AddResourseWindow.ShowDialog();
                    break;
                case "AddResourseWindowClose":
                    AddResourseWindow?.Close();
                    AddResourseWindow = null;
                    break;
                case "BossEditPage":
                    SPage.Source = new Uri("Pages\\EditBossPage.xaml", UriKind.Relative);
                    break;
                case "BossesPage":
                    MainPage.Source = new Uri("Pages\\BossPage.xaml", UriKind.Relative);
                    break;
                case "LevelPage":
                    MainPage.Source = new Uri("Pages\\LevelPage.xaml", UriKind.Relative);
                    break;
                case "LanguagePacksPage":
                    MainPage.Source = new Uri("Pages\\LanguagePacksPage.xaml", UriKind.Relative);
                    break;
                case "QuestionsPage":
                    MainPage.Source = new Uri("Pages\\QuestionPage.xaml", UriKind.Relative);
                    break;
                case "LevelEditPage":
                    SPage.Source = new Uri("Pages\\EditLevelPage.xaml", UriKind.Relative);
                    break;

                    case "LanguagePackEditPage":
                    SPage.Source = new Uri("Pages\\EditLanguagePackPage.xaml", UriKind.Relative);
                    break;
        case "EditQuestionSelectOnePage":
                    SPage.Source = new Uri("Pages\\EditQuestionSelectOnePage.xaml", UriKind.Relative);
                    break;
                case "NotEditPage":
                    SPage.Source = null;
                    break;
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

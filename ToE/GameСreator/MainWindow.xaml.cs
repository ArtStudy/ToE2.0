using GalaSoft.MvvmLight.Messaging;
using GameСreator.DWIndows;
using GameСreator.Pages;
using GameСreator.ToePac;
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
        private AddBoss AddBossWindow;
        private AddLevel AddLevelWindow;

        public MainWindow()
        {
            InitializeComponent();

            Messenger.Default.Register<NavigatorPageMessege>(this, NavigatorPageAction);


            var data = new PAC();
        /*    data.Items.Add(new Item() { Data = "ТЕКСТ ТЕКСТ ТЕКСТ".GenerateStreamFromString(), FileType = FileTypes.txt, Identifier = 123 });
            data.Items.Add(new Item() { Data = "ТЕКСТ ТЕКСТ".GenerateStreamFromString(), FileType = FileTypes.txt, Identifier = 12345 });
            data.Items.Add(new Item() { Data = "ТЕКСТ".GenerateStreamFromString(), FileType = FileTypes.txt, Identifier = 12345678 });
            data.Items.Add(new Item() { Data = "ТЕКСТ ТЕКСТ ТЕКСТ".GenerateStreamFromString(), FileType = FileTypes.txt, Identifier = 123 });
            data.Items.Add(new Item() { Data = "ТЕКСТ ТЕКСТ".GenerateStreamFromString(), FileType = FileTypes.txt, Identifier = 12345 });
            data.Items.Add(new Item() { Data = "ТЕКСТ".GenerateStreamFromString(), FileType = FileTypes.txt, Identifier = 12345678 });
            data.Items.Add(new Item() { Data = "ТЕКСТ ТЕКСТ ТЕКСТ".GenerateStreamFromString(), FileType = FileTypes.txt, Identifier = 123 });
            data.Items.Add(new Item() { Data = "ТЕКСТ".GenerateStreamFromString(), FileType = FileTypes.Unknown, Name = "11111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111", Identifier = 12345678 });
            data.Items.Add(new Item() { Data = "ТЕКСТ".GenerateStreamFromString(), FileType = FileTypes.Unknown, Name = "1234", Identifier = 12345678 });

            data.Items.Add(new Item() { Data = "ТЕКСТ ТЕКСТ".GenerateStreamFromString(), FileType = FileTypes.txt, Identifier = 12345 });
            data.Items.Add(new Item() { Data = "ТЕКСТ".GenerateStreamFromString(), FileType = FileTypes.txt, Identifier = 12345678 });
            data.Items.Add(new Item() { Data = "ТЕКСТ ТЕКСТ ТЕКСТ".GenerateStreamFromString(), FileType = FileTypes.txt, Identifier = 123 });
            data.Items.Add(new Item() { Data = "ТЕКСТ ТЕКСТ".GenerateStreamFromString(), FileType = FileTypes.txt, Identifier = 12345 });
            data.Items.Add(new Item() { Data = "ТЕКСТ".GenerateStreamFromString(), FileType = FileTypes.txt, Identifier = 12345678 });
            data.Items.Add(new Item() { Data = "ТЕКСТ ТЕКСТ ТЕКСТ".GenerateStreamFromString(), FileType = FileTypes.txt, Identifier = 123 });
            data.Items.Add(new Item() { Data = "ТЕКСТ ТЕКСТ".GenerateStreamFromString(), FileType = FileTypes.txt, Identifier = 12345 });
            data.Items.Add(new Item() { Data = "ТЕКСТ".GenerateStreamFromString(), FileType = FileTypes.Unknown,  Name = "11111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111",  Identifier = 12345678 });
            var str = data.Serialization();
            var r =  new PAC(str);

            using (FileStream fs = new FileStream("1.txt", FileMode.OpenOrCreate))
            {
               ((MemoryStream) str).WriteTo(fs);
               // fs.CopyTo(str);
            }
              
        

            foreach (var item in r.Items)
            {
                StreamReader reader = new StreamReader( item.Data);
                string text = reader.ReadToEnd();
                Debug.WriteLine(item.FileType + " " +item.Identifier  + " " +text + " " + item.Name);
            }
           
           */
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
                case "AddBossWindowOpen":
                    AddBossWindow?.Close();
                    AddBossWindow = new AddBoss();
                    AddBossWindow.ShowDialog();
                    break;
                case "AddBossWindowClose":
                    AddBossWindow?.Close();
                    AddBossWindow = null;
                    break;
                case "BossesPage":
                    MainPage.Source = new Uri("Pages\\BossPage.xaml", UriKind.Relative);
                    break;
                case "LevelPage":
                    MainPage.Source = new Uri("Pages\\LevelPage.xaml", UriKind.Relative);
                    break;
                case "AddLevelWindowOpen":
                    AddLevelWindow?.Close();
                    AddLevelWindow = new AddLevel();
                    AddLevelWindow.ShowDialog();
                    break;
                case "AddLevelWindowClose":
                    AddLevelWindow?.Close();
                    AddLevelWindow = null;
                    break;
                case "LevelEditPage":
                    SPage.Source = new Uri("Pages\\EditLevelPage.xaml", UriKind.Relative);
                    break;
            }
        }


    }
}

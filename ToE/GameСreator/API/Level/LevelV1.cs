using GameСreator.ToePac;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace GameСreator.API.Boss
{
    public class LevelV1 : INotifyPropertyChanged
    {
        private int _iD;
        private string _name;
        private int _health;
        private int _damage;

        /// <summary>
        /// Номер босса
        /// </summary>
        public int IDLevel { get => _iD; set { _iD = value; OnPropertyChanged("ID"); } }
        /// <summary>
        /// Имя босса
        /// </summary>
        public string NameLavel { get => _name; set { _name = value; OnPropertyChanged("Name"); }}

       
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        public ListResourse ToListResourse()
        {
            TypeLevel tl = new TypeLevel();
            tl.IDLevel = this.IDLevel;
            tl.NameLavel = "Lavel." + this.NameLavel;
         
            MemoryStream ms = new MemoryStream();
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(TypeLevel));
            ser.WriteObject(ms, tl);

            Item item = new Item();
            item.FileType = FileTypes.Lavel;
            item.Identifier = ("Json." + tl.NameLavel).GetUInt64HashCode();
            item.Name = "Json." + tl.NameLavel;
            item.Version = 1;
            item.Data = new MemoryStream( ms.ToArray());

            return new ListResourse() { item };
        }
        public LevelV1() { }
        public LevelV1(Item item, ListResourse list)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(TypeLevel));
            item.Data.Position = 0;
            TypeLevel tb = (TypeLevel) ser.ReadObject(item.Data);
            this.IDLevel = tb.IDLevel;
            this.NameLavel =  tb.NameLavel.Replace("Lavel.", "");

        }

    }
}

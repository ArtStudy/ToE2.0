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
    public class BossV1 : INotifyPropertyChanged
    {
        private int _iD;
        private string _name;
        private int _health;
        private int _damage;

        /// <summary>
        /// Номер босса
        /// </summary>
        public int ID { get => _iD; set { _iD = value; OnPropertyChanged("ID"); } }
        /// <summary>
        /// Имя босса
        /// </summary>
        public string Name { get => _name; set { _name = value; OnPropertyChanged("Name"); }}

        /// <summary>
        /// Здоровье
        /// </summary>
        public int Health { get => _health; set { _health = value; OnPropertyChanged("Health"); }  }
        /// <summary>
        /// Урон
        /// </summary>
        public int Damage { get => _damage; set { _damage = value; OnPropertyChanged("Damage"); }  }

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
            TypeBoss tb = new TypeBoss();
            tb.ID = this.ID;
            tb.Name = "Boss."+this.Name;
            tb.Health = this.Health;
            tb.Damage = this.Damage;

            MemoryStream ms = new MemoryStream();
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(TypeBoss));
            ser.WriteObject(ms, tb);

            Item item = new Item();
            item.FileType = FileTypes.Boss;
            item.Identifier = ("Json." + tb.Name).GetUInt64HashCode();
            item.Name = "Json." + tb.Name;
            item.Version = 1;
            item.Data = new MemoryStream( ms.ToArray());

            return new ListResourse() { item };
        }
        public BossV1() { }
        public BossV1(Item item, ListResourse list)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(TypeBoss));
            item.Data.Position = 0;
            TypeBoss tb = (TypeBoss) ser.ReadObject(item.Data);
            this.ID = tb.ID;
            this.Name =  tb.Name.Replace("Boss.", "");
            this.Health = tb.Health;
            this.Damage = tb.Damage;
        }

    }
}

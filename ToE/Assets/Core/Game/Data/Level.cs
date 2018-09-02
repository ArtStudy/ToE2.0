using Assets.Core.Levels;
using Assets.Core.Levels.Questions;
using Assets.Core.LevelsStructureInterfaces;
using Assets.Core.Serialization;
using Assets.Core.ToePac;
using Assets.Core.Volutes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Core.Game.Data
{
   public  class Level : ILevel 
    {
        // Идентификатор
        public int ID { get; set; }
        public string Name { get; set; }

        // Цены
        public Money Price { get; set; }
        public Money Remuneration { get; set; }

        // Этапы
        public IEntranceTest EntranceTest { get; set; }
        public IPassageLevel PassageLevel { get; set; }
        public IBossFight BossFight { get; set; }

        //Вопросы
        public List<IQuestion> QuestionsLevel { get; set; }

        //
        public StateLevel StateLevel { get; set; }



        /// <summary>
        /// Родители уровня
        /// </summary>
        public List<ILevel> Parents { get; set; } = new List<ILevel>();

        public event PropertyChangedEventHandler PropertyChanged;
        private ListResourse curlist;
     /*   public ListResourse ToListResourse()
        {
            SerializableLevel sl = new SerializableLevel();
            sl.ID = this.ID;
            sl.Name = "Lavel." + this.Name;
            sl.Parents = this.Parents.ToArray();

            MemoryStream ms = new MemoryStream();
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(SerializableLevel));
            ser.WriteObject(ms, sl);

            if (this.curlist == null)
            {
                Item item = new Item();
                item.FileType = FileTypes.Lavel;
                item.Identifier = ("Json." + sl.Name).GetUInt64HashCode();
                item.Name = "Json." + sl.Name;
                item.Version = 1;
                item.Data = new MemoryStream(ms.ToArray());


                return new ListResourse() { item };
            }
            else
            {
                var dataitem = this.curlist.Find((item) => item.FileType == FileTypes.Lavel);
                dataitem.Data = new MemoryStream(ms.ToArray());
                return new ListResourse() { dataitem };
            }
        }
        public Level() { }
        public Level(Item item, ListResourse list) => ToRealObject(item, list);


        public void ToRealObject(Item item, ListResourse list)
        {
            this.curlist = new ListResourse { item };
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(SerializableLevel));
            item.Data.Position = 0;
            SerializableLevel tb = (SerializableLevel)ser.ReadObject(item.Data);
            this.ID = tb.ID;
            this.Name = tb.Name.Replace("Lavel.", "");
            this.Parents = tb.Parents?.ToList();

            //Код который споосбсвует работе старых пакето
            if (this.Parents == null)
                this.Parents = new List<int>();
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }*/
    }
}

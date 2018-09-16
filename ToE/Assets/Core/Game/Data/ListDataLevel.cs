
using Assets.Core.Serialization;
using Assets.Core.ToePac;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Core.Game.Data
{/*
    public class ListDataLevel : ListDataBase<Level>
    {
        private ListDataBoss _bosses;
        private ListDataQuestion _questions;
        public ListDataLevel (ToePackage pac, ListDataBoss bosses, ListDataQuestion questions)
        {
            _bosses = bosses;
            _questions = questions;
            var levelsresourse = pac.Items.GetResourcesByType(FileTypes.Level);
            Dictionary<DataItem<Level>, int[]> parentdata = new Dictionary<DataItem<Level>, int[]>();
            for (int i = 0; i < levelsresourse.Count; i++)
            {
                DataItem<Level> dataItem = new DataItem<Level>();
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(SerializableLevel));
                levelsresourse[i].Data.Position = 0;
                SerializableLevel s = (SerializableLevel)ser.ReadObject(levelsresourse[i].Data);
                dataItem.Value = new Level();

                dataItem.Value.Name = s.Name.Replace(StringNameData, "");
                dataItem.Value.Boss = bosses.FindById(s.Boss);
                dataItem.Value.TranslationIdentifier = s.TranslationIdentifier;
               // dataItem.Value.QuestionsLevel = s.QuestionsLevel == null ? new DataList<IQuestion>() : s.QuestionsLevel.ToList().ConvertAll((item) => questions.FindById(item));
                dataItem.Value.Price = new Volutes.Money(s.Price);
                dataItem.Value.Remuneration = new Volutes.Money(s.Remuneration);

                dataItem.ListResourse.Add(levelsresourse[i]);
                this.Add(dataItem);

            }

        }

        public override string StringNameData => "Level.";

        public override void ReLoad(DataItem<Level> obj)
        {
            ReLoad(obj, _bosses, _questions);
        }
        public void ReLoad(DataItem<Level> obj, ListDataBoss bosses, ListDataQuestion questions)
        {
            //Подгружаем уровни
            var levelsresourse = obj.ListResourse.GetResourcesByType(FileTypes.Level);
            for (int i = 0; i < levelsresourse.Count; i++)
            {

                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(SerializableLevel));
                levelsresourse[i].Data.Position = 0;
                SerializableLevel s = (SerializableLevel)ser.ReadObject(levelsresourse[i].Data);

                obj.Value.Name = s.Name.Replace(StringNameData, "");
                obj.Value.Boss = bosses.FindById(s.Boss);
                obj.Value.TranslationIdentifier = s.TranslationIdentifier;
          //      obj.Value.QuestionsLevel = s.QuestionsLevel == null ? new DataList<IQuestion>(): s.QuestionsLevel.ToList().ConvertAll((item) => questions.FindById(item));
                obj.Value.Price = new Volutes.Money(s.Price);
                obj.Value.Remuneration = new Volutes.Money(s.Remuneration);
               obj.Value.Parents.Clear();
                for (int j = 0; j < s.Parents.Length; j++)
                {
                   var r=  this.Find((item) => item.Value.ID == s.Parents[j]);
                    if (r != null)
                    {
                        obj.Value.Parents.Add(r.Value);
                    }

                }
            }
        }

        public override void Save(DataItem<Level> obj)
        {
            SerializableLevel s = new SerializableLevel();
            s.Name = StringNameData + obj.Value.Name;
            s.Parents = obj.Value.Parents.ConvertAll((item) => item.ID).ToArray();
            s.Boss = obj.Value.Boss.ID;
            s.QuestionsLevel = obj.Value.QuestionsLevel.ConvertAll((item) => item.ID).ToArray();
            s.TranslationIdentifier = obj.Value.TranslationIdentifier;
            s.Price = obj.Value.Price.ToArray();
            s.Remuneration = obj.Value.Remuneration.ToArray();
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(SerializableLevel));
            MemoryStream ms = new MemoryStream();
            ser.WriteObject(ms, s);

            var dataitem = obj.ListResourse.Find((item) => item.FileType == FileTypes.Level);
            if (dataitem == null)
            {
                Item item = new Item();
                item.FileType = FileTypes.Level;
                item.Identifier = ("Json." + s.Name).GetUInt64HashCode();
                item.Name = "Json." + s.Name;
                item.Version = 1;
                item.Data = new MemoryStream(ms.ToArray());
                obj.ListResourse.Add(item);
            }
            else
            {
                dataitem.Data = new MemoryStream(ms.ToArray());
            }
        }
    }*/
}

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
{
    public class ListDataLevel : ListDataBase<Level>
    {
        private ListDataBoss _bosses;
        public ListDataLevel (PAC pac, ListDataBoss bosses)
        {
            _bosses = bosses;
            var levelsresourse = pac.Items.GetResourcesByType(FileTypes.Level);
            Dictionary<DataItem<Level>, int[]> parentdata = new Dictionary<DataItem<Level>, int[]>();
            for (int i = 0; i < levelsresourse.Count; i++)
            {
                DataItem<Level> dataItem = new DataItem<Level>();
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(SerializableLevel));
                levelsresourse[i].Data.Position = 0;
                SerializableLevel s = (SerializableLevel)ser.ReadObject(levelsresourse[i].Data);
                dataItem.Value = new Level();
                dataItem.Value.ID = s.ID;
                dataItem.Value.Name = s.Name.Replace(StringNameData, "");
                dataItem.Value.Boss = bosses.FindById(s.Boss);
                dataItem.Value.TranslationIdentifier = s.TranslationIdentifier;

                if (s.Parents.Length > 0)
                    parentdata[dataItem] = s.Parents;
                dataItem.ListResourse.Add(levelsresourse[i]);
                this.Add(dataItem);

            }
            foreach (var item in parentdata)
            {
                for (int i = 0; i < item.Value.Length; i++)
                {
                    item.Key.Value.Parents.Add(this.Find((level) => level.Value.ID == item.Value[i])?.Value);
                }
            }
        }

        public override string StringNameData => "Level.";

        public override void ReLoad(DataItem<Level> obj)
        {
            ReLoad(obj, _bosses);
        }
        public void ReLoad(DataItem<Level> obj, ListDataBoss bosses)
        {
            //Подгружаем уровни
            var levelsresourse = obj.ListResourse.GetResourcesByType(FileTypes.Level);
            for (int i = 0; i < levelsresourse.Count; i++)
            {

                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(SerializableLevel));
                levelsresourse[i].Data.Position = 0;
                SerializableLevel s = (SerializableLevel)ser.ReadObject(levelsresourse[i].Data);

                obj.Value.ID = s.ID;
                obj.Value.Name = s.Name.Replace(StringNameData, "");
                obj.Value.Boss = bosses.FindById(s.Boss);
                obj.Value.TranslationIdentifier = s.TranslationIdentifier;


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
            s.ID = obj.Value.ID;
            s.Name = StringNameData + obj.Value.Name;
            s.Parents = obj.Value.Parents.ConvertAll((item) => item.ID).ToArray();
            s.Boss = obj.Value.Boss.ID;
            s.TranslationIdentifier = obj.Value.TranslationIdentifier;

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
    }
}

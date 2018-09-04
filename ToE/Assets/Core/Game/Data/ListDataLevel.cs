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
                SerializableLevel sLevel = (SerializableLevel)ser.ReadObject(levelsresourse[i].Data);
                dataItem.Value = new Level();
                dataItem.Value.ID = sLevel.ID;
                dataItem.Value.Name = sLevel.Name.Replace("Level.", "");
                dataItem.Value.Boss = bosses.FindById(sLevel.Boss);

                if (sLevel.Parents.Length > 0)
                    parentdata[dataItem] = sLevel.Parents;
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
                SerializableLevel sLevel = (SerializableLevel)ser.ReadObject(levelsresourse[i].Data);

                obj.Value.ID = sLevel.ID;
                obj.Value.Name = sLevel.Name.Replace("Level.", "");
                obj.Value.Boss = bosses.FindById(sLevel.Boss);


                obj.Value.Parents.Clear();
                for (int j = 0; j < sLevel.Parents.Length; j++)
                {
                    obj.Value.Parents.Add(this.Find((item) => item.Value.ID == sLevel.Parents[j]).Value);

                }
            }
        }

        public override void Save(DataItem<Level> obj)
        {
            SerializableLevel sl = new SerializableLevel();
            sl.ID = obj.Value.ID;
            sl.Name = "Level." + obj.Value.Name;
            sl.Parents = obj.Value.Parents.ConvertAll((item) => item.ID).ToArray();
            sl.Boss = obj.Value.Boss.ID;

          DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(SerializableLevel));
            MemoryStream ms = new MemoryStream();
            ser.WriteObject(ms, sl);

            var dataitem = obj.ListResourse.Find((item) => item.FileType == FileTypes.Level);
            if (dataitem == null)
            {
                Item item = new Item();
                item.FileType = FileTypes.Level;
                item.Identifier = ("Json." + sl.Name).GetUInt64HashCode();
                item.Name = "Json." + sl.Name;
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

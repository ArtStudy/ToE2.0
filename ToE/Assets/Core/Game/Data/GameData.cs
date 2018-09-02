using Assets.Core.Levels;
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
    public class GameData
    {
        public ListData<Level> Levels { get; } = new ListData<Level>();

        public GameData(PAC pac)
        {
            //Подгружаем уровни
            var levelsresourse =  pac.Items.GetResourcesByType(FileTypes.Lavel);
            Dictionary<DataItem<Level>, int[]> parentdata = new Dictionary<DataItem<Level>, int[]>();
            for(int i = 0; i < levelsresourse.Count;  i++)
            {
                DataItem<Level> dataItem = new DataItem<Level>();
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(SerializableLevel));
                levelsresourse[i].Data.Position = 0;
                SerializableLevel sLevel = (SerializableLevel)ser.ReadObject(levelsresourse[i].Data);
                dataItem.Value = new Level();
                dataItem.Value.ID = sLevel.ID;
                dataItem.Value.Name = sLevel.Name.Replace("Lavel.", "");

                if (sLevel.Parents.Length > 0)
                    parentdata[dataItem] = sLevel.Parents;
                dataItem.ListResourse.Add(levelsresourse[i]);
                Levels.Add(dataItem);
                
            }
            foreach(var item in parentdata)
            {
                for(int i = 0; i< item.Value.Length; i++)
                {
                    item.Key.Value.Parents.Add(Levels.Find((level) => level.Value.ID == item.Value[i])?.Value);
                }
            }
            // = new ListResourse { item };

            
          
      
        }
        public void ReLoadLevel (DataItem<Level> level)
        {
            //Подгружаем уровни
            var levelsresourse = level.ListResourse.GetResourcesByType(FileTypes.Lavel);
            for (int i = 0; i < levelsresourse.Count; i++)
            {

                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(SerializableLevel));
                levelsresourse[i].Data.Position = 0;
                SerializableLevel sLevel = (SerializableLevel)ser.ReadObject(levelsresourse[i].Data);

                level.Value.ID = sLevel.ID;
                level.Value.Name = sLevel.Name.Replace("Lavel.", "");


                level.Value.Parents.Clear();
                for (int j = 0; j < sLevel.Parents.Length; j++)
                {
                    level.Value.Parents.Add(Levels.Find((item) => item.Value.ID == sLevel.Parents[j]).Value);

                }
            }
        }
        public void SaveLevel(DataItem<Level> level)
        {
            SerializableLevel sl = new SerializableLevel();
            sl.ID = level.Value.ID;
            sl.Name = "Lavel." + level.Value.Name;
            sl.Parents = level.Value.Parents.ConvertAll((item) => item.ID).ToArray();

            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(SerializableLevel));
            MemoryStream ms = new MemoryStream();
            ser.WriteObject(ms, sl);

            var dataitem = level.ListResourse.Find((item) => item.FileType == FileTypes.Lavel);
            if (dataitem == null)
            {
                Item item = new Item();
                item.FileType = FileTypes.Lavel;
                item.Identifier = ("Json." + sl.Name).GetUInt64HashCode();
                item.Name = "Json." + sl.Name;
                item.Version = 1;
                item.Data = new MemoryStream(ms.ToArray());
                level.ListResourse.Add(item);
            }
            else
            {
                dataitem.Data = new MemoryStream(ms.ToArray());
            }
        }

        public PAC SaveToPAC()
        {
            PAC pac = new PAC();


            //Преобразуем уровни уровни
            for(int i = 0; i < Levels.Count; i++)
            {
             /*   SerializableLevel sl = new SerializableLevel();
                sl.ID = this.Levels[i].Value.ID;
                sl.Name = "Lavel." + this.Levels[i].Value.Name;
                sl.Parents = this.Levels[i].Value.Parents.ConvertAll((item)=> item.ID).ToArray();

                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(SerializableLevel));
                MemoryStream ms = new MemoryStream();
                ser.WriteObject(ms, sl);

                var dataitem = this.Levels[i].ListResourse.Find((item) => item.FileType == FileTypes.Lavel);
                if(dataitem == null)
                {
                    Item item = new Item();
                    item.FileType = FileTypes.Lavel;
                    item.Identifier = ("Json." + sl.Name).GetUInt64HashCode();
                    item.Name = "Json." + sl.Name;
                    item.Version = 1;
                    item.Data = new MemoryStream(ms.ToArray());
                    this.Levels[i].ListResourse.Add(item);
                }
                else
                {
                    dataitem.Data  = new MemoryStream(ms.ToArray());
                }*/

                pac.Items.AddRange(this.Levels[i].ListResourse);
            }


      

            return pac;


        }
    }
}

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
    public class ListDataBoss : ListDataBase<Boss>
    {
        public override void ReLoad(DataItem<Boss> obj)
        {
       
            var bosssresourse = obj.ListResourse.GetResourcesByType(FileTypes.Boss);
            for (int i = 0; i < bosssresourse.Count; i++)
            {

                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(SerializableBoss));
                bosssresourse[i].Data.Position = 0;
                SerializableBoss sBoss = (SerializableBoss)ser.ReadObject(bosssresourse[i].Data);

                obj.Value.ID = sBoss.ID;
                obj.Value.Name = sBoss.Name.Replace("Boss.", "");
                obj.Value.Damage = sBoss.Damage;
                obj.Value.Health = sBoss.Health;
            }
        }

        public override void Save(DataItem<Boss> obj)
        {
            SerializableBoss sb = new SerializableBoss();
            sb.ID = obj.Value.ID;
            sb.Name = "Boss." + obj.Value.Name;
            sb.Damage = obj.Value.Damage;
            sb.Health = obj.Value.Health;

            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(SerializableBoss));
            MemoryStream ms = new MemoryStream();
            ser.WriteObject(ms, sb);

            var dataitem = obj.ListResourse.Find((item) => item.FileType == FileTypes.Boss);
            if (dataitem == null)
            {
                Item item = new Item();
                item.FileType = FileTypes.Boss;
                item.Identifier = ("Json." + sb.Name).GetUInt64HashCode();
                item.Name = "Json." + sb.Name;
                item.Version = 1;
                item.Data = new MemoryStream(ms.ToArray());
                obj.ListResourse.Add(item);
            }
            else
            {
                dataitem.Data = new MemoryStream(ms.ToArray());
            }
        }
     
            public ListDataBoss(PAC pac)
        {
            var levelsresourse = pac.Items.GetResourcesByType(FileTypes.Boss);
            Dictionary<DataItem<Level>, int[]> parentdata = new Dictionary<DataItem<Level>, int[]>();
            for (int i = 0; i < levelsresourse.Count; i++)
            {
                DataItem<Boss> dataItem = new DataItem<Boss>();
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(SerializableBoss));
                levelsresourse[i].Data.Position = 0;
                SerializableBoss sBoss = (SerializableBoss)ser.ReadObject(levelsresourse[i].Data);
                dataItem.Value = new Boss();
                dataItem.Value.ID = sBoss.ID;
                dataItem.Value.Name = sBoss.Name.Replace("Boss.", "");
                dataItem.Value.Health = sBoss.Health;
                dataItem.Value.Damage = sBoss.Damage;

                dataItem.ListResourse.Add(levelsresourse[i]);
                this.Add(dataItem);
            }
  
        }
    }
}

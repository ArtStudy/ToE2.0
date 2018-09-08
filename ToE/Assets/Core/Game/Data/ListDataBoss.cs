using Assets.Core.LevelsStructureInterfaces;
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
        public override string StringNameData => "Boss.";

        public override void ReLoad(DataItem<Boss> obj)
        {
       
            var bosssresourse = obj.ListResourse.GetResourcesByType(FileTypes.Boss);
            for (int i = 0; i < bosssresourse.Count; i++)
            {

                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(SerializableBoss));
                bosssresourse[i].Data.Position = 0;
                SerializableBoss s = (SerializableBoss)ser.ReadObject(bosssresourse[i].Data);

                obj.Value.ID = s.ID;
                obj.Value.Name = s.Name.Replace(StringNameData, "");
                obj.Value.Damage = s.Damage;
                obj.Value.Health = s.Health;
                obj.Value.TranslationIdentifier = s.TranslationIdentifier;
            }
        }

        public override void Save(DataItem<Boss> obj)
        {
            SerializableBoss s = new SerializableBoss();
            s.ID = obj.Value.ID;
            s.Name = StringNameData + obj.Value.Name;
            s.Damage = obj.Value.Damage;
            s.Health = obj.Value.Health;
            s.TranslationIdentifier = obj.Value.TranslationIdentifier;

            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(SerializableBoss));
            MemoryStream ms = new MemoryStream();
            ser.WriteObject(ms, s);

            var dataitem = obj.ListResourse.Find((item) => item.FileType == FileTypes.Boss);
            if (dataitem == null)
            {
                Item item = new Item();
                item.FileType = FileTypes.Boss;
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
     
            public ListDataBoss(PAC pac)
        {
            var levelsresourse = pac.Items.GetResourcesByType(FileTypes.Boss);
            for (int i = 0; i < levelsresourse.Count; i++)
            {
                DataItem<Boss> dataItem = new DataItem<Boss>();
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(SerializableBoss));
                levelsresourse[i].Data.Position = 0;
                SerializableBoss s = (SerializableBoss)ser.ReadObject(levelsresourse[i].Data);
                dataItem.Value = new Boss();
                dataItem.Value.ID = s.ID;
                dataItem.Value.Name = s.Name.Replace(StringNameData, "");
                dataItem.Value.Health = s.Health;
                dataItem.Value.Damage = s.Damage;
                dataItem.Value.TranslationIdentifier = s.TranslationIdentifier;

               dataItem.ListResourse.Add(levelsresourse[i]);
                this.Add(dataItem);
            }
  
        }


    }
}

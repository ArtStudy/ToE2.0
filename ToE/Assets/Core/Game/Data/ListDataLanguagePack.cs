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
  /*  public class ListDataLanguagePack : ListDataBase<LanguagePack>
    {
        public override string StringNameData => "Lang.";

        public override void ReLoad(DataItem<LanguagePack> obj)
        {
            var bosssresourse = obj.ListResourse.GetResourcesByType(FileTypes.Language);
            for (int i = 0; i < bosssresourse.Count; i++)
            {

                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(SerializableLanguagePack));
                bosssresourse[i].Data.Position = 0;
                SerializableLanguagePack s= (SerializableLanguagePack)ser.ReadObject(bosssresourse[i].Data);

                obj.Value.Name = s.Name.Replace(StringNameData, "");
                obj.Value.LanguageData = s.LanguageData;
            }
        }

        public override void Save(DataItem<LanguagePack> obj)
        {
            SerializableLanguagePack s = new SerializableLanguagePack();

            s.Name = StringNameData + obj.Value.Name;
            s.LanguageData = obj.Value.LanguageData ;

             DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(SerializableLanguagePack));
            MemoryStream ms = new MemoryStream();
            ser.WriteObject(ms, s);

            var dataitem = obj.ListResourse.Find((item) => item.FileType == FileTypes.Language);
            if (dataitem == null)
            {
                Item item = new Item();
                item.FileType = FileTypes.Language;
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
        public ListDataLanguagePack(ToePackage pac)
        {
            var levelsresourse = pac.Items.GetResourcesByType(FileTypes.Language);
            for (int i = 0; i < levelsresourse.Count; i++)
            {
                DataItem<LanguagePack> dataItem = new DataItem<LanguagePack>();
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(SerializableLanguagePack));
                levelsresourse[i].Data.Position = 0;
                SerializableLanguagePack s = (SerializableLanguagePack)ser.ReadObject(levelsresourse[i].Data);
                dataItem.Value = new LanguagePack();
                dataItem.Value.Name = s.Name.Replace(StringNameData, "");
                dataItem.Value.LanguageData =  s.LanguageData;

                dataItem.ListResourse.Add(levelsresourse[i]);
                this.Add(dataItem);
            }
        }

  
    }*/
}

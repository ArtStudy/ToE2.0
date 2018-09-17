using Assets.Core.ToePac;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Core.Game.Data
{
    public class SaveGame
    {
        static string path = Path.Combine(Application.persistentDataPath, "save.ToePackage");
        static ToePackage TPSaves;
        static SaveGame()
        {
            if(File.Exists(path))
            {
                using (var fs = new FileStream(path, FileMode.Open))
                {
                    TPSaves = new ToePackage(fs);
                }
            }
            else
            {
                TPSaves = new ToePackage();
            }
           
               

        }
        public static T GetValue<T>(IBase obj, T defaultvalue, [CallerMemberName] string name = "")
        {
            Dictionary<string, T> savedata = new Dictionary<string, T>();
            var type = ((TypeDataAttribute)obj.GetType().GetCustomAttributes(typeof(TypeDataAttribute), false)[0]).Type;
            UInt64 id = obj.ID ^ (ulong)type;

            var r = TPSaves.Items.GetResourceByTypeAndIdentifier(FileTypes.SaveData, id);
            if (r == null)
            {
                return defaultvalue;
            }
            else
            {
                DataContractJsonSerializer SerializerSave = new DataContractJsonSerializer(typeof(Dictionary<string, T>));
                r.Data.Position = 0;
                Dictionary<string, T> resultditionary  = (Dictionary<string, T>)SerializerSave.ReadObject(r.Data);
                
                if(resultditionary.ContainsKey(name))
                {
                    return resultditionary[name];
                }
                else
                {
                    return defaultvalue;
                }
            }

        }

        public static void SetValue<T>(IBase obj, T value,  [CallerMemberName] string name = "")
        {
            Dictionary<string, T> savedata = new Dictionary<string, T>();
            var type = ((TypeDataAttribute)obj.GetType().GetCustomAttributes(typeof(TypeDataAttribute), false)[0]).Type;
            UInt64 id = obj.ID ^ (ulong)type;

            ResourceItem item = TPSaves.Items.GetResourceByTypeAndIdentifier(FileTypes.SaveData, id);
            DataContractJsonSerializer SerializerSave = new DataContractJsonSerializer(typeof(Dictionary<string, T>));
            if (item == null)
            {
                item = new ResourceItem();
                item.Identifier = id;
                item.FileType = FileTypes.SaveData;
                item.Version = 1;
                item.Name = "Json.Save." + type.ToString();
                TPSaves.Items.Add(item);
                using (MemoryStream ms = new MemoryStream())
                {

                    SerializerSave.WriteObject(ms, new Dictionary<string, T>());
                    item.Data = new MemoryStream(ms.ToArray());
                }
               
              
            }

            item.Data.Position = 0;
            Dictionary<string, T> resultditionary = (Dictionary<string, T>)SerializerSave.ReadObject(item.Data);

            resultditionary[name] = value;

            using (MemoryStream ms = new MemoryStream())
            {
                SerializerSave.WriteObject(ms, resultditionary);
                item.Data = new MemoryStream(ms.ToArray());
            }

               
            
            Save();
        }

        public static void Save()
        {
            using (var fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                fs.SetLength(0);
                TPSaves.Serialization().WriteTo(fs);
            }

           ;
        }
    }
}

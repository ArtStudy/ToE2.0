using Assets.Core.Game.Data.Level;
using Assets.Core.Game.Data.User;
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
        public const UInt64 IDUserData = 0x0001;
        public const UInt64 IDLevelData = 0x0002;
        static string path = Path.Combine(Application.persistentDataPath, "save.ToePackage");
        public static SaveLevelData SaveLevelData { get; }

        static ToePackage TPSaves;

        static SaveGame()
        {
            if (File.Exists(path))
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

            SaveLevelData = new SaveLevelData();
            var levelitem = TPSaves.Items.GetResourceByTypeAndIdentifier(FileTypes.SaveData, IDLevelData);
            if (levelitem != null)
            {
                using (var data = new BinaryReader(levelitem.Data))
                {
                    var stateLevelSavecount = data.ReadInt32();
                    SaveLevelData.StateLevel.Clear();

                    for (int i = 0; i < stateLevelSavecount; i++)
                    {
                        SaveLevelData.StateLevel.Add(data.ReadUInt64(), (StateLevel)data.ReadUInt64());
                    }


                }

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

        public static IUser GetUser()
        {
            List<ResourceItem> items = TPSaves.Items.GetResourcesByType(FileTypes.User);
            if (items.Count > 0)
            {
                var item = items[0];
            }
         
            return new User.User();
        }
    }
}

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
        private static SaveUserData SaveUserData { get; }

        private static SaveLevelData SaveLevelData { get; }
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

          ;
            var levelitem = TPSaves.Items.GetResourceByTypeAndIdentifier(FileTypes.SaveData, IDLevelData);
            if (levelitem != null)
            {

                SaveLevelData = ResourceConverter.ResourceToSaveLevel(levelitem, TPSaves.Items);
                SaveUserData = ResourceConverter.ResourceToSaveUser(levelitem, TPSaves.Items);
            }
          else
            {
                SaveLevelData = new SaveLevelData();
                SaveUserData = new SaveUserData();
            }



        }
        public static T GetValue<T>(ISaveData obj, T defaultvalue, [CallerMemberName] string name = "")
        {
            var pairs = GetProperty<T>(obj, name);
            if (pairs.ContainsKey(obj.ID))
            {
                return pairs[obj.ID];
            }
            else
            {
                return defaultvalue;
            }
           
        }

        public static void SetValue<T>(ISaveData obj, T value,  [CallerMemberName] string name = "")
        {
            var pairs = GetProperty<T>(obj, name);
            pairs[obj.ID] = value;

            Save();
        }


        private static Dictionary<UInt64, T> GetProperty<T>(ISaveData obj, string name)
        {
            var type = ((TypeDataAttribute)obj.GetType().GetCustomAttributes(typeof(TypeDataAttribute), false)[0]).Type;
            IBase @base = null;
            switch (type)
            {
                case FileTypes.Level:
                    @base = SaveLevelData;
                    break;
                case FileTypes.User:
                    @base = SaveUserData;
                    break;
            }

            var typebase = @base.GetType();
            var property = typebase.GetProperty(name);
            if (property == null)
            {
                throw new Exception("Отсутвует поле сохранения");
            }


            Dictionary<UInt64, T> pairs = (Dictionary<UInt64, T>)property.GetValue(@base);
            return pairs;
        }
        public static void Save()
        {
            TPSaves.Items.Clear();

            TPSaves.Items.AddRange(ResourceConverter.ToResource(SaveLevelData));

            TPSaves.Items.AddRange(ResourceConverter.ToResource(SaveUserData));
            using (var fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                fs.SetLength(0);
                TPSaves.Serialization().WriteTo(fs);
            }

           ;
        }

       
    }
}

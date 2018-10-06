using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Assets.Core.Game.Data;
using Assets.Core.Game.Data.Age;
using Assets.Core.Game.Data.Boss;
using Assets.Core.Game.Data.Cultures;
using Assets.Core.Game.Data.Inventor;
using Assets.Core.Game.Data.Level;
using Assets.Core.Game.Data.Question;
using Assets.Core.Game.Data.User;
using Assets.Core.LevelsStructureInterfaces;

using Assets.Core.Volutes;

namespace Assets.Core.ToePac {
    public partial class ResourceConverter {

        

   


        public static SaveLevelData ResourceToSaveLevel(ResourceItem obj, ListResourse lr)
        {

                obj.Data.Position = 0;
            SaveLevelData result = new SaveLevelData();
            using (var data = new BinaryReader(obj.Data, Encoding.UTF8, true))
            {
        
                var stateLevelSavecount = data.ReadInt32();
                result.StateLevel.Clear();

                for (int i = 0; i < stateLevelSavecount; i++)
                {
                    result.StateLevel.Add(data.ReadUInt64(), (StateLevel)data.ReadUInt64());
                }


            }
            return result;

      

        }
        public static SaveUserData ResourceToSaveUser(ResourceItem obj, ListResourse lr)
        {

            obj.Data.Position = 0;
            SaveUserData result = new SaveUserData();
            using (var data = new BinaryReader(obj.Data, Encoding.UTF8, true))
            {

                var stateLevelSavecount = data.ReadInt32();
                result.CorrectName.Clear();

                for (int i = 0; i < stateLevelSavecount; i++)
                {
                    result.CorrectName.Add(data.ReadUInt64(), data.ReadString());
                }


            }
            return result;



        }

        public static ListResourse ToResource(SaveUserData obj) {

            ResourceItem resourse = null;
            using (MemoryStream ms = new MemoryStream())
            {
                using (var data = new BinaryWriter(ms, Encoding.UTF8, true))
                {

                    data.Write(obj.CorrectName.Count); //Колличесво элементов
                  
                    for (int i = 0; i < obj.CorrectName.Count; i++)
                    {
                        data.Write(obj.CorrectName.ElementAt(i).Key);
                        data.Write(obj.CorrectName.ElementAt(i).Value);
                    }

                }
                resourse = CreateItem(obj.ID, obj.Name, FileTypes.SaveData, ms);
            }

            return new ListResourse { resourse };





        }
        public static ListResourse ToResource(SaveLevelData obj)
        {

            ResourceItem resourse = null;
            using (MemoryStream ms = new MemoryStream())
            {
                using (var data = new BinaryWriter(ms, Encoding.UTF8, true))
                {

                    data.Write(obj.StateLevel.Count); //Колличесво элементов

                    for (int i = 0; i < obj.StateLevel.Count; i++)
                    {
                        data.Write(obj.StateLevel.ElementAt(i).Key);
                        data.Write((UInt64)obj.StateLevel.ElementAt(i).Value);
                    }

                }
                resourse = CreateItem(obj.ID, obj.Name, FileTypes.SaveData, ms);
            }

            return new ListResourse { resourse };





        }


    }
}
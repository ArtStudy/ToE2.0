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
using Assets.Core.LevelsStructureInterfaces;

using Assets.Core.Volutes;

namespace Assets.Core.ToePac {
    public partial class ResourceConverter {

        

   

        /// <summary>
        /// Преобразование ресурса в объект типа уровень
        /// </summary>
        /// <param name="obj">Преобразуемый ресурс</param>
        /// <param name="lr">список всех ресурсов</param>
        /// <param name="bosses">Боссы</param>
        /// <param name="questions">Вопросы</param>
        /// <param name="levels">Уровни</param>
        /// <returns></returns>
        public static SaveLevelData ResourceToSaveLevel(ResourceItem obj, ListResourse lr)
        {

                obj.Data.Position = 0;
            SaveLevelData result = new SaveLevelData();
            using (var data = new BinaryReader(obj.Data))
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
        
        /// <summary>
        /// Преобразует ILevel в масив ресуров
        /// </summary>
        /// <param name="level">Объект уровня</param>
        /// <returns>Массив русурсов</returns>
        public static ListResourse SaveLevelToResource(SaveLevelData obj) {

            ResourceItem resourse = null;
            using (MemoryStream ms = new MemoryStream())
            {
                using (var data = new BinaryWriter(ms))
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
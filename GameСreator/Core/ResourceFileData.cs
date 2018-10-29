
using Assets.Core.Game.Data;
using Assets.Core.Game.Data.Age;
using Assets.Core.Game.Data.Boss;
using Assets.Core.Game.Data.Cultures;
using Assets.Core.Game.Data.Inventor;
using Assets.Core.Game.Data.Level;
using Assets.Core.Game.Data.Question;
using Assets.Core.Game.Data.UI;
using Assets.Core.Levels;
using Assets.Core.LevelsStructureInterfaces;

using Assets.Core.ToePac;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace GameСreator.Core
{
    public class ResourceFileData
    {
       


      




        public Dictionary<FileTypes, Dictionary<IBase, ListResourse>> Data { get; } = new Dictionary<FileTypes, Dictionary<IBase, ListResourse>>
        {
            [FileTypes.Level] = new Dictionary<IBase, ListResourse>(),
            [FileTypes.Boss] = new Dictionary<IBase, ListResourse>(),
            [FileTypes.Question] = new Dictionary<IBase, ListResourse>(),
            [FileTypes.Language] = new Dictionary<IBase, ListResourse>(),
            [FileTypes.Age] = new Dictionary<IBase, ListResourse>(),
            [FileTypes.InventoryItem] = new Dictionary<IBase, ListResourse>(),
               [FileTypes.TextStyle] = new Dictionary<IBase, ListResourse>()
        };
            

        public ResourceFileData(ToePackage pac)
        {

       
            for (int i = 0; i < pac.Items.Count; i++)
            {
                pac.Items[i].Data.Position = 0;
             
                
                switch (pac.Items[i].FileType)
                {
                    case FileTypes.Level:

                     var   Levelresult = ResourceConverter.ResourceToLevel(pac.Items[i], pac.Items);
                        Data[pac.Items[i].FileType][Levelresult.Item1] = Levelresult.Item2;
                        break;
                    case FileTypes.Boss:
                       var Bossresult = ResourceConverter.ResourceToBoss(pac.Items[i], pac.Items);

                        Data[pac.Items[i].FileType][Bossresult.Item1] = Bossresult.Item2;
                        break;
                    case FileTypes.Question:

                        var Questionesult = ResourceConverter.ResourceToQuestion(pac.Items[i], pac.Items);

                        Data[pac.Items[i].FileType][Questionesult.Item1] = Questionesult.Item2;

                        break;
                    case FileTypes.Language:
                        var LanguagePacresult = ResourceConverter.ResourceToLanguagePack(pac.Items[i], pac.Items);

                        Data[pac.Items[i].FileType][LanguagePacresult.Item1] = LanguagePacresult.Item2;
                        break;
                    case FileTypes.Age:
                        var Ageresult = ResourceConverter.ResourceToAge(pac.Items[i], pac.Items);

                        Data[pac.Items[i].FileType][Ageresult.Item1] = Ageresult.Item2;
                        break;
                    case FileTypes.InventoryItem:
                        var InventoryItemresult = ResourceConverter.ResourceToInventoryItem(pac.Items[i], pac.Items);

                        Data[pac.Items[i].FileType][InventoryItemresult.Item1] = InventoryItemresult.Item2;
                        break;
                    case FileTypes.TextStyle:
                        var TextStyleresult = ResourceConverter.ResourceToTextStyle(pac.Items[i], pac.Items);

                        Data[pac.Items[i].FileType][TextStyleresult.Item1] = TextStyleresult.Item2;
                        break;


                }
             

            }
      

        }


        public ListResourse GetListResourse()
        {
            ListResourse result = new ListResourse();

            foreach(var item in this.Data)
            {
                foreach(var item2 in item.Value)
                {
                 
                    result.AddRange(item2.Value);
                }
            }

            return result;


        }

  


        public void ReLoad(IBase obj)
        {

            var typevalue = obj.GetType();
            FileTypes type = ((TypeDataAttribute)obj.GetType().GetCustomAttributes(typeof(TypeDataAttribute), false)[0]).Type;
            if (this.Data[type].ContainsKey(obj))
            {
                var baseobj = this.Data[type][obj].Find((item) => item.FileType == type);
                var lr = this.Data[type][obj];
                this.Data[type].Remove(obj);

                switch (type)
                {
                    case FileTypes.Level:

                        var Levelresult = ResourceConverter.ResourceToLevel(baseobj, lr);
                        Data[type][Levelresult.Item1] = Levelresult.Item2;
                        break;
                    case FileTypes.Boss:
                        var Bossresult = ResourceConverter.ResourceToBoss(baseobj, lr);

                        Data[type][Bossresult.Item1] = Bossresult.Item2;
                        break;
                    case FileTypes.Question:

                        var Questionesult = ResourceConverter.ResourceToQuestion(baseobj, lr);

                        Data[type][Questionesult.Item1] = Questionesult.Item2;

                        break;
                    case FileTypes.Language:
                        var LanguagePacresult = ResourceConverter.ResourceToLanguagePack(baseobj, lr);

                        Data[type][LanguagePacresult.Item1] = LanguagePacresult.Item2;
                        break;
                    case FileTypes.Age:

                        var Ageresult = ResourceConverter.ResourceToAge(baseobj, lr);
                        Data[type][Ageresult.Item1] = Ageresult.Item2;
                        break;
                    case FileTypes.InventoryItem:
                        var InventoryItemresult = ResourceConverter.ResourceToInventoryItem(baseobj, lr);

                        Data[type][InventoryItemresult.Item1] = InventoryItemresult.Item2;
                        break;
                    case FileTypes.TextStyle:
                        var TextStyleresult = ResourceConverter.ResourceToTextStyle(baseobj, lr);

                        Data[type][TextStyleresult.Item1] = TextStyleresult.Item2;
                        break;
                }
            }

        }

        public void Save(IBase obj)
        {
  
            FileTypes type = ((TypeDataAttribute)obj.GetType().GetCustomAttributes(typeof(TypeDataAttribute), false)[0]).Type;
            ListResourse lr = null;

            switch (type)
            {
                case FileTypes.Level:

                    lr = ResourceConverter.ToResource((ILevel)obj);
                    break;
                case FileTypes.Boss:
                    lr = ResourceConverter.ToResource((IBoss)obj);
                    break;
                case FileTypes.Question:

                    lr = ResourceConverter.ToResource((IQuestion)obj);

                    break;
                case FileTypes.Language:
                    lr = ResourceConverter.ToResource((ILanguagePack)obj);
                    break;
                case FileTypes.Age:

                    lr = ResourceConverter.ToResource((IAge)obj);
                    break;
                case FileTypes.InventoryItem:

                    lr = ResourceConverter.ToResource((IInventoryItem)obj);
                    break;
                case FileTypes.TextStyle:

                    lr = ResourceConverter.ToResource((ITextStyle)obj);
                    break;
            }



            Data[type][obj] = lr;


        }


    }
}

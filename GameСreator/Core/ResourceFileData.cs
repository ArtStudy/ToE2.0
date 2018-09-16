
using Assets.Core.Game.Data;
using Assets.Core.Game.Data.Age;
using Assets.Core.Game.Data.Boss;
using Assets.Core.Game.Data.Cultures;
using Assets.Core.Game.Data.Level;
using Assets.Core.Game.Data.Question;
using Assets.Core.Levels;
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
             [FileTypes.Age] = new Dictionary<IBase, ListResourse>()
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
            FileTypes type =  FileTypes.Unknown;
            if (typeof(ILevel).IsAssignableFrom(typevalue))
            {
                type = FileTypes.Level;
            }
            else if (typeof(IBoss).IsAssignableFrom(typevalue))
            {
                type = FileTypes.Boss;
            }
            else if (typeof(IQuestion).IsAssignableFrom(typevalue))
            {
                type = FileTypes.Question;
            }
            else if (typeof(ILanguagePack).IsAssignableFrom(typevalue))
            {
                type = FileTypes.Language;
            }
            else if (typeof(IAge).IsAssignableFrom(typevalue))
            {
                type = FileTypes.Age;
            }
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
                }
            }

        }

        public void Save(IBase obj)
        {
            var typevalue = obj.GetType();
            FileTypes type = FileTypes.Unknown;
            ListResourse lr = null;
            if (typeof(ILevel).IsAssignableFrom(typevalue))
            {
                type = FileTypes.Level;
                lr = ResourceConverter.LevelToResource((ILevel)obj);
            }
            else if (typeof(IBoss).IsAssignableFrom(typevalue))
            {
                type = FileTypes.Boss;
                lr = ResourceConverter.BossToResource((IBoss)obj);
            }
            else if (typeof(IQuestion).IsAssignableFrom(typevalue))
            {
                type = FileTypes.Question;
                lr = ResourceConverter.QuestionToResource((IQuestion)obj);
            }
            else if (typeof(ILanguagePack).IsAssignableFrom(typevalue))
            {
                type = FileTypes.Language;
                lr = ResourceConverter.LanguagePackToResource((ILanguagePack)obj);
            }
            else if (typeof(IAge).IsAssignableFrom(typevalue))
            {
                type = FileTypes.Age;
                lr = ResourceConverter.AgeToResource((IAge)obj);
            }

            Data[type][obj] = lr;


        }


    }
}

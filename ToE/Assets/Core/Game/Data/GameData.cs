
using Assets.Core.Game.Data.Boss;
using Assets.Core.Game.Data.Cultures;
using Assets.Core.Game.Data.Level;
using Assets.Core.Game.Data.Question;
using Assets.Core.Levels;
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
    public class GameData
    {
        public List<ILanguagePack> LanguagePacks { get; } = new List<ILanguagePack>();
        public List<ILevel> Levels { get; } = new List<ILevel>();
        public List<IBoss> Bosses { get; } = new List<IBoss>();
        public List<IQuestion> Questions { get; } = new List<IQuestion>();

        public GameData(ToePackage pac)
        {
            for (int i = 0; i < pac.Items.Count; i++)
            {
                pac.Items[i].Data.Position = 0;


                switch (pac.Items[i].FileType)
                {
                    case FileTypes.Level:

                        Levels.Add( ResourceConverter.ResourceToLevel(pac.Items[i], pac.Items, Bosses, Questions, Levels).Item1);
                      
                        break;
                    case FileTypes.Boss:
                        Bosses.Add( ResourceConverter.ResourceToBoss(pac.Items[i], pac.Items).Item1);

                        break;
                    case FileTypes.Question:

                       Questions.Add(ResourceConverter.ResourceToQuestion(pac.Items[i], pac.Items).Item1);

                        

                        break;
                    case FileTypes.Language:
                        LanguagePacks.Add( ResourceConverter.ResourceToLanguagePack(pac.Items[i], pac.Items).Item1);

                        break;

                }


            }


        }


  

    }
}

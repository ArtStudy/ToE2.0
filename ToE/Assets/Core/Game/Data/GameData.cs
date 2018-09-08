using Assets.Core.Data.Question;
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
        public ListDataLanguagePack LanguagePacks { get; }
        public ListDataLevel Levels { get; }
        public ListDataBoss Bosses { get; }
        public ListDataQuestion Questions { get; }

        public GameData(PAC pac)
        {
            LanguagePacks = new ListDataLanguagePack(pac);
            Questions = new ListDataQuestion(pac);
            Bosses = new ListDataBoss(pac);
            Levels = new ListDataLevel(pac, Bosses);


        }


        public PAC SaveToPAC()
        {
            PAC pac = new PAC();



            pac.Items.AddRange(this.LanguagePacks.GetListResourse());
            pac.Items.AddRange(this.Questions.GetListResourse());
            pac.Items.AddRange(this.Bosses.GetListResourse());
            pac.Items.AddRange(this.Levels.GetListResourse());


            return pac;


        }

    }
}

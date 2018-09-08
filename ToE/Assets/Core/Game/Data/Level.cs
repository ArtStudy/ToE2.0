using Assets.Core.Data.Question;
using Assets.Core.Levels;

using Assets.Core.LevelsStructureInterfaces;
using Assets.Core.Serialization;
using Assets.Core.ToePac;
using Assets.Core.Volutes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Core.Game.Data
{
    public class Level : ILevel
    {
        private string _name;

        // Идентификатор
        public int ID { get; set; }
        public string Name
        {
            get => _name; set
            {
                _name = value;
                if (String.IsNullOrWhiteSpace(TranslationIdentifier))
                {
                    TranslationIdentifier = "Level." + value;
                }
            }
        }

        // Цены
        public Money Price { get; set; }
        public Money Remuneration { get; set; }

        // Этапы
        public IEntranceTest EntranceTest { get; set; }
        public IPassageLevel PassageLevel { get; set; }
        public IBoss Boss { get; set; }

        //Вопросы
        public List<IQuestion> QuestionsLevel { get; set; }

        //
        public StateLevel StateLevel { get; set; }



        /// <summary>
        /// Родители уровня
        /// </summary>
        public List<ILevel> Parents { get; set; } = new List<ILevel>();

        public string TranslationIdentifier { get; set; }

        public string[] BasicLocalizationFields => new string[] { "Name", "Description" };

        public event PropertyChangedEventHandler PropertyChanged;


    }
}

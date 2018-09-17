
using Assets.Core.Game.Data.Boss;
using Assets.Core.Game.Data.Question;
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

namespace Assets.Core.Game.Data.Level
{
    [TypeDataAttribute(FileTypes.Level)]
    public class Level : ILevel
    {
        private string _name;

        // Идентификатор
        public UInt64 ID { get; set; }
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
        public Money Price { get; set; } = new Money(0, 0);
        public Money Remuneration { get; set; } = new Money(0, 0);

        // Этапы
        public IEntranceTest EntranceTest { get; set; }
        public IPassageLevel PassageLevel { get; set; }
        public IBoss Boss { get; set; }

        //Вопросы
        public DataList<IQuestion> QuestionsLevel { get; set; } = new DataList<IQuestion>();

        //
        public StateLevel StateLevel { get => SaveGame.GetValue<StateLevel>(this, StateLevel.Сlosed); set => SaveGame.SetValue<StateLevel>(this, value); }



        /// <summary>
        /// Родители уровня
        /// </summary>
        public DataList<ILevel> Parents { get; set; } = new DataList<ILevel>();

        public string TranslationIdentifier { get; set; }

        public string[] BasicLocalizationFields => new string[] { "Name", "Description" };


        public event PropertyChangedEventHandler PropertyChanged;

        public override string ToString() => $"{this.Name} ({this.ID})";
    }
}


using Assets.Core.Volutes;
using System.Collections.Generic;
using Assets.Core.Levels;
using Assets.Core.Game.Sorting;
using Assets.Core.Game.Data.Level;
using System.ComponentModel;
using System;

namespace Assets.Core.Game.Data.Age
{
    public class Age : IAge
    {
      
        private string _name;
        public Money Price { get; set; } = new Money(0,0);

        public bool Availability { get; set; }

        public DataList<ILevel> Levels { get; set; } = new DataList<ILevel>();
        public ulong ID { get; set; }
        public string Name
        {
            get => _name; set
            {
                _name = value;
                if (String.IsNullOrWhiteSpace(TranslationIdentifier))
                {
                    TranslationIdentifier = "Age." + value;
                }
            }
        }
        public string TranslationIdentifier { get; set; }

        public string[] BasicLocalizationFields => new string[] { "Name", "Description" };

        public IAge Parent { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;


        public override string ToString() => $"{this.Name} ({this.ID})";
    }

}
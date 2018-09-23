using Assets.Core.LevelsStructureInterfaces;
using Assets.Core.ToePac;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Core.Game.Data.Boss
{
    [TypeDataAttribute(FileTypes.Boss)]
    public class Boss : IBoss
    {
        private string _name;

        public uint Health { get; set; }

        public uint Damage { get; set; }

        public UInt64 ID { get; set; }
        public string Name
        {
            get => _name; set
            {
                _name = value;
                if (String.IsNullOrWhiteSpace(TranslationIdentifier))
                {
                    TranslationIdentifier = "Boss." + value;
                }

            }
        }


        public string TranslationIdentifier { get; set; }

        public string[] BasicLocalizationFields => new string[] { "Name", "Description" };

        public bool Loaded { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public override string ToString() => $"{this.Name} ({this.ID})";
    }
}

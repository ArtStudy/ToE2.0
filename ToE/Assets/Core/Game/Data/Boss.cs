using Assets.Core.LevelsStructureInterfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Core.Game.Data
{
    public class Boss : IBoss
    {
        private string _name;

        public int Health { get; set; }

        public int Damage { get; set; }

        public int ID { get; set; }
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

        public event PropertyChangedEventHandler PropertyChanged;
    }
}

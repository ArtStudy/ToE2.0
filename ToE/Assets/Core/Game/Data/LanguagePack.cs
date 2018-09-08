
using Assets.Core.Game.Data.Cultures;
using Assets.Core.LevelsStructureInterfaces;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Core.Game.Data
{
    public class LanguagePack : ILanguagePack
    {
        public CultureInfo Culture { get; set; }

        public LocalizationDictionary LanguageData { get; set; } =new LocalizationDictionary();
      
        public int ID { get => Culture.LCID; set => Culture = new CultureInfo(value); }
        public string Name { get => Culture.Name; set => Culture = new CultureInfo(value); }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}

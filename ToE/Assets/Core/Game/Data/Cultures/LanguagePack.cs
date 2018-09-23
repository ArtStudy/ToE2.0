
using Assets.Core.Game.Data.Cultures;
using Assets.Core.LevelsStructureInterfaces;
using Assets.Core.ToePac;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Core.Game.Data.Cultures
{
    [TypeDataAttribute(FileTypes.Language)]
    public class LanguagePack : ILanguagePack
    {
        public CultureInfo Culture { get; set; }

        public LocalizationDictionary LanguageData { get; set; } =new LocalizationDictionary();
      
        public ulong ID { get => (ulong) Culture.LCID; set => Culture = new CultureInfo((int)value); }
        public string Name { get => Culture.Name; set => Culture = new CultureInfo(value); }
        public bool Loaded { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public override string ToString() => $"{this.Name} ({this.ID})";
    }
}

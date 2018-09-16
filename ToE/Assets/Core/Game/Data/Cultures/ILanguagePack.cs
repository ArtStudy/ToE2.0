

using Assets.Core.Game.Data.Cultures;
using Assets.Core.LevelsStructureInterfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Core.Game.Data.Cultures
{
   public  interface ILanguagePack : IBase
    {
        CultureInfo Culture { get; }
        LocalizationDictionary LanguageData { get; set; }
    }
}

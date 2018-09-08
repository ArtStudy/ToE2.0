

using Assets.Core.Game.Data.Cultures;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Core.LevelsStructureInterfaces
{
    interface ILanguagePack : IBase
    {
        CultureInfo Culture { get; }
        LocalizationDictionary LanguageData { get;  }
    }
}

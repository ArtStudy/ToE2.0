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
        Dictionary<string, string> LanguageData { get;  }
    }
}

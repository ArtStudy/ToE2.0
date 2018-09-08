
using Assets.Core.Game.Data.Cultures;
using Assets.Core.Levels;

using Assets.Core.LevelsStructureInterfaces;

using Assets.Core.Volutes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Core.Serialization
{
    [DataContract]
    public class SerializableLanguagePack : SerializableBase
    {
         [DataMember]
        public LocalizationDictionary LanguageData { get; set; }
    }
}

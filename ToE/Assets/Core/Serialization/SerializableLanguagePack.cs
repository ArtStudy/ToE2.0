using Assets.Core.Levels;
using Assets.Core.Levels.Questions;
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
        public Dictionary<string, string> LanguageData { get; set; }
    }
}

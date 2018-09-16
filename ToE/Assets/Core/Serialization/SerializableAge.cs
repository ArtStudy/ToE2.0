using Assets.Core.Levels;

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
    public class SerializableAge : SerializableBase
    {
   
        [DataMember]
        public string TranslationIdentifier { get; set; }
        [DataMember]
        public ulong[] Levels { get; set; }
        [DataMember]
        public uint[] Price { get; set; } = new uint[2];
        [DataMember]
        public ulong Parent { get; set; }
    }
}

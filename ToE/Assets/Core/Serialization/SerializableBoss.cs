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
    public class SerializableBoss : SerializableBase
    {

        [DataMember]
        public int Health { get; set; }
        [DataMember]
        public int Damage { get; set; }
        [DataMember]
        public string TranslationIdentifier { get; set; }



    }
}

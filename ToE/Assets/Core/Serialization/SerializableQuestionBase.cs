using Assets.Core.Data.Question;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Core.Serialization
{
    [DataContract]
    public class SerializableQuestionBase : SerializableBase
    {
        [DataMember]
        public TypeQuestionEnum TypeQuestion { get; set; }
    }
}

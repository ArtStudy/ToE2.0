
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Core.Serialization
{
    [DataContract]
    public class SerializableQuestionSelectOne : SerializableQuestionBase
    {
        [DataMember]
        public string TranslationIdentifier { get; set; }
        [DataMember]
        /// <summary>
        /// Правильный ответ
        /// </summary>
        public int RightAnswer { get; set; }
        [DataMember]
        /// <summary>
        /// Колличесво ответов
        /// </summary>
        public int NumberAnswer { get; set; }


    }
}

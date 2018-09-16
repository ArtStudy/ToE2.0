using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Core.Serialization
{
    [DataContract]
    public class SerializableBase
    {



        [DataMember]
        /// <summary>
        /// Имя босса
        /// </summary>
        public string Name { get; set; }


      
    }
}

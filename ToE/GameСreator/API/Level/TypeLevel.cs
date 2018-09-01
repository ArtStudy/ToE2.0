using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GameСreator.API.Boss
{
    [DataContract]
    public class TypeLevel
    {
        [DataMember]
        /// <summary>
        /// Номер босса
        /// </summary>
        public int IDLevel { get; set; }

        [DataMember]
        /// <summary>
        /// Имя босса
        /// </summary>
        public string NameLavel { get; set; }

      
    }
}

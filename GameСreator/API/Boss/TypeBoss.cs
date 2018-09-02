using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GameСreator.API.Boss
{
    [DataContract]
    public class TypeBoss
    {
        [DataMember]
        /// <summary>
        /// Номер босса
        /// </summary>
        public int ID { get; set; }

        [DataMember]
        /// <summary>
        /// Имя босса
        /// </summary>
        public string Name { get; set; }

        [DataMember]
        /// <summary>
        /// Здоровье
        /// </summary>
        public int Health { get; set; }

        [DataMember]
        /// <summary>
        /// Урон
        /// </summary>
        public int Damage { get; set; }
    }
}

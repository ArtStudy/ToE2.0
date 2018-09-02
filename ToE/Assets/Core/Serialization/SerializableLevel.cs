using Assets.Core.Levels;
using Assets.Core.Levels.Questions;
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
    public class SerializableLevel : SerializableBase
    {


        // Цены
    //    [DataMember]
     //   public Money Price { get; set; }
     //   [DataMember]
     //   public Money Remuneration { get; set; }

        // Этапы
        [DataMember]
        public IEntranceTest EntranceTest { get; set; }
        [DataMember]
        public IPassageLevel PassageLevel { get; set; }
        [DataMember]
        public IBossFight BossFight { get; set; }

        //Вопросы
        [DataMember]
        public List<IQuestion> QuestionsLevel { get; set; }

        //
        [DataMember]
        public StateLevel StateLevel { get; set; }

        /// <summary>
        /// Родители уровня
        /// </summary>
            [DataMember]
        public int[] Parents { get; set; }

    }
}

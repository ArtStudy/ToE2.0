﻿using Assets.Core.Data.Question;
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
        public int Boss { get; set; }

        //Вопросы
        [DataMember]
        public int[] QuestionsLevel { get; set; }

        //
        [DataMember]
        public StateLevel StateLevel { get; set; }

        /// <summary>
        /// Родители уровня
        /// </summary>
            [DataMember]
        public int[] Parents { get; set; }
        [DataMember]
        public string TranslationIdentifier { get; set; }
        [DataMember]
        public int[] Price { get; set; } = new int[2];
        [DataMember]
        public int[] Remuneration { get; set; } = new int[2];

    }
}

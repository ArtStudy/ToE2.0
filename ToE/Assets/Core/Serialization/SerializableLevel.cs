﻿
using Assets.Core.Game.Data.Level;
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

        // Этапы
        [DataMember]
        public IEntranceTest EntranceTest { get; set; }
        [DataMember]
        public IPassageLevel PassageLevel { get; set; }
        [DataMember]
        public UInt64 Boss { get; set; }

        //Вопросы
        [DataMember]
        public UInt64[] QuestionsLevel { get; set; }

        //
        [DataMember]
        public StateLevel StateLevel { get; set; }

        /// <summary>
        /// Родители уровня
        /// </summary>
            [DataMember]
        public UInt64[] Parents { get; set; }
        [DataMember]
        public string TranslationIdentifier { get; set; }
        [DataMember]
        public uint[] Price { get; set; } = new uint[2];
        [DataMember]
        public uint[] Remuneration { get; set; } = new uint[2];

    }
}

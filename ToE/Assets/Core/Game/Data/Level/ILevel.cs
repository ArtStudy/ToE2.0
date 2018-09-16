
using Assets.Core.Game.Data;
using Assets.Core.Game.Data.Boss;
using Assets.Core.Game.Data.Cultures;
using Assets.Core.Game.Data.Question;
using Assets.Core.Levels;
using Assets.Core.LevelsStructureInterfaces;
using Assets.Core.Volutes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Core.Game.Data.Level
{
    public interface ILevel : IBase, IMulticulturalData
    {
       
        /// <summary>
        /// Цена
        /// </summary>
        Money Price { get; set; }
        /// <summary>
        /// Вознагрождение
        /// </summary>
        Money Remuneration { get; set; }
        /// <summary>
        /// Вступительный тест
        /// </summary>
        IEntranceTest EntranceTest { get; set; }
        /// <summary>
        /// Основное прохождение (Текст / Интерактив)
        /// </summary>
        IPassageLevel PassageLevel { get; set; }
        /// <summary>
        /// Босс
        /// </summary>
        IBoss Boss { get; set; }

        /// <summary>
        /// Вопросы уровня
        /// </summary>
        DataList<IQuestion> QuestionsLevel { get; set; }
        /// <summary>
        /// Статус уровня
        /// </summary>
        StateLevel StateLevel { get; set; }

        /// <summary>
        /// Родители уровня
        /// </summary>
        DataList<ILevel> Parents { get; set; }
  

    }
    public enum StateLevel
    {
        /// <summary>
        /// Незоступен
        /// </summary>
        NotAvailable = 0,
        /// <summary>
        /// Закрыт
        /// </summary>
        Сlosed = 2,
        /// <summary>
        /// Куплен
        /// </summary>
        Purchased = 4,
        /// <summary>
        /// Пройден вводный тест
        /// </summary>
        EntranceTest = 8,
        /// <summary>
        /// Пройдено изучение
        /// </summary>
        Study = 16,
        /// <summary>
        /// Битва с босом
        /// </summary>
        Boss = 32,
        /// <summary>
        /// Получение награды
        /// </summary>
        GettingReward = 64,
        /// <summary>
        /// Завершен
        /// </summary>
        Finished = 128,

    }
}

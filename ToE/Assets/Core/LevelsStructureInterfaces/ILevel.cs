using Assets.Core.Levels.Questions;
using Assets.Core.LevelsStructureInterfaces;
using Assets.Core.Volutes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Core.Levels
{
    public interface ILevel : IBase
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
        /// Борьба с боссом
        /// </summary>
        IBossFight BossFight { get; set; }

        /// <summary>
        /// Вопросы уровня
        /// </summary>
        List<IQuestion> QuestionsLevel { get; set; }
        /// <summary>
        /// Статус уровня
        /// </summary>
        StateLevel StateLevel { get; set; }

        /// <summary>
        /// Родители уровня
        /// </summary>
        List<ILevel> Parents { get; set; }
  

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

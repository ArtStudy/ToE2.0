using Assets.Core.Levels.Questions;
using Assets.Core.Volutes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Core.Levels
{
    public interface ILevel
    {
        /// <summary>
        /// Номер уровня
        /// </summary>
        int IDLevel { get; }
        /// <summary>
        /// Имя уровня
        /// </summary>
        string NameLavel { get; }

        /// <summary>
        /// Цена
        /// </summary>
        Money Price { get;  }
        /// <summary>
        /// Вознагрождение
        /// </summary>
        Money Remuneration { get; }
        /// <summary>
        /// Вступительный тест
        /// </summary>
        IEntranceTest EntranceTest { get; }
        /// <summary>
        /// Основное прохождение (Текст / Интерактив)
        /// </summary>
        IPassageLevel PassageLevel { get; }
        /// <summary>
        /// Борьба с боссом
        /// </summary>
        IBossFight BossFight { get; }

        /// <summary>
        /// Вопросы уровня
        /// </summary>
        IQuestion[] QuestionsLevel { get; }
        /// <summary>
        /// Статус уровня
        /// </summary>
        StateLevel StateLevel { get; }


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

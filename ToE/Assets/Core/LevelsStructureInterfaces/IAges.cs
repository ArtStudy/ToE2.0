using Assets.Core.Volutes;
using Assets.Core;
using Assets.Core.Levels;
using System.Collections.Generic;

namespace Assets.Core.Ages
{
    /// <summary>
    /// Интерфейс Эры
    /// </summary>
    interface IAges
    {
        /// <summary>
        /// Имя Эры
        /// </summary>
        string Name { get; }
        /// <summary>
        /// Номер Эры
        /// </summary>      
        int ID { get; }

        /// <summary>
        /// Цена перехода в Эру
        /// </summary>
        Money Price { get; }

        /// <summary>
        /// Доступность Эры
        /// </summary>
        bool Availability { get; }

        /// <summary>
        /// Уровни эры
        /// </summary>
        List<ILevel> Levels { get; }

    }
}

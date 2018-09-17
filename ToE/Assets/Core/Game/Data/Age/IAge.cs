using System.Collections.Generic;
using System.Collections.Generic;
using Assets.Core;
using Assets.Core.Game.Data.Cultures;
using Assets.Core.Game.Data.Level;
using Assets.Core.Levels;
using Assets.Core.Volutes;

namespace Assets.Core.Game.Data.Age {
    /// <summary>
    /// Интерфейс Эры
    /// </summary>
    public interface IAge : IBase, IMulticulturalData {
        /// <summary>
        /// Имя Эры
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// Номер Эры
        /// </summary>      
        ulong ID { get; set; }
        /// <summary>
        /// Цена перехода в Эру
        /// </summary>
        Money Price { get; set; }

        /// <summary>
        /// Доступность Эры
        /// </summary>
        bool Availability { get; set; }

        /// <summary>
        /// Уровни эры
        /// </summary>
        DataList<ILevel> Levels { get; set; }
        /// <summary>
        /// Родительская эра
        /// </summary>
        IAge Parent { get; set; }

        /// <summary>
        /// Сетка уровней
        /// </summary>
        Web Web { get; set; }

    }
}
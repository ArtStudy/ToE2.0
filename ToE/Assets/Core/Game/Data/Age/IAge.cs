using Assets.Core.Volutes;
using Assets.Core;
using Assets.Core.Levels;
using System.Collections.Generic;
using Assets.Core.Game.Data.Level;
using Assets.Core.Game.Data.Cultures;

namespace Assets.Core.Game.Data.Age
{
    /// <summary>
    /// Интерфейс Эры
    /// </summary>
   public interface IAge : IBase, IMulticulturalData
    {
       

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

    }
}

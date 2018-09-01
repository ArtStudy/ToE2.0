using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Core.Levels
{
    /// <summary>
    /// Интерфейс прохождения уровня
    /// </summary>
    public interface IPassageLevel
    {
        /// <summary>
        /// Тип прохождения уровня
        /// </summary>
        PassageLevelTypeEnum PassageLevelType { get; }
    }

    /// <summary>
    /// Тип прохождения уровня
    /// </summary>
   public enum PassageLevelTypeEnum
    {
        /// <summary>
        /// Статья
        /// </summary>
        Article = 2,
        /// <summary>
        /// Интератив
        /// </summary>
        Interactive = 4
    }
}

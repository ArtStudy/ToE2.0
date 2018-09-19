using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Core.Game.Data.Inventor
{
    public class Enhancements
    {
        /// <summary>
        /// Тип улучшения
        /// </summary>
        public TypeEnhancements TypeEnhancements { get; set; }
        /// <summary>
        /// Значение
        /// </summary>
        public double Value { get; set; }
    }
    public enum TypeEnhancements : byte
    {
        /// <summary>
        /// Добавочный тип
        /// </summary>
        Additional,
        /// <summary>
        /// Умножение
        /// </summary>
        Multiplier
    }
}

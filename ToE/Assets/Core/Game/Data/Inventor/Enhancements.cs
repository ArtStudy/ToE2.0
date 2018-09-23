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
        public TypeEnhancements TypeEnhancement { get; set; }
        /// <summary>
        /// Значение
        /// </summary>
        public double Value { get; set; }
    }
    public enum TypeEnhancements : ulong
    {
        /// <summary>
        /// Добавочный тип
        /// </summary>
        Additional = 2,
        /// <summary>
        /// Умножение
        /// </summary>
        Multiplier = 4
    }
}

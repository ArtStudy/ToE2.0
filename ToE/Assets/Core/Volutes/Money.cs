using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Core.Volutes
{
    public class Money
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="g">Золото</param>
        /// <param name="b">Мозги</param>
        public Money(int g, int b)
        {
            this.Gold = g;
            this.Brains = b;
        }
        /// <summary>
        /// Золото
        /// </summary>
        public int Gold { get; }
        /// <summary>
        /// Мозги
        /// </summary>
        public int Brains { get; }
    }
}

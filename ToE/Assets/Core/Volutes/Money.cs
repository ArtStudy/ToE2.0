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
        public Money(uint g, uint b)
        {
            this.Gold = g;
            this.Brains = b;
        }
        public Money(uint[] gb)
        {
            if (gb == null)
            {
                this.Gold = 0;
                this.Brains =0;
            }
            else
            {
                this.Gold = gb[0];
                this.Brains = gb[1];
            }
        }
        /// <summary>
        /// Золото
        /// </summary>
        public uint Gold { get; set; }
        /// <summary>
        /// Мозги
        /// </summary>
        public uint Brains { get; set; }

        public uint[] ToArray()
        {
            return new uint[] { Gold, Brains };
        }
    }
}

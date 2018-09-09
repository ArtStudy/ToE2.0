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
        public Money(int[] gb)
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
        public int Gold { get; set; }
        /// <summary>
        /// Мозги
        /// </summary>
        public int Brains { get; set; }

        public int[] ToArray()
        {
            return new int[] { Gold, Brains };
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Core.Levels.Bosses
{
    public interface IBoss
    {
        /// <summary>
        /// Номер босса
        /// </summary>
        int IDLevel { get; }
        /// <summary>
        /// Имя босса
        /// </summary>
        string NameLavel { get; }

        /// <summary>
        /// Здоровье
        /// </summary>
        int Health { get; }
        /// <summary>
        /// Урон
        /// </summary>
        int Damage { get; }
    }

    public enum BossTypeEnum
    {
        Standard = 2,
        Gold = 0x4
    }
}

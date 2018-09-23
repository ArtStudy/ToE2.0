using Assets.Core.Game.Data.Cultures;
using Assets.Core.LevelsStructureInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Core.Game.Data.Boss
{
    public interface IBoss : IBase, IMulticulturalData
    {
       
        /// <summary>
        /// Здоровье
        /// </summary>
        uint Health { get; }
        /// <summary>
        /// Урон
        /// </summary>
        uint Damage { get; }
    }

    public enum BossTypeEnum
    {
        Standard = 2,
        Gold = 0x4
    }
}

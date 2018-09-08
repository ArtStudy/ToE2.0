using Assets.Core.Game.Data.Cultures;
using Assets.Core.LevelsStructureInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Core.LevelsStructureInterfaces
{
    public interface IBoss : IBase, IMulticulturalData
    {
       
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

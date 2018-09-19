using Assets.Core.Game.Data.Cultures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Core.Game.Data.Inventor
{
   public  interface IInventoryItem : IBase, IMulticulturalData
    {
        Enhancements ImprovingHealth { get; }
        Enhancements ImproveResponseTime { get; }
    }
}

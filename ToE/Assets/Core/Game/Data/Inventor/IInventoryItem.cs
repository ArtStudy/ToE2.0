using Assets.Core.Game.Data.Cultures;
using Assets.Core.ToePac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Core.Game.Data.Inventor
{
    [TypeDataAttribute(FileTypes.InventoryItem)]
    public  interface IInventoryItem : IBase, IMulticulturalData
    {
        Enhancements ImprovingHealth { get; set; }
        Enhancements ImproveResponseTime { get; set; }
    }
}

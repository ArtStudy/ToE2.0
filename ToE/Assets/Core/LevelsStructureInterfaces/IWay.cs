using Assets.Core.Levels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Core.LevelsStructureInterfaces
{
    public interface IWay
    {
        ILevel Parent { get; }
        ILevel Child { get; }
    }
}

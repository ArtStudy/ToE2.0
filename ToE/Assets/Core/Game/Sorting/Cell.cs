using Assets.Core.Game.Data.Level;
using Assets.Core.Levels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Core.Game.Sorting
{
    public class Cell
    {
        public ILevel level { get; set; }
        public List<ILevel> parents { get; set; }
        public List<ILevel> childs { get; set; }
        

        public Cell(ILevel _level)
        {
            level = _level;
            parents = _level.Parents;
        }
    }
}

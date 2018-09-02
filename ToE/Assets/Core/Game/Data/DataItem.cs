using Assets.Core.LevelsStructureInterfaces;
using Assets.Core.ToePac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Core.Game.Data
{
    public class DataItem<T>  where T : IBase
    {
        public T Value { get; set; } 
        public ListResourse ListResourse { get; set; } = new ListResourse();

        public override string ToString()
        {
            return Value.ID + " " + Value.Name;
        }
    }
}

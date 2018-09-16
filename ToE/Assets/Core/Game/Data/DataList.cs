using Assets.Core.LevelsStructureInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Core.Game.Data
{
    public class DataList<T> : List<T> where T : IBase
    {

        public new void Add(T item)
        {
            var fit = this.Find((it) => it.ID == item.ID);
            if (fit != null)
                this.Remove(fit);

            base.Add(item);

        }
    }
}

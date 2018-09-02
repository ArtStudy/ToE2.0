using Assets.Core.ToePac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Core.LevelsStructureInterfaces
{
   public  interface IResourceHandler
    {

        ListResourse ToListResourse();
        void ToRealObject (Item item, ListResourse list);
    }
}

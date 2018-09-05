using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Core.LevelsStructureInterfaces
{
    public  interface IBase : INotifyPropertyChanged
    {
        /// <summary>
        /// Номер 
        /// </summary>
        int ID { get; set; }
        /// <summary>
        /// Имя 
        /// </summary>
        string Name { get; set; }

   

    }
}

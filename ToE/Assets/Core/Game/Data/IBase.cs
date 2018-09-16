using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Core.Game.Data
{
    public  interface IBase : INotifyPropertyChanged
    {
        /// <summary>
        /// Номер 
        /// </summary>
        UInt64 ID { get; set; }
        /// <summary>
        /// Имя 
        /// </summary>
        string Name { get; set; }


    }
}

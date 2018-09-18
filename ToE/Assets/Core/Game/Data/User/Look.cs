using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Core
{
    public class Look
    {
        /// <summary>
        /// Тип внешности
        /// </summary>
        LookTypeEnum LookType { get; } 

        /// <summary>
        /// Конструктор внешности, в зависимости от типа
        /// </summary>
        Look()
        {
        
        }
    }
    
    /// <summary>
    /// Варианты типа внешности
    /// </summary>
    public enum LookTypeEnum
    {
        
    }
}

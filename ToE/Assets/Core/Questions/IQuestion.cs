using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Core.Levels.Questions
{
    public interface IQuestion
    {
        /// <summary>
        /// Номер вопроса
        /// </summary>
        int IDQuestion { get; }
        /// <summary>
        /// Тип вопроса
        /// </summary>
        TypeQuestionEnum TypeQuestion { get; }
    /*    /// <summary>
        /// Урон
        /// </summary>
        int Damage { get; }*/
    }

    /// <summary>
    /// Тип вопроса
    /// </summary>
    public enum TypeQuestionEnum
    {
        FourOptions = 2 
        
    }
}

using Assets.Core.Game.Data.Cultures;
using Assets.Core.LevelsStructureInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Core.Data.Question
{
    public interface IQuestion : IBase, IMulticulturalData
    {

        /// <summary>
        /// Тип вопроса
        /// </summary>
        TypeQuestionEnum TypeQuestion { get; }

        bool AnswerCheck();
    }
}


using Assets.Core.ToePac;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Core.Game.Data.Question
{
    [TypeDataAttribute(FileTypes.Question)]
    public class QuestionSelectOne : IQuestion
    {
         private string _name;
        public TypeQuestionEnum TypeQuestion => TypeQuestionEnum.SelectOne;

        public UInt64 ID { get; set; }
        public string Name
        {
            get => _name; set
            {
                _name = value;
                if (String.IsNullOrWhiteSpace(TranslationIdentifier))
                {
                    TranslationIdentifier = "Question." + value;
                }
            }
        }

        /// <summary>
        /// Правильный ответ
        /// </summary>
        public uint RightAnswer { get; set; } = 1;
        /// <summary>
        /// Колличесво ответов
        /// </summary>
        public uint NumberAnswer { get; set; } = 4;
        /// <summary>
        /// Правильный ответ
        /// </summary>
        public int UserAnswer { get; set; }

        public string TranslationIdentifier { get; set; }

        public string[] BasicLocalizationFields
        {
            get
            {
                string[] result = new string[NumberAnswer + 3];
                result[0] = "Name";
                result[1] = "Description";
                result[2] = "Question";
                for (int i = 1; i <= NumberAnswer; i++)
                {
                    result[2 + i] = "Answer" + i;
                }

                return result;
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public bool AnswerCheck() => RightAnswer == UserAnswer;

        public override string ToString() => $"{this.Name} ({this.ID})";
    }
}

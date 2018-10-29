
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
    public class QuestionSelectLot : IQuestion
    {
        private string _name;

        public TypeQuestionEnum TypeQuestion => TypeQuestionEnum.SelectLot;

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
        /// Правильные ответы
        /// </summary>
        public List<uint> RightAnswers { get; set; }
        /// <summary>
        /// Колличесво ответов
        /// </summary>
        public uint NumberAnswer { get; set; } = 4;

        public List<uint> UserAnswers { get; set; }

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

        public double AnswerCheck() {
            Double preresult = 0;
            for (int i = 0; i < RightAnswers.Count; i++)
            {
                if (UserAnswers.Contains(RightAnswers[i]))
                    preresult += 1;

            }
            return preresult / RightAnswers.Count;
        }

        public override string ToString() => $"{this.Name} ({this.ID})";
    }
}

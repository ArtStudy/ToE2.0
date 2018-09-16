using Assets.Core.Game.Data;
using Assets.Core.Game.Data.Question;
using Assets.Core.LevelsStructureInterfaces;
using Assets.Core.Serialization;
using Assets.Core.ToePac;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Core.Game.Data.Question
{/*
    public class ListDataQuestion : ListDataBase<IQuestion>
    {
        public override string StringNameData => "Quest.";

        public override void ReLoad(DataItem<IQuestion> obj)
        {

            var resourse = obj.ListResourse.GetResourcesByType(FileTypes.Question);
            for (int i = 0; i < resourse.Count; i++)
            {
                DataItem<IQuestion> dataItem = new DataItem<IQuestion>();
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(SerializableQuestionBase));
                resourse[i].Data.Position = 0;
                SerializableQuestionBase s1 = (SerializableQuestionBase)ser.ReadObject(resourse[i].Data);
                if (s1.TypeQuestion == TypeQuestionEnum.SelectOne)
                {
                     ser = new DataContractJsonSerializer(typeof(SerializableQuestionSelectOne));
                    resourse[i].Data.Position = 0;
                    SerializableQuestionSelectOne s = (SerializableQuestionSelectOne)ser.ReadObject(resourse[i].Data);

                    QuestionSelectOne question = new QuestionSelectOne();

                    question.ID = resourse[i].Identifier;
                    question.Name = s.Name.Replace(StringNameData, "");
                    question.NumberAnswer = s.NumberAnswer;
                    question.RightAnswer = s.RightAnswer;
                    question.TranslationIdentifier = s.TranslationIdentifier;

                    obj.Value = question;
                }

              
            }
        }

        public override void Save(DataItem<IQuestion> obj)
        {
            if(obj.Value.TypeQuestion == TypeQuestionEnum.SelectOne)
            {

                SerializableQuestionSelectOne s = new SerializableQuestionSelectOne();
                var value = (QuestionSelectOne) obj.Value;
                s.Name = StringNameData + value.Name;
                s.NumberAnswer = value.NumberAnswer;
                s.RightAnswer = value.RightAnswer;
                s.TranslationIdentifier = value.TranslationIdentifier;
                s.TypeQuestion = value.TypeQuestion;

                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(SerializableQuestionSelectOne));
                MemoryStream ms = new MemoryStream();
                ser.WriteObject(ms, s);

                var dataitem = obj.ListResourse.Find((item) => item.FileType == FileTypes.Question);
                if (dataitem == null)
                {
                    Item item = new Item();
                    item.FileType = FileTypes.Question;
                    item.Identifier = ("Json." + s.Name).GetUInt64HashCode();
                    item.Name = "Json." + s.Name;
                    item.Version = 1;

                    item.Data = new MemoryStream(ms.ToArray());
                    obj.ListResourse.Add(item);
                }
                else
                {
                    dataitem.Data = new MemoryStream(ms.ToArray());
                }
            }


            
        }

        public ListDataQuestion(ToePackage pac)
        {
            var levelsresourse = pac.Items.GetResourcesByType(FileTypes.Question);
            for (int i = 0; i < levelsresourse.Count; i++)
            {
                DataItem<IQuestion> dataItem = new DataItem<IQuestion>();
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(SerializableQuestionBase));
                levelsresourse[i].Data.Position = 0;
                SerializableQuestionBase s1 = (SerializableQuestionBase)ser.ReadObject(levelsresourse[i].Data);
                if (s1.TypeQuestion == TypeQuestionEnum.SelectOne)
                {
                    ser = new DataContractJsonSerializer(typeof(SerializableQuestionSelectOne));
                    levelsresourse[i].Data.Position = 0;
                    SerializableQuestionSelectOne s = (SerializableQuestionSelectOne)ser.ReadObject(levelsresourse[i].Data);
                    QuestionSelectOne question = new QuestionSelectOne();

                    question.ID = levelsresourse[i].Identifier;
                    question.Name = s.Name.Replace(StringNameData, "");
                    question.NumberAnswer = s.NumberAnswer;
                    question.RightAnswer = s.RightAnswer;
                    question.TranslationIdentifier = s.TranslationIdentifier;

                    dataItem.Value = question;
                }


                dataItem.ListResourse.Add(levelsresourse[i]);
                this.Add(dataItem);
            }

        }


    }*/
}

using Assets.Core.Game.Data;
using Assets.Core.Game.Data.Boss;
using Assets.Core.Game.Data.Cultures;
using Assets.Core.Game.Data.Level;
using Assets.Core.Game.Data.Question;
using Assets.Core.LevelsStructureInterfaces;
using Assets.Core.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Core.ToePac
{
    public class ResourceConverter
    {

       private static Dictionary<ulong, List<ILevel>> bosslevelcashe = new Dictionary<ulong, List<ILevel>>();
        private static Dictionary<ulong, List<ILevel>> questionlevelcashe = new Dictionary<ulong, List<ILevel>>();
        private static Dictionary<ulong, List<ILevel>> levellevelcashe = new Dictionary<ulong, List<ILevel>>();



        private const string StringNameLevelData = "Level.";
        private const string StringNameBossData = "Boss.";
        private const string StringNameQuestionData = "Quest.";
        private const string StringNameLanguagePackData = "Lang.";

        private static DataContractJsonSerializer SerializerLevel = new DataContractJsonSerializer(typeof(SerializableLevel));
        private static DataContractJsonSerializer SerializerBoss = new DataContractJsonSerializer(typeof(SerializableBoss));
        private static DataContractJsonSerializer SerializerQuestionBase = new DataContractJsonSerializer(typeof(SerializableQuestionBase));
        private static DataContractJsonSerializer SerializerQuestionSelectOne = new DataContractJsonSerializer(typeof(SerializableQuestionSelectOne));
        private static DataContractJsonSerializer SerializerLanguagePack = new DataContractJsonSerializer(typeof(SerializableLanguagePack));

        /// <summary>
        /// Преобразование ресурса в объект типа уровень
        /// </summary>
        /// <param name="obj">Преобразуемый ресурс</param>
        /// <param name="lr">список всех ресурсов</param>
        /// <param name="bosses">Боссы</param>
        /// <param name="questions">Вопросы</param>
        /// <param name="levels">Уровни</param>
        /// <returns></returns>
        public static Tuple<ILevel, ListResourse> ResourceToLevel(Item obj, ListResourse lr, List<IBoss> bosses, List<IQuestion> questions,  List<ILevel> levels)
        {
            Level level = new Level();
            obj.Data.Position = 0;
            SerializableLevel slevel = (SerializableLevel)SerializerLevel.ReadObject(obj.Data);

            level.ID = obj.Identifier;
            level.Name = slevel.Name.Replace(StringNameLevelData, "");


            {  //Обработка босса уровня
                IBoss bosslevel = (IBoss)bosses.Find((item) => item.ID == slevel.Boss);
                if (bosslevel != null)
                {
                    level.Boss = bosslevel;
                }
                else
                {
                    level.Boss = new Boss { ID = slevel.Boss };
                    if (!bosslevelcashe.ContainsKey(slevel.Boss))
                        bosslevelcashe[slevel.Boss] = new List<ILevel>();
                    bosslevelcashe[slevel.Boss].Add(level);
                }
            }



            { //Обработка вопросов уровня
                level.QuestionsLevel = new DataList<IQuestion>();
                for (int j = 0; j < slevel.QuestionsLevel?.Length; j++)
                {
                    IQuestion qestionlevel = (IQuestion)questions.Find((item) => item.ID == slevel.QuestionsLevel[j]);
                    if (qestionlevel != null)
                    {
                        level.QuestionsLevel.Add(qestionlevel);
                    }
                    else
                    {
                        level.QuestionsLevel.Add(new QuestionSelectOne { ID = slevel.QuestionsLevel[j] });
                        if (!questionlevelcashe.ContainsKey(slevel.QuestionsLevel[j]))
                            questionlevelcashe[slevel.QuestionsLevel[j]] = new List<ILevel>();
                        questionlevelcashe[slevel.QuestionsLevel[j]].Add(level);
                    }
                }
            }
            //   
            level.TranslationIdentifier = slevel.TranslationIdentifier;

            level.Price = new Assets.Core.Volutes.Money(slevel.Price);
            level.Remuneration = new Assets.Core.Volutes.Money(slevel.Remuneration);

            { //Обработка родителей уровня 1
                level.Parents = new DataList<ILevel>();
                for (int j = 0; j < slevel.Parents?.Length; j++)
                {
                    ILevel levellevel = (ILevel)levels.Find((item) => item.ID == slevel.Parents[j]);
                    if (levellevel != null)
                    {
                        level.Parents.Add(levellevel);
                    }
                    else
                    {
                        level.Parents.Add(new Level { ID = slevel.Parents[j] });
                        if (!levellevelcashe.ContainsKey(slevel.Parents[j]))
                            levellevelcashe[slevel.Parents[j]] = new List<ILevel>();
                        levellevelcashe[slevel.Parents[j]].Add(level);
                    }
                }
            }

            { //Обработка родителей уровня 2
                if (levellevelcashe.ContainsKey(level.ID))
                {
                    for (int j = 0; j < levellevelcashe[level.ID].Count; j++)
                    {
                        levellevelcashe[level.ID][j].Parents.Add(level);
                    }
                }
            }
            return new Tuple<ILevel, ListResourse>(level, new ListResourse { obj });
        }
        /// <summary>
        /// Преобразование ресурса в объект типа босс
        /// </summary>
        /// <param name="obj">Преобразуемый ресурс</param>
        /// <param name="lr">список всех ресурсов</param>
        /// <returns></returns>
        public static Tuple<IBoss, ListResourse> ResourceToBoss(Item obj, ListResourse lr)
        {
            Boss boss = new Boss();
           
            obj.Data.Position = 0;

            SerializableBoss sboss = (SerializableBoss)SerializerBoss.ReadObject(obj.Data);
            boss.ID = obj.Identifier;
            boss.Name = sboss.Name.Replace(StringNameBossData, "");
            boss.Health = sboss.Health;
            boss.Damage = sboss.Damage;
            boss.TranslationIdentifier = sboss.TranslationIdentifier;
            //
            if (bosslevelcashe.ContainsKey(boss.ID))
            {
                for (int j = 0; j < bosslevelcashe[boss.ID].Count; j++)
                {
                    bosslevelcashe[boss.ID][j].Boss = boss;
                }
            }

            return new Tuple<IBoss, ListResourse>(boss, new ListResourse { obj });
        }
        /// <summary>
        /// Преобразование ресурса в объект типа вопрос
        /// </summary>
        /// <param name="obj">Преобразуемый ресурс</param>
        /// <param name="lr">список всех ресурсов</param>
        public static Tuple<IQuestion, ListResourse> ResourceToQuestion(Item obj, ListResourse lrp)
        {
            obj.Data.Position = 0;
            SerializableQuestionBase s1 = (SerializableQuestionBase)SerializerQuestionBase.ReadObject(obj.Data);
            if (s1.TypeQuestion == TypeQuestionEnum.SelectOne)
            {
                obj.Data.Position = 0;
                SerializableQuestionSelectOne squestion = (SerializableQuestionSelectOne)SerializerQuestionSelectOne.ReadObject(obj.Data);
                QuestionSelectOne question = new QuestionSelectOne();
             
                question.ID = obj.Identifier;
                question.Name = squestion.Name.Replace(StringNameQuestionData, "");
                question.NumberAnswer = squestion.NumberAnswer;
                question.RightAnswer = squestion.RightAnswer;
                question.TranslationIdentifier = squestion.TranslationIdentifier;


                if (questionlevelcashe.ContainsKey(question.ID))
                {
                    for (int j = 0; j < questionlevelcashe[question.ID].Count; j++)
                    {
                        questionlevelcashe[question.ID][j].QuestionsLevel.Add(question);
                    }
                }
                return new Tuple<IQuestion, ListResourse>(question, new ListResourse { obj });
            }

            return null;
        }
        /// <summary>
        /// Преобразование ресурса в объект типа языковой пакет
        /// </summary>
        /// <param name="obj">Преобразуемый ресурс</param>
        /// <param name="lr">список всех ресурсов</param>
        public static Tuple<ILanguagePack, ListResourse> ResourceToLanguagePack(Item obj, ListResourse lr)
        {
            obj.Data.Position = 0;
            ILanguagePack language = new LanguagePack();
         
            SerializableLanguagePack s = (SerializableLanguagePack)SerializerLanguagePack.ReadObject(obj.Data);
            language.Name = s.Name.Replace(StringNameLanguagePackData, "");
            language.LanguageData = s.LanguageData;


            return new Tuple<ILanguagePack, ListResourse>(language, new ListResourse { obj });

        }
        /// <summary>
        /// Преобразует ILevel в масив ресуров
        /// </summary>
        /// <param name="level">Объект уровня</param>
        /// <returns>Массив русурсов</returns>
        public static ListResourse  LevelToResource(ILevel level)
        {
            SerializableLevel sLevel = new SerializableLevel();
            sLevel.Name = StringNameLevelData + level.Name;
            sLevel.Parents = level.Parents.ConvertAll((parent) => parent.ID).ToArray();
            sLevel.Boss = level.Boss.ID;
            sLevel.QuestionsLevel = level.QuestionsLevel.ConvertAll((question) => question.ID).ToArray();
            sLevel.TranslationIdentifier = level.TranslationIdentifier;
            sLevel.Price = level.Price.ToArray();
            sLevel.Remuneration = level.Remuneration.ToArray();


            var resourse = CreateItem(level.ID, sLevel, FileTypes.Level, SerializerLevel);

            return new ListResourse { resourse };
        }
        /// <summary>
        /// Преобразует IBoss в масив ресуров
        /// </summary>
        /// <param name="boss">Объект босса</param>
        /// <returns>Массив русурсов</returns>
        public static ListResourse BossToResource (IBoss boss)
        {
            SerializableBoss sBoss = new SerializableBoss();
            sBoss.Name = StringNameBossData + boss.Name;
            sBoss.Damage = boss.Damage;
            sBoss.Health = boss.Health;
            sBoss.TranslationIdentifier = boss.TranslationIdentifier;


            var resourse = CreateItem(boss.ID, sBoss, FileTypes.Boss, SerializerBoss);


            return new ListResourse { resourse };
        }
        /// <summary>
        /// Преобразует IQuestion в масив ресуров
        /// </summary>
        /// <param name="questionbase">Объект вопроса</param>
        /// <returns>Массив русурсов</returns>
        public static ListResourse QuestionToResource(IQuestion questionbase)
        {

            Item resourse = null;
            switch (questionbase.TypeQuestion)
            {
                case TypeQuestionEnum.SelectOne:
                    QuestionSelectOne questionSelectOne = (QuestionSelectOne)questionbase;

                    SerializableQuestionSelectOne sQuestionSelectOne = new SerializableQuestionSelectOne();
                    ;
                    sQuestionSelectOne.Name = StringNameQuestionData + questionSelectOne.Name;
                    sQuestionSelectOne.NumberAnswer = questionSelectOne.NumberAnswer;
                    sQuestionSelectOne.RightAnswer = questionSelectOne.RightAnswer;
                    sQuestionSelectOne.TranslationIdentifier = questionSelectOne.TranslationIdentifier;
                    sQuestionSelectOne.TypeQuestion = questionSelectOne.TypeQuestion;



                    resourse = CreateItem(questionbase.ID, sQuestionSelectOne, FileTypes.Question, SerializerQuestionSelectOne);
                    break;
            }




            return new ListResourse { resourse };
        }
        /// <summary>
        /// Преобразует ILanguagePack в масив ресуров
        /// </summary>
        /// <param name="languagePack">Объект языкового пакета</param>
        /// <returns>Массив русурсов</returns>
        public static ListResourse LanguagePackToResource(ILanguagePack languagePack)
        {

            SerializableLanguagePack sLanguagePack = new SerializableLanguagePack();
            sLanguagePack.Name = StringNameLanguagePackData + languagePack.Name;
            sLanguagePack.LanguageData = languagePack.LanguageData;


            var resourse = CreateItem(languagePack.ID, sLanguagePack, FileTypes.Language, SerializerLanguagePack);


            return new ListResourse { resourse };
        }


        private static Item CreateItem(ulong ID, SerializableBase data, FileTypes type, DataContractJsonSerializer serializer)
        {
            if (ID == 0)
                throw new Exception("ID = 0");

            MemoryStream ms = new MemoryStream();

            serializer.WriteObject(ms, data);

            Item item = new Item();
            item.FileType = type;
            item.Identifier = ID;
            item.Name = "Json." + data.Name;
            item.Version = 1;
            item.Data = new MemoryStream(ms.ToArray());
            return item;


        }

    }
}

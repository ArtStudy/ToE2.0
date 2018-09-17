using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Assets.Core.Game.Data;
using Assets.Core.Game.Data.Age;
using Assets.Core.Game.Data.Boss;
using Assets.Core.Game.Data.Cultures;
using Assets.Core.Game.Data.Level;
using Assets.Core.Game.Data.Question;
using Assets.Core.LevelsStructureInterfaces;
using Assets.Core.Serialization;
using Assets.Core.Volutes;

namespace Assets.Core.ToePac {
    public class ResourceConverter {

        private static List<ILevel> LevelCashe = new List<ILevel> ();
        private static List<IBoss> BossCashe = new List<IBoss> ();
        private static List<IQuestion> QuestionCashe = new List<IQuestion> ();
        private static List<ILanguagePack> LanguagePackCashe = new List<ILanguagePack> ();
        private static List<IAge> AgeCashe = new List<IAge> ();

        private static Dictionary<ulong, List<ILevel>> bosslevelcashe = new Dictionary<ulong, List<ILevel>> ();
        private static Dictionary<ulong, List<ILevel>> questionlevelcashe = new Dictionary<ulong, List<ILevel>> ();
        private static Dictionary<ulong, List<ILevel>> levellevelcashe = new Dictionary<ulong, List<ILevel>> ();
        private static Dictionary<ulong, List<IAge>> levelagecashe = new Dictionary<ulong, List<IAge>> ();
        private static Dictionary<ulong, List<IAge>> ageagecashe = new Dictionary<ulong, List<IAge>> ();

        private const string StringNameLevelData = "Level.";
        private const string StringNameBossData = "Boss.";
        private const string StringNameQuestionData = "Quest.";
        private const string StringNameLanguagePackData = "Lang.";
        private const string StringNameAgeData = "Age.";

        private static DataContractJsonSerializer SerializerLevel = new DataContractJsonSerializer (typeof (SerializableLevel));
        private static DataContractJsonSerializer SerializerBoss = new DataContractJsonSerializer (typeof (SerializableBoss));
        private static DataContractJsonSerializer SerializerQuestionBase = new DataContractJsonSerializer (typeof (SerializableQuestionBase));
        private static DataContractJsonSerializer SerializerQuestionSelectOne = new DataContractJsonSerializer (typeof (SerializableQuestionSelectOne));
        private static DataContractJsonSerializer SerializerLanguagePack = new DataContractJsonSerializer (typeof (SerializableLanguagePack));
        private static DataContractJsonSerializer SerializerAge = new DataContractJsonSerializer (typeof (SerializableAge));

        /// <summary>
        /// Преобразование ресурса в объект типа уровень
        /// </summary>
        /// <param name="obj">Преобразуемый ресурс</param>
        /// <param name="lr">список всех ресурсов</param>
        /// <param name="bosses">Боссы</param>
        /// <param name="questions">Вопросы</param>
        /// <param name="levels">Уровни</param>
        /// <returns></returns>
        public static Tuple<ILevel, ListResourse> ResourceToLevel (ResourceItem obj, ListResourse lr) {
            Level level = new Level ();
            obj.Data.Position = 0;
            SerializableLevel slevel = (SerializableLevel) SerializerLevel.ReadObject (obj.Data);

            level.ID = obj.Identifier;
            level.Name = slevel.Name.Replace (StringNameLevelData, "");

            { //Обработка босса уровня
                IBoss bosslevel = BossCashe.Find ((item) => item.ID == slevel.Boss);
                if (bosslevel != null) {
                    level.Boss = bosslevel;
                } else {
                    level.Boss = new Boss { ID = slevel.Boss };
                    if (!bosslevelcashe.ContainsKey (slevel.Boss))
                        bosslevelcashe[slevel.Boss] = new List<ILevel> ();
                    bosslevelcashe[slevel.Boss].Add (level);
                }
            }

            { //Обработка вопросов уровня
                level.QuestionsLevel = new DataList<IQuestion> ();
                for (int j = 0; j < slevel.QuestionsLevel?.Length; j++) {
                    IQuestion qestionlevel = QuestionCashe.Find ((item) => item.ID == slevel.QuestionsLevel[j]);
                    if (qestionlevel != null) {
                        level.QuestionsLevel.Add (qestionlevel);
                    } else {
                        level.QuestionsLevel.Add (new QuestionSelectOne { ID = slevel.QuestionsLevel[j] });
                        if (!questionlevelcashe.ContainsKey (slevel.QuestionsLevel[j]))
                            questionlevelcashe[slevel.QuestionsLevel[j]] = new List<ILevel> ();
                        questionlevelcashe[slevel.QuestionsLevel[j]].Add (level);
                    }
                }
            }
            //   
            level.TranslationIdentifier = slevel.TranslationIdentifier;

            level.Price = new Money (slevel.Price);
            level.Remuneration = new Money (slevel.Remuneration);

            { //Обработка родителей уровня 1
                level.Parents = new DataList<ILevel> ();
                for (int j = 0; j < slevel.Parents?.Length; j++) {
                    ILevel levellevel = LevelCashe.Find ((item) => item.ID == slevel.Parents[j]);
                    if (levellevel != null) {
                        level.Parents.Add (levellevel);
                    } else {
                        level.Parents.Add (new Level { ID = slevel.Parents[j] });
                        if (!levellevelcashe.ContainsKey (slevel.Parents[j]))
                            levellevelcashe[slevel.Parents[j]] = new List<ILevel> ();
                        levellevelcashe[slevel.Parents[j]].Add (level);
                    }
                }
            }

            { //Обработка родителей уровня 2
                if (levellevelcashe.ContainsKey (level.ID)) {
                    for (int j = 0; j < levellevelcashe[level.ID].Count; j++) {
                        levellevelcashe[level.ID][j].Parents.Add (level);
                    }
                }
            } { //Обработка эр
                if (levelagecashe.ContainsKey (level.ID)) {
                    for (int j = 0; j < levellevelcashe[level.ID].Count; j++) {
                        levelagecashe[level.ID][j].Levels.Add (level);
                    }
                }
            }

            LevelCashe.Add (level);
            return new Tuple<ILevel, ListResourse> (level, new ListResourse { obj });
        }
        /// <summary>
        /// Преобразование ресурса в объект типа босс
        /// </summary>
        /// <param name="obj">Преобразуемый ресурс</param>
        /// <param name="lr">список всех ресурсов</param>
        /// <returns></returns>
        public static Tuple<IBoss, ListResourse> ResourceToBoss (ResourceItem obj, ListResourse lr) {
            Boss boss = new Boss ();

            obj.Data.Position = 0;

            SerializableBoss sboss = (SerializableBoss) SerializerBoss.ReadObject (obj.Data);
            boss.ID = obj.Identifier;
            boss.Name = sboss.Name.Replace (StringNameBossData, "");
            boss.Health = sboss.Health;
            boss.Damage = sboss.Damage;
            boss.TranslationIdentifier = sboss.TranslationIdentifier;
            //
            if (bosslevelcashe.ContainsKey (boss.ID)) {
                for (int j = 0; j < bosslevelcashe[boss.ID].Count; j++) {
                    bosslevelcashe[boss.ID][j].Boss = boss;
                }
            }
            BossCashe.Add (boss);
            return new Tuple<IBoss, ListResourse> (boss, new ListResourse { obj });
        }
        /// <summary>
        /// Преобразование ресурса в объект типа вопрос
        /// </summary>
        /// <param name="obj">Преобразуемый ресурс</param>
        /// <param name="lr">список всех ресурсов</param>
        public static Tuple<IQuestion, ListResourse> ResourceToQuestion (ResourceItem obj, ListResourse lrp) {
            obj.Data.Position = 0;
            SerializableQuestionBase s1 = (SerializableQuestionBase) SerializerQuestionBase.ReadObject (obj.Data);
            if (s1.TypeQuestion == TypeQuestionEnum.SelectOne) {
                obj.Data.Position = 0;
                SerializableQuestionSelectOne squestion = (SerializableQuestionSelectOne) SerializerQuestionSelectOne.ReadObject (obj.Data);
                QuestionSelectOne question = new QuestionSelectOne ();

                question.ID = obj.Identifier;
                question.Name = squestion.Name.Replace (StringNameQuestionData, "");
                question.NumberAnswer = squestion.NumberAnswer;
                question.RightAnswer = squestion.RightAnswer;
                question.TranslationIdentifier = squestion.TranslationIdentifier;

                if (questionlevelcashe.ContainsKey (question.ID)) {
                    for (int j = 0; j < questionlevelcashe[question.ID].Count; j++) {
                        questionlevelcashe[question.ID][j].QuestionsLevel.Add (question);
                    }
                }
                QuestionCashe.Add (question);
                return new Tuple<IQuestion, ListResourse> (question, new ListResourse { obj });
            }

            return null;
        }
        /// <summary>
        /// Преобразование ресурса в объект типа языковой пакет
        /// </summary>
        /// <param name="obj">Преобразуемый ресурс</param>
        /// <param name="lr">список всех ресурсов</param>
        public static Tuple<ILanguagePack, ListResourse> ResourceToLanguagePack (ResourceItem obj, ListResourse lr) {
            obj.Data.Position = 0;
            ILanguagePack language = new LanguagePack ();

            SerializableLanguagePack s = (SerializableLanguagePack) SerializerLanguagePack.ReadObject (obj.Data);
            language.Name = s.Name.Replace (StringNameLanguagePackData, "");
            language.LanguageData = s.LanguageData;

            LanguagePackCashe.Add (language);
            return new Tuple<ILanguagePack, ListResourse> (language, new ListResourse { obj });

        }
        /// <summary>
        /// Преобразование ресурса в объект типа эра
        /// </summary>
        /// <param name="obj">Преобразуемый ресурс</param>
        /// <param name="lr">список всех ресурсов</param>
        public static Tuple<IAge, ListResourse> ResourceToAge (ResourceItem obj, ListResourse lr) {
            obj.Data.Position = 0;
            IAge result = new Age ();

            SerializableAge s = (SerializableAge) SerializerAge.ReadObject (obj.Data);
            result.ID = obj.Identifier;
            result.Name = s.Name.Replace (StringNameAgeData, ""); { //Обработка уровней
                result.Levels = new DataList<ILevel> ();
                for (int j = 0; j < s.Levels?.Length; j++) {
                    ILevel levelage = LevelCashe.Find ((item) => item.ID == s.Levels[j]);
                    if (levelage != null) {
                        result.Levels.Add (levelage);
                    } else {
                        result.Levels.Add (new Level { ID = s.Levels[j] });
                        if (!levelagecashe.ContainsKey (s.Levels[j]))
                            levelagecashe[s.Levels[j]] = new List<IAge> ();
                        levelagecashe[s.Levels[j]].Add (result);
                    }
                }
            }

            { //Обработка родителя эры 1 
                if (s.Parent != 0) {
                    IAge AgeAge = AgeCashe.Find ((item) => (ulong) item.ID == s.Parent);
                    if (AgeAge != null) {
                        result.Parent = AgeAge;
                    } else {
                        result.Parent = new Age { ID = s.Parent };
                        if (!ageagecashe.ContainsKey (s.Parent))
                            ageagecashe[s.Parent] = new List<IAge> ();
                        ageagecashe[s.Parent].Add (result);
                    }
                }
            }

            result.Price = new Money (s.Price);

            { //Обработка родителя эры 2
                if (ageagecashe.ContainsKey ((ulong) result.ID)) {
                    for (int j = 0; j < levellevelcashe[(ulong) result.ID].Count; j++) {
                        ageagecashe[(ulong) result.ID][j].Parent = result;
                    }
                }
            }

            AgeCashe.Add (result);
            return new Tuple<IAge, ListResourse> (result, new ListResourse { obj });

        }
        /// <summary>
        /// Преобразует ILevel в масив ресуров
        /// </summary>
        /// <param name="level">Объект уровня</param>
        /// <returns>Массив русурсов</returns>
        public static ListResourse LevelToResource (ILevel level) {
            SerializableLevel sLevel = new SerializableLevel ();
            sLevel.Name = StringNameLevelData + level.Name;
            sLevel.Parents = level.Parents.ConvertAll ((parent) => parent.ID).ToArray ();
            sLevel.Boss = level.Boss.ID;
            sLevel.QuestionsLevel = level.QuestionsLevel.ConvertAll ((question) => question.ID).ToArray ();
            sLevel.TranslationIdentifier = level.TranslationIdentifier;
            sLevel.Price = level.Price.ToArray ();
            sLevel.Remuneration = level.Remuneration.ToArray ();

            var resourse = CreateItem (level.ID, sLevel, FileTypes.Level, SerializerLevel);

            return new ListResourse { resourse };
        }
        /// <summary>
        /// Преобразует IBoss в масив ресуров
        /// </summary>
        /// <param name="boss">Объект босса</param>
        /// <returns>Массив русурсов</returns>
        public static ListResourse BossToResource (IBoss boss) {
            SerializableBoss sBoss = new SerializableBoss ();
            sBoss.Name = StringNameBossData + boss.Name;
            sBoss.Damage = boss.Damage;
            sBoss.Health = boss.Health;
            sBoss.TranslationIdentifier = boss.TranslationIdentifier;

            var resourse = CreateItem (boss.ID, sBoss, FileTypes.Boss, SerializerBoss);

            return new ListResourse { resourse };
        }
        /// <summary>
        /// Преобразует IQuestion в масив ресуров
        /// </summary>
        /// <param name="questionbase">Объект вопроса</param>
        /// <returns>Массив русурсов</returns>
        public static ListResourse QuestionToResource (IQuestion questionbase) {

            ResourceItem resourse = null;
            switch (questionbase.TypeQuestion) {
                case TypeQuestionEnum.SelectOne:
                    QuestionSelectOne questionSelectOne = (QuestionSelectOne) questionbase;

                    SerializableQuestionSelectOne sQuestionSelectOne = new SerializableQuestionSelectOne ();;
                    sQuestionSelectOne.Name = StringNameQuestionData + questionSelectOne.Name;
                    sQuestionSelectOne.NumberAnswer = questionSelectOne.NumberAnswer;
                    sQuestionSelectOne.RightAnswer = questionSelectOne.RightAnswer;
                    sQuestionSelectOne.TranslationIdentifier = questionSelectOne.TranslationIdentifier;
                    sQuestionSelectOne.TypeQuestion = questionSelectOne.TypeQuestion;

                    resourse = CreateItem (questionbase.ID, sQuestionSelectOne, FileTypes.Question, SerializerQuestionSelectOne);
                    break;
            }

            return new ListResourse { resourse };
        }
        /// <summary>
        /// Преобразует ILanguagePack в масив ресуров
        /// </summary>
        /// <param name="languagePack">Объект языкового пакета</param>
        /// <returns>Массив русурсов</returns>
        public static ListResourse LanguagePackToResource (ILanguagePack languagePack) {

            SerializableLanguagePack sLanguagePack = new SerializableLanguagePack ();
            sLanguagePack.Name = StringNameLanguagePackData + languagePack.Name;
            sLanguagePack.LanguageData = languagePack.LanguageData;

            var resourse = CreateItem (languagePack.ID, sLanguagePack, FileTypes.Language, SerializerLanguagePack);

            return new ListResourse { resourse };
        }
        /// <summary>
        /// Преобразует IAge в масив ресуров
        /// </summary>
        /// <param name="languagePack">Объект эры</param>
        /// <returns>Массив русурсов</returns>
        public static ListResourse AgeToResource (IAge age) {

            SerializableAge s = new SerializableAge ();
            s.Name = StringNameAgeData + age.Name;
            s.Levels = age.Levels.ConvertAll ((parent) => parent.ID).ToArray ();
            s.Parent = (ulong) age.Parent.ID;
            s.Price = age.Price.ToArray ();
            s.TranslationIdentifier = age.TranslationIdentifier;

            var resourse = CreateItem ((ulong) age.ID, s, FileTypes.Age, SerializerAge);

            return new ListResourse { resourse };
        }

        private static ResourceItem CreateItem (ulong ID, SerializableBase data, FileTypes type, DataContractJsonSerializer serializer) {
            if (ID == 0)
                throw new Exception ("ID = 0");

            MemoryStream ms = new MemoryStream ();

            serializer.WriteObject (ms, data);

            ResourceItem item = new ResourceItem ();
            item.FileType = type;
            item.Identifier = ID;
            item.Name = "Json." + data.Name;
            item.Version = 1;
            item.Data = new MemoryStream (ms.ToArray ());
            return item;

        }

    }
}
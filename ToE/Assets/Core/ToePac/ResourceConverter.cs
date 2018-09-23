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
using Assets.Core.Game.Data.Inventor;
using Assets.Core.Game.Data.Level;
using Assets.Core.Game.Data.Question;
using Assets.Core.LevelsStructureInterfaces;

using Assets.Core.Volutes;

namespace Assets.Core.ToePac {
    public class ResourceConverter {

        private static List<ILevel> LevelCashe = new List<ILevel> ();
        private static List<IBoss> BossCashe = new List<IBoss> ();
        private static List<IQuestion> QuestionCashe = new List<IQuestion> ();
        private static List<ILanguagePack> LanguagePackCashe = new List<ILanguagePack> ();
        private static List<IAge> AgeCashe = new List<IAge> ();
        private static List<IInventoryItem> InventoryItemCashe = new List<IInventoryItem>();

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
        private const string StringNameInventoryItemData = "InvIt.";


   

        /// <summary>
        /// Преобразование ресурса в объект типа уровень
        /// </summary>
        /// <param name="obj">Преобразуемый ресурс</param>
        /// <param name="lr">список всех ресурсов</param>
        /// <param name="bosses">Боссы</param>
        /// <param name="questions">Вопросы</param>
        /// <param name="levels">Уровни</param>
        /// <returns></returns>
        public static Tuple<ILevel, ListResourse> ResourceToLevel(ResourceItem obj, ListResourse lr)
        {

                obj.Data.Position = 0;
                Level result = new Level();

                // SerializableInventoryItem s = new SerializableInventoryItem(obj.Data);
                using (var data = new BinaryReader(obj.Data, Encoding.UTF8, true))
                {
                    result.Name = data.ReadString();
                    result.ID = obj.Identifier;
                    result.TranslationIdentifier = data.ReadString();
               
                    { //Обработка босса уровня
                        var Isboss = data.ReadBoolean();
                        if (Isboss)
                        {
                            var bossid = data.ReadUInt64();
                            IBoss bosslevel = BossCashe.Find((item) => item.ID == bossid);
                            if (bosslevel != null)
                            {
                                result.Boss = bosslevel;
                            }
                            else
                            {
                                result.Boss = new Boss { ID = bossid };
                                if (!bosslevelcashe.ContainsKey(bossid))
                                    bosslevelcashe[bossid] = new List<ILevel>();
                                bosslevelcashe[bossid].Add(result);
                            }
                        }
                    }
                    { //Обработка вопросов уровня
                        var questioncount = data.ReadInt32();
                        result.QuestionsLevel = new DataList<IQuestion>();
                        for (int j = 0; j < questioncount; j++)
                        {
                            var questionid = data.ReadUInt64();
                            IQuestion qestionlevel = QuestionCashe.Find((item) => item.ID == questionid);
                            if (qestionlevel != null)
                            {
                                result.QuestionsLevel.Add(qestionlevel);
                            }
                            else
                            {
                                result.QuestionsLevel.Add(new QuestionSelectOne { ID = questionid });
                                if (!questionlevelcashe.ContainsKey(questionid))
                                    questionlevelcashe[questionid] = new List<ILevel>();
                                questionlevelcashe[questionid].Add(result);
                            }
                        }
                    }
                    result.Price = new Money(data.ReadUInt32(), data.ReadUInt32());
                    result.Remuneration = new Money(data.ReadUInt32(), data.ReadUInt32());

                    { //Обработка родителей уровня 1
                        result.Parents = new DataList<ILevel>();
                        var parentcount = data.ReadInt32();
                        for (int j = 0; j < parentcount; j++)
                        {
                            var parentid = data.ReadUInt64();
                            ILevel levellevel = LevelCashe.Find((item) => item.ID == parentid);
                            if (levellevel != null)
                            {
                                result.Parents.Add(levellevel);
                            }
                            else
                            {
                                result.Parents.Add(new Level { ID = parentid });
                                if (!levellevelcashe.ContainsKey(parentid))
                                    levellevelcashe[parentid] = new List<ILevel>();
                                levellevelcashe[parentid].Add(result);
                            }
                        }
                    }
                    { //Обработка родителей уровня 2
                        if (levellevelcashe.ContainsKey(result.ID))
                        {
                            for (int j = 0; j < levellevelcashe[result.ID].Count; j++)
                            {
                                levellevelcashe[result.ID][j].Parents.Add(result);
                            }
                        }
                    }
                    { //Обработка эр
                        if (levelagecashe.ContainsKey(result.ID))
                        {
                            for (int j = 0; j < levellevelcashe[result.ID].Count; j++)
                            {
                                levelagecashe[result.ID][j].Levels.Add(result);
                            }
                        }
                    }

                }

                LevelCashe.Add(result);
                return new Tuple<ILevel, ListResourse>(result, new ListResourse { obj });

      

        }
        /// <summary>
        /// Преобразование ресурса в объект типа босс
        /// </summary>
        /// <param name="obj">Преобразуемый ресурс</param>
        /// <param name="lr">список всех ресурсов</param>
        /// <returns></returns>
        public static Tuple<IBoss, ListResourse> ResourceToBoss(ResourceItem obj, ListResourse lr)
        {
 
                obj.Data.Position = 0;
                Boss result = new Boss();

                // SerializableInventoryItem s = new SerializableInventoryItem(obj.Data);
                using (var data = new BinaryReader(obj.Data, Encoding.UTF8, true))
                {
                    result.Name = data.ReadString();
                    result.ID = obj.Identifier;
                    result.TranslationIdentifier = data.ReadString();
                    result.Health = data.ReadUInt32();
                    result.Damage = data.ReadUInt32();
                }
                if (bosslevelcashe.ContainsKey(result.ID))
                {
                    for (int j = 0; j < bosslevelcashe[result.ID].Count; j++)
                    {
                        bosslevelcashe[result.ID][j].Boss = result;
                    }
                }
                BossCashe.Add(result);
                return new Tuple<IBoss, ListResourse>(result, new ListResourse { obj });
          
        }
        /// <summary>
        /// Преобразование ресурса в объект типа вопрос
        /// </summary>
        /// <param name="obj">Преобразуемый ресурс</param>
        /// <param name="lr">список всех ресурсов</param>
        public static Tuple<IQuestion, ListResourse> ResourceToQuestion (ResourceItem obj, ListResourse lrp) {

                obj.Data.Position = 0;
                IQuestion result = null;

                // SerializableInventoryItem s = new SerializableInventoryItem(obj.Data);
                using (var data = new BinaryReader(obj.Data, Encoding.UTF8, true))
                {
                    String name  =  data.ReadString();
                    String translationIdentifier = data.ReadString();

                    TypeQuestionEnum typeQuestion =(TypeQuestionEnum)data.ReadUInt64();
                    if(typeQuestion == TypeQuestionEnum.SelectOne)
                    {

                        QuestionSelectOne qsoresult = new QuestionSelectOne();
                        qsoresult.Name = name;
                        qsoresult.ID = obj.Identifier;
                        qsoresult.TranslationIdentifier = translationIdentifier;

                        qsoresult.NumberAnswer = data.ReadUInt32();
                        qsoresult.RightAnswer = data.ReadUInt32();
                        result = qsoresult;

                    }

   


                 
                }
                if (questionlevelcashe.ContainsKey(result.ID))
                {
                    for (int j = 0; j < questionlevelcashe[result.ID].Count; j++)
                    {
                        questionlevelcashe[result.ID][j].QuestionsLevel.Add(result);
                    }
                }
                QuestionCashe.Add(result);
                return new Tuple<IQuestion, ListResourse>(result, new ListResourse { obj });
      
          
        }
        /// <summary>
        /// Преобразование ресурса в объект типа языковой пакет
        /// </summary>
        /// <param name="obj">Преобразуемый ресурс</param>
        /// <param name="lr">список всех ресурсов</param>
        public static Tuple<ILanguagePack, ListResourse> ResourceToLanguagePack (ResourceItem obj, ListResourse lr) {

                obj.Data.Position = 0;
                ILanguagePack result = new LanguagePack();

                // SerializableInventoryItem s = new SerializableInventoryItem(obj.Data);
                using (var data = new BinaryReader(obj.Data, Encoding.UTF8, true))
                {
                    result.Name = data.ReadString();
                    //result.ID = obj.Identifier;
                  
                    int ldatacount = data.ReadInt32();
                    result.LanguageData = new LocalizationDictionary();
                    for (int i = 0; i < ldatacount; i++)
                    {
                        result.LanguageData.Add(new LocalizationKeyValuePair(data.ReadString(), data.ReadString()));
                    } 
   
                }

                LanguagePackCashe.Add(result);
                return new Tuple<ILanguagePack, ListResourse>(result, new ListResourse { obj });
     
        }
        /// <summary>
        /// Преобразование ресурса в объект типа эра
        /// </summary>
        /// <param name="obj">Преобразуемый ресурс</param>
        /// <param name="lr">список всех ресурсов</param>
        public static Tuple<IAge, ListResourse> ResourceToAge (ResourceItem obj, ListResourse lr) {
    
                obj.Data.Position = 0;

                IAge result = new Age();

                // SerializableInventoryItem s = new SerializableInventoryItem(obj.Data);
                using (var data = new BinaryReader(obj.Data, Encoding.UTF8, true))
                {
                    result.Name = data.ReadString();
                    result.ID = obj.Identifier;
                    result.TranslationIdentifier = data.ReadString();

                    { //Обработка уровней
                        int levelcount = data.ReadInt32();
                        result.Levels = new DataList<ILevel>();
                        for (int j = 0; j < levelcount; j++)
                        {
                            var levelid = data.ReadUInt64();
                            ILevel levelage = LevelCashe.Find((item) => item.ID == levelid);
                            if (levelage != null)
                            {
                                result.Levels.Add(levelage);
                            }
                            else
                            {
                                result.Levels.Add(new Level { ID = levelid });
                                if (!levelagecashe.ContainsKey(levelid))
                                    levelagecashe[levelid] = new List<IAge>();
                                levelagecashe[levelid].Add(result);
                            }
                        }



                    }

                    { //Обработка родителя эры 1 
                        var Isparent = data.ReadBoolean();
                        if (Isparent)
                        {
                            var parentid = data.ReadUInt64();
                            IAge AgeAge = AgeCashe.Find((item) => item.ID == parentid);
                            if (AgeAge != null)
                            {
                                result.Parent = AgeAge;
                            }
                            else
                            {
                                result.Parent = new Age { ID = parentid };
                                if (!ageagecashe.ContainsKey(parentid))
                                    ageagecashe[parentid] = new List<IAge>();
                                ageagecashe[parentid].Add(result);
                            }
                        } 
                    }
                    result.Price = new Money(data.ReadUInt32(), data.ReadUInt32());

                    { //Обработка родителя эры 2
                        if (ageagecashe.ContainsKey((ulong)result.ID))
                        {
                            for (int j = 0; j < levellevelcashe[(ulong)result.ID].Count; j++)
                            {
                                ageagecashe[(ulong)result.ID][j].Parent = result;
                            }
                        }
                    }
                   
                }
                AgeCashe.Add(result);
                return new Tuple<IAge, ListResourse>(result, new ListResourse { obj });
          

        }
        /// <summary>
        /// Преобразование ресурса в объект типа элемент инвентаря
        /// </summary>
        /// <param name="obj">Преобразуемый ресурс</param>
        /// <param name="lr">список всех ресурсов</param>
        public static Tuple<IInventoryItem, ListResourse> ResourceToInventoryItem(ResourceItem obj, ListResourse lr)
        {
            obj.Data.Position = 0;
            IInventoryItem result = new InventoryItem();

            // SerializableInventoryItem s = new SerializableInventoryItem(obj.Data);
            using (var data = new BinaryReader(obj.Data))
            {
                result.Name = data.ReadString();
                result.ID = obj.Identifier;
                result.TranslationIdentifier = data.ReadString();
                result.ImproveResponseTime = new Enhancements ();
                result.ImproveResponseTime.TypeEnhancement = (TypeEnhancements)data.ReadUInt64();
                result.ImproveResponseTime.Value = data.ReadDouble();
                result.ImprovingHealth = new Enhancements { TypeEnhancement = (TypeEnhancements)data.ReadUInt64(), Value = data.ReadDouble() };

            }

            InventoryItemCashe.Add(result);
            return new Tuple<IInventoryItem, ListResourse>(result, new ListResourse { obj });

        }
        /// <summary>
        /// Преобразует ILevel в масив ресуров
        /// </summary>
        /// <param name="level">Объект уровня</param>
        /// <returns>Массив русурсов</returns>
        public static ListResourse LevelToResource (ILevel level) {

            ResourceItem resourse = null;
            using (MemoryStream ms = new MemoryStream())
            {
                using (var data = new BinaryWriter(ms))
                {
                    data.Write(level.Name); //Поле Name
                    data.Write(level.TranslationIdentifier); //Идентификатор перевода
                    var isboss = level.Boss != null;
                    data.Write(isboss);
                    if(isboss)
                    data.Write(level.Boss.ID);
                    data.Write(level.QuestionsLevel.Count); 
                    for(int i = 0; i < level.QuestionsLevel.Count; i++ )
                    {
                        data.Write(level.QuestionsLevel[i].ID);
                    }
                    data.Write(level.Price.Gold);
                    data.Write(level.Price.Brains);
                    data.Write(level.Remuneration.Gold);
                    data.Write(level.Remuneration.Brains);
                    data.Write(level.Parents.Count);
                    for (int i = 0; i < level.Parents.Count; i++)
                    {
                        data.Write(level.Parents[i].ID);
                    }

                }
                resourse = CreateItem(level.ID, StringNameLevelData + level.Name, FileTypes.Level, ms);
            }

            return new ListResourse { resourse };





        }
        /// <summary>
        /// Преобразует IBoss в масив ресуров
        /// </summary>
        /// <param name="boss">Объект босса</param>
        /// <returns>Массив русурсов</returns>
        public static ListResourse BossToResource (IBoss boss) {
            ResourceItem resourse = null;
            using (MemoryStream ms = new MemoryStream())
            {
                using (var data = new BinaryWriter(ms))
                {
                    data.Write(boss.Name); //Поле Name
                    data.Write(boss.TranslationIdentifier); //Идентификатор перевода
                   
                    data.Write(boss.Health);
                    data.Write(boss.Damage);
 
                }
                resourse = CreateItem(boss.ID, StringNameBossData + boss.Name, FileTypes.Boss, ms);
            }

            return new ListResourse { resourse };
        }
        /// <summary>
        /// Преобразует IQuestion в масив ресуров
        /// </summary>
        /// <param name="questionbase">Объект вопроса</param>
        /// <returns>Массив русурсов</returns>
        public static ListResourse QuestionToResource(IQuestion questionbase) {
            ResourceItem resourse = null;
            using (MemoryStream ms = new MemoryStream())
            {
                using (var data = new BinaryWriter(ms))
                {
                    data.Write(questionbase.Name); //Поле Name
                    data.Write(questionbase.TranslationIdentifier); //Идентификатор перевода
                    data.Write((ulong)questionbase.TypeQuestion);
                    switch (questionbase.TypeQuestion)
                    {
                        case TypeQuestionEnum.SelectOne:
                            QuestionSelectOne questionSelectOne = (QuestionSelectOne)questionbase;
                            data.Write(questionSelectOne.NumberAnswer);
                            data.Write(questionSelectOne.RightAnswer);
                            break;
                    }

                }
            resourse = CreateItem(questionbase.ID, StringNameQuestionData + questionbase.Name, FileTypes.Question, ms);
        }

            return new ListResourse { resourse };


        }
        /// <summary>
        /// Преобразует ILanguagePack в масив ресуров
        /// </summary>
        /// <param name="languagePack">Объект языкового пакета</param>
        /// <returns>Массив русурсов</returns>
        public static ListResourse LanguagePackToResource (ILanguagePack languagePack) {

            ResourceItem resourse = null;
            using (MemoryStream ms = new MemoryStream())
            {
                using (var data = new BinaryWriter(ms))
                {
                    data.Write(languagePack.Name); //Поле Name
   
                  
                    data.Write(languagePack.LanguageData.Count);
                    languagePack.LanguageData.ForEach((item) => { data.Write(item.Key ?? string.Empty); data.Write(item.Value ?? string.Empty); });

                }
                resourse = CreateItem(languagePack.ID, StringNameLanguagePackData + languagePack.Name, FileTypes.Language, ms);
            }

            return new ListResourse { resourse };

        
        }
        /// <summary>
        /// Преобразует IAge в масив ресуров
        /// </summary>
        /// <param name="languagePack">Объект эры</param>
        /// <returns>Массив русурсов</returns>
        public static ListResourse AgeToResource (IAge age) {

            ResourceItem resourse = null;
            using (MemoryStream ms = new MemoryStream())
            {
                using (var data = new BinaryWriter(ms))
                {
                    data.Write(age.Name); //Поле Name
                    data.Write(age.TranslationIdentifier); //Идентификатор перевода

                    data.Write(age.Levels.Count);
                    age.Levels.ForEach((item) => data.Write(item.ID));
                    var isparent = age.Parent != null;
                    data.Write(isparent);
                    if(isparent)
                        data.Write(age.Parent.ID);
                    data.Write(age.Price.Gold);
                    data.Write(age.Price.Brains);

                }
                resourse = CreateItem(age.ID, StringNameAgeData + age.Name, FileTypes.Age, ms);
            }

            return new ListResourse { resourse };

        
        }
        /// <summary>
        /// Преобразует IInventoryItem в масив ресуров
        /// </summary>
        /// <param name="languagePack">Объект элемента инвентаря</param>
        /// <returns>Массив русурсов</returns>
        public static ListResourse InventoryItemToResource(IInventoryItem inventoryItem)
        {

          

            ResourceItem resourse = null;
            using (MemoryStream ms  = new MemoryStream())
            {
                using (var data = new BinaryWriter(ms))
                {
                    data.Write(inventoryItem.Name); //Поле Name
                    data.Write(inventoryItem.TranslationIdentifier); //Идентификатор перевода

                    data.Write((ulong)inventoryItem.ImproveResponseTime.TypeEnhancement); //Поле типа улучшения для времени
                    data.Write(inventoryItem.ImproveResponseTime.Value); //Поле значения улучшения для времени
                    data.Write((ulong)inventoryItem.ImprovingHealth.TypeEnhancement); //Поле типа улучшения для здоровья
                    data.Write(inventoryItem.ImprovingHealth.Value); //Поле значения улучшения для здоровья
                }
                resourse = CreateItem(inventoryItem.ID, StringNameInventoryItemData + inventoryItem.Name, FileTypes.InventoryItem, ms);
            }
     
            return new ListResourse { resourse };
        }
       
        private static ResourceItem CreateItem(ulong ID, string name, FileTypes type, MemoryStream ms)
        {
            if (ID == 0)
                throw new Exception("ID = 0");

          

            ResourceItem item = new ResourceItem();
            item.FileType = type;
            item.Identifier = ID;
            item.Name = "Binary." + name;
            item.Version = 2;
            item.Data = new MemoryStream(ms.ToArray());
            return item;

        }
    }
}
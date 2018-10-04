using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Assets.Core.Game.Data.Age;
using Assets.Core.Game.Data.Boss;
using Assets.Core.Game.Data.Cultures;
using Assets.Core.Game.Data.Inventor;
using Assets.Core.Game.Data.Level;
using Assets.Core.Game.Data.Question;
using Assets.Core.Game.Data.User;
using Assets.Core.Levels;

using Assets.Core.ToePac;
using Assets.Core.User;
using UnityEngine;

namespace Assets.Core.Game.Data {
    public class GameData

    {
        public static GameData Default { get; }

        static GameData () {
            ListResourse lr = new ListResourse ();
            var files = Directory.GetFiles (Path.Combine (Application.dataPath, "GameData"), "*.ToePackage");
            for (int i = 0; i < files.Length; i++) {
                var tp = new ToePackage (new FileStream (files[i], FileMode.Open));
                lr.AddRange (tp.Items);
            }
            Default = new GameData (lr);
        }
<<<<<<< HEAD
        public List<ILanguagePack> LanguagePacks { get; } = new List<ILanguagePack>();
        public List<ILevel> Levels { get; } = new List<ILevel>();
        public List<IBoss> Bosses { get; } = new List<IBoss>();
        public List<IQuestion> Questions { get; } = new List<IQuestion>();
        public List<IAge> Ages { get; } = new List<IAge>();
        public List<IInventoryItem> IInventoryItems { get; } = new List<IInventoryItem>();

        public IUser User { get; set; } = new User.User();

        public GameData(ListResourse items)
        {
            for (int i = 0; i < items.Count; i++)
            {
                items[i].Data.Position = 0;
=======
        public List<ILanguagePack> LanguagePacks { get; } = new List<ILanguagePack> ();
        public List<ILevel> Levels { get; } = new List<ILevel> ();
        public List<IBoss> Bosses { get; } = new List<IBoss> ();
        public List<IQuestion> Questions { get; } = new List<IQuestion> ();
        public List<IAge> Ages { get; } = new List<IAge> ();
>>>>>>> d25cca021d585087fab35f09ff428ee989fd02fb

        public IUser User { get; set; } = new User.User ();

        public GameData (ListResourse items) {
            for (int i = 0; i < items.Count; i++) {
                items[i].Data.Position = 0;

                switch (items[i].FileType) {
                    case FileTypes.Level:

                        Levels.Add (ResourceConverter.ResourceToLevel (items[i], items).Item1);

                        break;
                    case FileTypes.Boss:
                        Bosses.Add (ResourceConverter.ResourceToBoss (items[i], items).Item1);

                        break;
                    case FileTypes.Question:

                        Questions.Add (ResourceConverter.ResourceToQuestion (items[i], items).Item1);

                        break;
                    case FileTypes.Language:
                        LanguagePacks.Add (ResourceConverter.ResourceToLanguagePack (items[i], items).Item1);
                        break;
                    case FileTypes.Age:
                        Ages.Add (ResourceConverter.ResourceToAge (items[i], items).Item1);

                        break;
                    case FileTypes.InventoryItem:
                        IInventoryItems.Add(ResourceConverter.ResourceToInventoryItem(items[i], items).Item1);

                        break;
                }

            }

        }

    }
}
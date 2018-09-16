using System;
using System.Collections.Generic;
using Assets.Core.Game.Ages_and_Graphs;
using UnityEngine;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Core.Game.Sorting
{
    static class Sorting
    {
        public static List<GameObject> levels;

        public static List<Vector2> groups;

        public static void SortLevels(List<GameObject> _levels)
        {
            levels = _levels;
            CreateWeb();
        }

        private static void CreateWeb()
        {
            int group = 0;
            for (int i = 0; i <levels.Count; i++)
            {
                LevelObj levelObj = levels[i].GetComponent<LevelObj>();
                if (levelObj.level.Parents.Count == 0)
                {
                    groups[i] = new Vector2(group, levelObj.level.ID);
                }
                if (levelObj.level.Parents.Count == 1)
                {
                    for (int j = 0; j < groups.Count; j++)
                    {
                        
                    }
              //      group = (ulong) groups[levelObj.level.Parents[0].ID].x + 1;
                    groups[i] = new Vector2(group,levelObj.level.ID);
                }
            }
            for (int i = 0; i<levels.Count; i++)
            {
                LevelObj levelObj = levels[i].GetComponent<LevelObj>();               
                if(levelObj.level.Parents.Count > 1)
                for (int j = 0; j < levelObj.level.Parents.Count; j++)
                    {
                        int k = 0;
                        while ((ulong) groups[k].y != levelObj.level.Parents[j].ID)
                        {
                            k++;
                        }
                        
                    }
                groups[i] = new Vector2(group, levelObj.level.ID);
            }

        }
    }
}

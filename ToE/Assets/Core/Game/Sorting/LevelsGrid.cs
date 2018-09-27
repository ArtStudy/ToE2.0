using System;
using System.Collections.Generic;
using Assets.Core.Game.Data.Level;
using UnityEngine;

namespace Assets.Core.Game.Sorting {
    public class LevelsGrid {

        public float scale, r;

        public Vector3 pos;

        public Dictionary<int, ILevel> levels = new Dictionary<int, ILevel> ();
        public Dictionary<int, Vector3> levelsPositions = new Dictionary<int, Vector3> ();

        private Vector3[] bigOffset = {
            new Vector3 (1, 0, 3),
            new Vector3 (2, 0, 0),
            new Vector3 (1, 0, -3),
            new Vector3 (-1, 0, -3),
            new Vector3 (-2, 0, 0),
            new Vector3 (-1, 0, 3)
        };

        private Vector3[] smallOffset = {
            new Vector3 (0, 0, 2),
            new Vector3 (1, 0, 1),
            new Vector3 (1, 0, -1),
            new Vector3 (0, 0, -2),
            new Vector3 (-1, 0, -1),
            new Vector3 (-1, 0, 1),

        };

        public void AddLevel (ILevel level) {
            if (!levels.ContainsValue (level)) {
                levels.Add (levels.Count, level);
                levelsPositions.Add (levelsPositions.Count, FindRoughPosOfLevel (level));
            }
        }

        public void RemoveLevel () {

        }

        public Vector3 FindRoughPosOfLevel (ILevel level) {
            int x = 0, y = 0, z = 0;
            int counter = 0;
            float levelX = 0, levelY = 0, levelZ = 0;
            for (int i = 0; i < levels.Count; i++)
                for (int j = 0; j < level.Parents.Count; j++)
                    if (levels[i] == level.Parents[j]) {
                        levelX += levelsPositions[i].x;
                        levelZ += levelsPositions[i].z;
                        counter++;
                        if (levelsPositions[i].y >= levelY)
                            levelY = levelsPositions[i].y + 1;
                    }
            if (counter != 0) {
                x = (int) (levelX / counter);
                z = (int) (levelZ / counter);
            }
            y = (int) levelY;
            return RandomOffset (new Vector3 (x, y, z));
        }

        public Vector3 RandomOffset (Vector3 pos) {
            Vector3 newPos = pos + bigOffset[(int) UnityEngine.Random.Range (0, bigOffset.Length)];
            //UnityEngine.Debug.Log ("I choose new pos " + newPos);
            while (((levelsPositions.ContainsValue (newPos)) || levelsPositions.ContainsValue (newPos + new Vector3 ((int) 0, (int) - 2, (int) 0)))) {
                newPos = pos + bigOffset[(int) UnityEngine.Random.Range (0, bigOffset.Length)];
                //UnityEngine.Debug.Log ("Oh,No! I must change position... The new pos is " + newPos);
            }
            return newPos;
        }
        public void FindPosition (ILevel level) { }

    }
}
using System;
using System.Collections.Generic;
using Assets.Core.Game.Data.Level;
using UnityEngine;

namespace Assets.Core.Game.Sorting {
    public static class Field {

        public static Dictionary<Vector3, GameObject> field;

        public static float CircleRadius;

        public static float scale;

        public static float y = 0;
        public static void DrawLevels (Dictionary<int, GameObject> levels) {
            field = new Dictionary<Vector3, GameObject> ();
            scale = levels[0].transform.localScale.x;
            CircleRadius = ((int) Math.Sqrt (levels.Count) * scale) / 2;
            UnityEngine.Debug.Log (CircleRadius);
            Vector3 pos;
            int j = 0;
            for (float x = -CircleRadius; x < CircleRadius; x += scale)
                for (float z = -CircleRadius; z < CircleRadius; z += scale) {
                    if (x * x + z * z <= CircleRadius * CircleRadius) {
                        pos = new Vector3 (x * scale, y, z * scale);
                        field[pos] = levels[j];
                        levels[j].transform.position = pos;
                        j++;
                    }
                }
            for (int i = j; i < levels.Count; i++) {
                if (!levels.ContainsValue (levels[i])) {
                    pos = DrawRandom (levels, i);
                    field[pos] = levels[i];
                    levels[i].transform.position = pos;
                }
            }
        }
        public static Vector3 DrawRandom (Dictionary<int, GameObject> levels, int number) {
            float x = UnityEngine.Random.Range (CircleRadius, (CircleRadius + 1) * 2);
            float z = UnityEngine.Random.Range (CircleRadius, (CircleRadius + 1) * 2);
            if (UnityEngine.Random.Range (1, 100) % 2 == 0)
                x = -x;
            if (UnityEngine.Random.Range (1, 100) % 2 == 0)
                z = -z;
            Vector3 posTemp = new Vector3 (x * scale, y, z * scale);
            if (x * x + z * z > ((CircleRadius + 1) * 2) * ((CircleRadius + 1) * 2) || field.ContainsKey (posTemp) || field.ContainsKey (new Vector3 (posTemp.x - scale, y, posTemp.z)) || field.ContainsKey (new Vector3 (posTemp.x + scale, y, posTemp.z)) || field.ContainsKey (new Vector3 (posTemp.x - scale, y, posTemp.z - scale)) || field.ContainsKey (new Vector3 (posTemp.x, y, posTemp.z + scale))) {
                DrawRandom (levels, number);

            }
            return posTemp;
        }

        public static bool FindNeighbours (Vector3 pos, Dictionary<int, GameObject> levels) {
            bool b = false;
            for (int i = 0; i < levels.Count; i++) {

            }
            return b;
        }

    }
}
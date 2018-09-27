using System.Collections;
using System.Collections.Generic;
using Assets.Core.Game.Ages_and_Graphs;
using Assets.Core.Game.Sorting;
using UnityEngine;

public static class LevelsDrawer {

	public static float levelScale;

	public static LevelsGrid grid;
	public static void DrawLevel (GameObject level) {
		LevelObj levelObj = level.GetComponent<LevelObj> ();
		for (int i = 0; i < grid.levels.Count; i++) {
			if (grid.levels[i] == levelObj.level) {
				level.transform.position = grid.levelsPositions[i] * levelScale;
			}
		}

	}
}
using System;
using Assets.Core.Game.Ages_and_Graphs;
using Assets.Core.Game.Sorting;
using Assets.Core.LevelsStructureInterfaces;
using UnityEngine;

public static class WebDrawer {
    public static int kSize = 5;

    public static Transform ageTransform;

    //Установка размера сферы в зависимости от количества уровней
    public static void ResizeAge (AgeObj age) {
        ageTransform = age.GetComponent<Transform> ();
        //        Vector3 size = new Vector3((float)( age.LevelPrefab.transform.localScale.x*Math.Sqrt(age.Age.web.LevelsCount/2)), (float)( age.LevelPrefab.transform.localScale.y*Math.Sqrt(age.Age.web.LevelsCount/2)), (float)( age.LevelPrefab.transform.localScale.z*Math.Sqrt(age.Age.web.LevelsCount/2)));
        Vector3 size = new Vector3 ((float) (age.LevelPrefab.transform.lossyScale.x * Math.Sqrt (age.Age.Web.LevelsCount * kSize)), (float) (age.LevelPrefab.transform.lossyScale.y * Math.Sqrt (age.Age.Web.LevelsCount * kSize)), (float) (age.LevelPrefab.transform.lossyScale.z * Math.Sqrt (age.Age.Web.LevelsCount * kSize)));
        ageTransform.localScale = size;
    }

    // Отрисовка уровня
    public static void DrawLevel (GameObject level, AgeObj age) {
        LevelObj levelObj = level.GetComponent<LevelObj> ();
        Vector2 levelPos = age.Age.Web.FindLevelPos (levelObj.level);
        UnityEngine.Debug.Log (levelPos.y);
        double x, y, z;
        float r, h, step, delta;
        delta = level.transform.localScale.x / 2;
        r = ageTransform.localScale.x / 2;
        h = r * 2 / age.Age.Web.groups.Count;
        x = UnityEngine.Random.Range (r - levelPos.x * h, r - (levelPos.x + 1) * h);
        step = (float) Math.Sqrt (r * r - x * x) / age.Age.Web.groups[(int) levelPos.x].Count;
        y = UnityEngine.Random.Range (-step, step);
        z = Math.Sqrt (r * r - x * x - y * y);
        if (UnityEngine.Random.Range (0, 100) % 2 == 0)
            z = -z;
        level.transform.position = new Vector3 ((float) x, (float) y, (float) z);
    }

}
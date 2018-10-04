using System;
using Assets.Core.Game.Ages_and_Graphs;
using Assets.Core.Game.Sorting;
using Assets.Core.LevelsStructureInterfaces;
using UnityEngine;

public static class WebDrawer {
    public static int kSize = 8;

    public static Transform ageTransform;

    //Установка размера сферы в зависимости от количества уровней
    /*
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
          double x, y, z, angle, deltaAngle;
          float r, h, deltaPos;
          r = ageTransform.localScale.x / 2 + level.transform.lossyScale.x;
          h = r * 2 / age.Age.Web.groups.Count;
          deltaPos = level.transform.localScale.x / 2;
          y = UnityEngine.Random.Range (r - levelPos.x * h, r - (levelPos.x + 1) * h);
          deltaAngle = 2 * Math.Asin (Math.PI * r / deltaPos);
          angle = Math.PI * 2 * UnityEngine.Random.Range ((float) (levelPos.y - 0.5), (float) (levelPos.y + 0.5)) / age.Age.Web.groups[(int) levelPos.x].Count;
          x = Math.Sqrt (r * r - y * y) * Math.Cos (angle);
          z = Math.Sqrt (r * r - x * x - y * y) * Math.Sign (Math.Cos (angle));
          UnityEngine.Debug.Log (" r = " + r);
          level.transform.position = new Vector3 ((float) x, (float) y, (float) z);
      }*/
}
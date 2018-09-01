using Assets.Core.Game.Ages_and_Graphs;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortLevels {

    public float levelSpeed = 10.0f; 


    public SortLevels()
    {

    }

    public List<LevelUI> StartSorting(List<LevelUI> temp)
    {

        return temp;
    }

    public Vector3 randomFirstLevelPos(float r)
    {
        double x, y, z;
        x = UnityEngine.Random.Range(-r, r);
        y = UnityEngine.Random.Range(-(float)Math.Sqrt(Math.Pow(r, 2) - Math.Pow(x, 2)), (float)Math.Sqrt(Math.Pow(r, 2) - Math.Pow(x, 2)));
        z = Math.Sqrt(Math.Pow(r, 2) - Math.Pow(x, 2) - Math.Pow(y, 2));
        if (UnityEngine.Random.Range(0, 100) % 2 == 0)
            z = -z;
        Vector3 pos = new Vector3((float)x, (float)y, (float)z);
        return  pos;
    }
    
    public void Sorting()
    {

    }
}

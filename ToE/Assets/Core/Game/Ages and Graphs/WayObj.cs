using System.Collections;
using System.Collections.Generic;
using Assets.Core.Game.Ages_and_Graphs;
using Assets.Core.Levels;
using Assets.Core.LevelsStructureInterfaces;
using UnityEngine;

public class WayObj : MonoBehaviour {

    public Vector3 parentPos;
    public Vector3 childPos;
    void Update () {
        WayDrawer.DrawWay (this);
    }

}
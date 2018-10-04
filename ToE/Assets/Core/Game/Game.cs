using System.Collections;
using System.Collections.Generic;
using System.IO;
using Assets.Core.Game.Animations;
using Assets.Core.Game.Data;
using Assets.Core.Game.Sorting;
using Assets.Core.Levels;
using Assets.Core.ToePac;
using UnityEngine;

public class Game : MonoBehaviour {

    public GameObject AgePrefab;

    public GameObject camera;
    void Awake () {
        //LoadGUI ();
    }
    void Start () {
        LoadAges ();
    }

    void Update () { }

    void LoadAges () {
        var ages = GameData.Default.Ages;
        for (int i = 0; i < ages.Count - 1; i++) { //Только 1 эра спавнится
            GameObject age = Instantiate (AgePrefab);
            age.name = ages[i].Name;

        }
    }
    void LoadGUI () {
        //camera.AddComponent ();
    }
}
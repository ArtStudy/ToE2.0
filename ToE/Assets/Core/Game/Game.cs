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

    public List<GameObject> Ages;

    public GameObject camera;
    void Awake () {
        //LoadGUI ();
    }
    void Start () {
        LoadAges ();
    }

    void Update () {
        //for (int i = 0; i < Ages.Count; i++) 
        //MenuAnimations.Rotation(Ages[i]);
    }

    void LoadAges () {
        for (int i = 0; i < Ages.Count; i++) {
            Ages[i] = Instantiate (Ages[i]);
        }
    }
    void LoadGUI () {
        //camera = Instantiate (camera);
    }
}
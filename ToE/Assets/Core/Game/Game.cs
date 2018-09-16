using Assets.Core.Game.Animations;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Assets.Core.ToePac;
using Assets.Core.Game.Data;
using Assets.Core.Levels;
using Assets.Core.Game.Sorting;

public class Game : MonoBehaviour
{


    public List<GameObject> Ages;


    // Use this for initialization
    void Start ()
    { 
        LoadAges();
    }

    // Update is called once per frame
    void Update ()
    {
        for (int i = 0; i < Ages.Count; i++) 
            MenuAnimations.Rotation(Ages[i]);
    }

    void LoadAges()
    {
        for (int i = 0; i < Ages.Count; i++)
        {
            Ages[i] = Instantiate(Ages[i]);
        }
    }
}

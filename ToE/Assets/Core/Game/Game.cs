using Assets.Core.Game.Animations;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Assets.Core.ToePac;
using Assets.Core.Game.Data;

public class Game : MonoBehaviour
{
    static  FileStream GD1;
    public static PAC GD1PAC;
    public List<GameObject> Ages;
    public static GameData gameData;


    // Use this for initialization
    void Start ()
    {
        GD1 = new FileStream(Application.dataPath + "\\GameData\\GD1.ToePackage", FileMode.Open);
        GD1PAC = new PAC(GD1);
        GD1PAC.Items.ForEach((item) => Debug.Log(item.Name.ToString()));
        gameData = new GameData(GD1PAC);
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

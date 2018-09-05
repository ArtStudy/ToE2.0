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

        Dictionary<int, List<Level>> LevelGrid = new Dictionary<int, List<Level>>();
       // LevelGrid[0] = new List<Level>();
        var templevel = gameData.Levels.ConvertAll((item) => item.Value);
        int k = 0;
        int k1 = 0;
        int CurrentLatitude = 0;
        List<ILevel> AddLevels = new List<ILevel>();

        CurrentLatitude = 0;
        LevelGrid[CurrentLatitude] = new List<Level>();
        List<ILevel> AddLevels2 = new List<ILevel>();
        for (int i = 0; i< templevel.Count; i++)
        {
           
            int contemp = 0;
            for (int j = 0; j < templevel[i].Parents.Count; j++)
            {
                k1++;
                if (AddLevels.Contains(templevel[i].Parents[j]))
                {
                    contemp++;
                }
                else
                    break;
                
            }
            if(contemp == templevel[i].Parents.Count)
            {
                LevelGrid[CurrentLatitude].Add(templevel[i]);
                AddLevels2.Add(templevel[i]);
                templevel.Remove(templevel[i]);
              
                i--;
            }
            if (templevel.Count == 0)
            {
                break;
            }
            else if (templevel.Count == i +1)
            {
                AddLevels.AddRange(AddLevels2);
                AddLevels2.Clear();
                 i = -1;
                CurrentLatitude++;
                LevelGrid[CurrentLatitude] = new List<Level>();
            }
   
            k++;
        }
        Debug.Log(k);
        Debug.Log(k1);
   
        Debug.Log("");
        templevel = gameData.Levels.ConvertAll((item) => item.Value);
        k = 0;
        Web web = new Web();
        for (int i = 0; i < templevel.Count; i++)
        {
            web.AddLevel(templevel[i]);
            k++;
        }
        Debug.Log(k);
        Debug.Log(web.k);
        Debug.Log(web.k1);
        Debug.Log(web.k2);
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

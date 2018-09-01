using Assets.Core.Levels;
using Assets.Core.Levels.Questions;
using Assets.Core.Volutes;

using UnityEngine;
using UnityEngine.UI;

namespace Assets.Core.Game.Ages_and_Graphs
{
    public class LevelObj : MonoBehaviour
    {
        public ILevel level;

   
        public Text text;
        void Start()
        {
        
            text = Instantiate(this.text);
            text.transform.parent = this.transform;
            text.transform.position = new Vector3(10, 10);
        }

        void Update()
        {
            
        }

        
    }
}

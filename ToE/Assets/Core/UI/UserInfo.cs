using Assets.Core.BindingData;
using Assets.Core.BindingData.Converters;
using Assets.Core.Game.Data;
using Assets.Core.Game.Data.Level;
using Assets.Core.Game.Data.User;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInfo : MonoBehaviour {
    public Text Name;
    public InputField InputText;
    public Button OkButton;
    public string NewCorrectName { get; set; }
    // Use this for initialization
    void Start ()
    {
        
        Binding.Register(InputText, DependencyProperty.textonInputFieldtProperty, this, "NewCorrectName");
        Binding.Register(this.gameObject, DependencyProperty.activedonGameObjectProperty, GameData.Default.User, "CorrectName.Length", new IntToNotBoolConverter());


        if (GameData.Default.User.CorrectName.Length == 0)
        {
    
            OkButton.onClick.AddListener(() => {

                GameData.Default.User.CorrectName = NewCorrectName;

            });

           // Binding.Register(Name, DependencyProperty.textonTextProperty, us, "CorrectName");
        }
    }
	
	// Update is called once per frame
	void Update () {
    //    Name.text = InputText.text;


    }
}

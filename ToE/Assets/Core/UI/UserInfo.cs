using Assets.Core.BindingData;
using Assets.Core.Game.Data.Level;
using Assets.Core.Game.Data.User;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInfo : MonoBehaviour {
    public Text Name;
    public InputField InputText;

    // Use this for initialization
    void Start () {
        User us = new User();


        Binding.Register(InputText, DependencyProperty.textonInputFieldtProperty, us, "CorrectName");
        Binding.Register(Name, DependencyProperty.textonTextProperty, us, "CorrectName");

    }
	
	// Update is called once per frame
	void Update () {
    //    Name.text = InputText.text;


    }
}

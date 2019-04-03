using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MainButton : MonoBehaviour {

    public void StartButton()
    {
        GameProduction.gameProduction.GameStart();

        gameObject.SetActive(false);
    }



    public void ExitButton()
    {
        Application.Quit();
    }
    

}

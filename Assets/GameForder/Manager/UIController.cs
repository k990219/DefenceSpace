using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour {

    private UISprite gameOverMsg;

    private UISprite gameWinScreen;
    private UISprite gameWinMsg;

    private UISprite gameRankScreen;
    private UISprite gameQuitScreen;


    private UISprite helpGrayScreen;
    private GameObject helpTextToltip;

    private UISprite helpButton;

    private UIInput nameInput;

    string userName { get; set; }

    private UILabel rankName;
    private UILabel rankScore;
    private UILabel scoreNum;

    public GameObject CenterHUD;

    bool gameHelpScreen = false;

    private void Awake()
    {
        GameObject popUpWindows = GameObject.Find("UI Root/PopUPWindows");

        gameWinScreen = popUpWindows.transform.Find("GameWin/GameWinScreen").GetComponent<UISprite>();
        gameWinMsg = popUpWindows.transform.Find("GameWin/GameWinMsg").GetComponent<UISprite>();

        nameInput = gameWinScreen.transform.Find("InputField/Input").GetComponent<UIInput>();
        scoreNum = gameWinScreen.transform.Find("ScoreNum").GetComponent<UILabel>();

        gameOverMsg = popUpWindows.transform.Find("GameOver/GameOverMsg").GetComponent<UISprite>();

        gameRankScreen = popUpWindows.transform.Find("GameRankScreen").GetComponent<UISprite>();
        gameQuitScreen = popUpWindows.transform.Find("GameQuitScreen").GetComponent<UISprite>();

        rankName = gameRankScreen.transform.Find("RankValue/NameValue").GetComponent<UILabel>();
        rankScore = gameRankScreen.transform.Find("RankValue/ScoreValue").GetComponent<UILabel>();

        CenterHUD = GameObject.Find("UI Root/HUDWindows").transform.Find("Center").gameObject;
    }



    public void OverMsgOff()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        CenterHUD.SetActive(false);
        gameOverMsg.gameObject.SetActive(false);
    }

    public void WinMsgOff()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        CenterHUD.SetActive(false);
        gameWinMsg.gameObject.SetActive(false);
        gameWinScreen.gameObject.SetActive(true);
    }

    public void WinScreenOKButton()
    {
        CenterHUD.SetActive(false);
        userName = nameInput.label.text;
        GameManager.gameManager.InputUserData(userName, GameManager.gameManager.gameScore);
        RankTextSet();

        gameWinScreen.gameObject.SetActive(false);
        gameRankScreen.gameObject.SetActive(true);

        JsonManager.RankingSave();
    }



    public void RankTextSet()
    {
        foreach(var obj in GameManager.gameManager.playerRanking)
        {
            rankName.text += obj.name + '\n';

        }

        foreach (var obj in GameManager.gameManager.playerRanking)
        {
            rankScore.text += obj.score.ToString("") + '\n';

        }

    }

    public void QuitScreenCloseButton()
    {
        GameManager.isPlaying = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        gameQuitScreen.gameObject.SetActive(false);
        CenterHUD.SetActive(true);
    }


    public void GoToMainMenu()
    {
        GameManager.sceneName = "MainScene";
        SceneManager.LoadScene(1);
    }

    public void ShowHelpButton()
    {
        gameQuitScreen.gameObject.SetActive(false);
//        helpGrayScreen.gameObject.SetActive(true);
//        helpTextToltip.SetActive(true);
        CenterHUD.SetActive(true);
        gameHelpScreen = true;
        PlayerHud.playerHudScript.boostSlider.alpha = 1.0f;
    }

    public void GameWinUIControll(int gameScore)
    {
        scoreNum.text = gameScore.ToString();
        gameWinMsg.gameObject.SetActive(true);
    }

    public void GameOverUIControll()
    {
        gameOverMsg.gameObject.SetActive(true);
    }


    // Update is called once per frame
    void Update () {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            
            if (GameManager.isPlaying)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                gameQuitScreen.gameObject.SetActive(true);
                //                helpButton.gameObject.SetActive(true);
                GameManager.isPlaying = false;
                CenterHUD.SetActive(false);

            }
            else
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                gameQuitScreen.gameObject.SetActive(false);
                //                helpButton.gameObject.SetActive(false);
                GameManager.isPlaying = true;
                CenterHUD.SetActive(true);
            }
        }

        if (gameHelpScreen && Input.anyKey)
        {
            gameQuitScreen.gameObject.SetActive(!false);
            helpGrayScreen.gameObject.SetActive(!true);
            helpTextToltip.SetActive(!true);
            CenterHUD.SetActive(!true);
            gameHelpScreen = false;

        }

    }
}

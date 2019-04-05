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

        nameInput = gameWinScreen.transform.Find("Input").GetComponent<UIInput>();
        scoreNum = gameWinScreen.transform.Find("ScoreNum").GetComponent<UILabel>();

        gameOverMsg = popUpWindows.transform.Find("GameOver/GameOverMsg").GetComponent<UISprite>();

        gameRankScreen = popUpWindows.transform.Find("GameRankScreen").GetComponent<UISprite>();
        gameQuitScreen = popUpWindows.transform.Find("GameQuitScreen").GetComponent<UISprite>();

        rankName = gameRankScreen.transform.Find("RankValue/RankName").GetComponent<UILabel>();
        rankScore = gameRankScreen.transform.Find("RankValue/RankScore").GetComponent<UILabel>();

        CenterHUD = GameObject.Find("UI Root/HUDWindows").transform.Find("Center").gameObject;
    }



    public void OverMsgOff()
    {

        CenterHUD.SetActive(false);
        gameOverMsg.gameObject.SetActive(false);
    }

    public void WinMsgOff()
    {
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
        rankName.text = "Name\n";
        foreach(var obj in GameManager.playerRanking)
        {
            rankName.text += obj.name + '\n';

        }
        rankScore.text = "Score\n";
        foreach (var obj in GameManager.playerRanking)
        {
            rankScore.text += obj.score.ToString("") + '\n';

        }

    }

    public void QuitScreenCloseButton()
    {
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        gameQuitScreen.gameObject.SetActive(false);
        CenterHUD.SetActive(true);
    }


    public void GoToMainMenu()
    {
        Time.timeScale = 1;
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
            if (!gameQuitScreen.gameObject.activeInHierarchy)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                gameQuitScreen.gameObject.SetActive(true);
//                helpButton.gameObject.SetActive(true);
                Time.timeScale = 0;
                CenterHUD.SetActive(false);

            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                gameQuitScreen.gameObject.SetActive(false);
//                helpButton.gameObject.SetActive(false);
                Time.timeScale = 1;
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

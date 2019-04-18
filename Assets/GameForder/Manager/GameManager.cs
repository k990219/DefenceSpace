using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private static GameManager instance;
    public static GameManager gameManager
    {
        get
        {
            if (!instance)
            {
                instance = FindObjectOfType(typeof(GameManager)) as GameManager;
            }
            return instance;
        }         
    }

    public static string sceneName;

    public static bool isPlaying = false;
    public bool mouse;

    public float gameStartTime;
    public float gameTimer;// = 180.0f;

    public List<RankData> playerRanking = new List<RankData>();

    public List<GameObject> deadRespwan = new List<GameObject>();
    GameObject []spwanTile;

    List<GameObject> initSpwan = new List<GameObject>();
    UIController uiControll;

    public float minMoveY = -4, maxMoveY = 7;
    Vector3 playerStartPosition = new Vector3(7.0f, 0.0f, 18.0f);


    string[] monsterName = {"Slug","Clocker","Insect","Arachnid","Juggernaut"};
    int[] monsterCnt = { 12, 8, 6, 6, 3 };
    int monsterCategory;

    int rand;
    public int gameScore { get; set; }

    private void Awake()
    {
        monsterCategory = monsterCnt.Length;
        uiControll = GetComponentInChildren<UIController>();
        spwanTile = GameObject.FindGameObjectsWithTag("Spwan");
    }

    void Start () {

        Initialized();

        Invoke("DeadMonsterSpwan", 5f);

        InvokeRepeating("MonsterSpwan", 10, 3f);
    }

    private void Update()
    {
        Timer();
        PlayerYCheck();
    }

    private void Initialized()
    { 
        foreach (RankData obj in playerRanking)
        {
            Debug.Log(obj.name + ": " + obj.score);
        }

        gameStartTime = 10.0f ;
        gameTimer = gameStartTime;
        gameScore = 0;
        Time.timeScale = 1;
        GameManager.isPlaying = true;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        for (int j = 0; j < monsterCategory; j++)
        {
            for (int i = 0; i < monsterCnt[j]; i++)
            {
                GameObject monster = ObjectPool.Instance.PopFromPool(monsterName[j]);
                initSpwan.Add(monster);
            }
        }

        while(initSpwan.Count!=0)
        {
            int posRand = Random.Range(0, 8);

            int spwanRand = Random.Range(0, initSpwan.Count);
            Vector3 randPos = new Vector3(Random.Range(-5f, 5f), 0, Random.Range(-5f, 5f));
            Vector3 spwanPosition = spwanTile[posRand].transform.position + randPos;

            initSpwan[spwanRand].transform.position = spwanPosition;
            initSpwan[spwanRand].SetActive(true);
            initSpwan.RemoveAt(spwanRand);
        }
        
    }

    void DeadMonsterSpwan()
    {
        if(!(deadRespwan.Count==0))
        {
            GameObject item = deadRespwan[0];
            deadRespwan.RemoveAt(0);
            int spwanRand = Random.Range(0, 8);
            Vector3 spwanPosition = spwanTile[spwanRand].transform.position;

            item.transform.position = spwanPosition;
            item.SetActive(true);       
            
        }
    }

    public void DeadMonsterPush(GameObject item)
    {
        item.SetActive(false);
        deadRespwan.Add(item);
    }

    void MonsterSpwan()
    {
        rand = Random.Range(0, monsterCategory);
        GameObject monster = ObjectPool.Instance.PopFromPool(monsterName[rand]);
        int spwanRand = Random.Range(0,8);
        Vector3 spwanPosition = spwanTile[spwanRand].transform.position;

        monster.transform.position = spwanPosition;
        monster.SetActive(true);  
    }

    void Timer()
    {
        gameTimer -= Time.deltaTime;

        if (gameTimer < 0f)
        {
            GameSuccess();
        }
    }

    public void GameSuccess()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        JsonManager.RankingLoad();

        Time.timeScale = 0;
        gameTimer = 0f;
        GameManager.isPlaying = false;

        uiControll.GameWinUIControll(gameScore);

    }

    public void GameOverMsg()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        GameManager.isPlaying = false;
        Time.timeScale = 0;
        uiControll.GameOverUIControll();
    }

    public void InputUserData(string name,int score)
    {     
        RankData data = new RankData(name, score);
        playerRanking.Add(data);
        playerRanking.Sort(delegate (RankData d1, RankData d2) { return d1.score.CompareTo(d2.score); });
        playerRanking.Reverse();

        if (playerRanking.Count >= 6)
        {
            while (playerRanking.Count > 5)
            {
                playerRanking.RemoveAt(playerRanking.Count - 1);
            }

        }

    }

    void PlayerYCheck()
    {
        if (!(PlayerMovement.playerMovement.transform.position.y < minMoveY))
            return;
        PlayerMovement.playerMovement.transform.position = playerStartPosition;
    }

    public void RemoveRank()
    {
        playerRanking.Clear();
    }

}


public class RankData
{
    public string name { get; set; }
    public int score { get; set; }

    public RankData(string name,int score) { this.name = name; this.score = score; }

}
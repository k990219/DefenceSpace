using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;

public class JsonManager : MonoBehaviour
{
    private static JsonManager instance;
    public static JsonManager gameManager
    {
        get
        {
            if (!instance)
            {
                instance = FindObjectOfType(typeof(JsonManager)) as JsonManager;
            }
            return instance;
        }
    }

    static string dirPath = "/Resources/RankingData.josn";

    public void RankingReset()
    {
        GameManager.gameManager.playerRanking.Clear();

        JsonData RankingJson = JsonMapper.ToJson(GameManager.gameManager.playerRanking);

        File.WriteAllText(Application.dataPath + dirPath, RankingJson.ToString());
    }

    public static void RankingSave()
    {
        JsonData RankingJson = JsonMapper.ToJson(GameManager.gameManager.playerRanking);

        File.WriteAllText(Application.dataPath + dirPath, RankingJson.ToString());
    }


    public static void RankingLoad()
    {
        if (!File.Exists(Application.dataPath + dirPath))
        {
            JsonData RankingJson = JsonMapper.ToJson(GameManager.gameManager.playerRanking);

            File.WriteAllText(Application.dataPath + dirPath, RankingJson.ToString());

        }

        else
        {
            string JsonString = File.ReadAllText(Application.dataPath + dirPath);

            JsonData rankingData = JsonMapper.ToObject(JsonString);

            for( int i = 0; i < rankingData.Count; i++)
            {
                string name = rankingData[i]["name"].ToString();
                int score = int.Parse(rankingData[i]["score"].ToString());
                GameManager.gameManager.playerRanking.Add(new RankData(name, score));
            }

        }
    }




}

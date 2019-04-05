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

    static string dirPath = "/GameForder/PlugIn/RankingData.josn";

    public static void RankingSave()
    {
        JsonData RankingJson = JsonMapper.ToJson(GameManager.playerRanking);

        File.WriteAllText(Application.dataPath + dirPath, RankingJson.ToString());
    }


    public static void RankingLoad()
    {
        if (!File.Exists(Application.dataPath + dirPath))
        {
            Debug.Log("NotFile");
            JsonData RankingJson = JsonMapper.ToJson(GameManager.playerRanking);

            File.WriteAllText(Application.dataPath + dirPath, RankingJson.ToString());

            foreach (RankData obj in GameManager.playerRanking)
            {
                obj.name = RankingJson["name"].ToString();
                Debug.Log(obj.name);
                obj.score = int.Parse(RankingJson["score"].ToString());
                Debug.Log(obj.score);
            }

        }

        else
        {
            string JsonString = File.ReadAllText(Application.dataPath + dirPath);

            Debug.Log(JsonString);
            JsonData rankingData = JsonMapper.ToObject(JsonString);

            for( int i = 0; i < rankingData.Count; i++)
            {
                string name = rankingData[i]["name"].ToString();
                int score = int.Parse(rankingData[i]["score"].ToString());
                GameManager.playerRanking.Add(new RankData(name, score));
                Debug.Log(name + ": " + score);
            }

        }
    }




}

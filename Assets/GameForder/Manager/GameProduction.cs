using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameProduction : MonoBehaviour
{

    private static GameProduction instance;
    public static GameProduction gameProduction
    {
        get
        {
            if (!instance)
                instance = FindObjectOfType(typeof(GameProduction)) as GameProduction;
            return instance;
        }
    }

    public GameObject meteoObj;
    Transform meteoTransform;
    Transform shipPosition;

    new Rigidbody rigidbody;

    public UISprite warningProduction;
    Color warningColor = new Color(1.0f, 0.0f, 0.0f, 0.3f);

    float[] setMeteoPosition = { 13.0f, 4.0f, 5.0f };
    public bool isStart = false;

    private void Awake()
    {
        rigidbody = meteoObj.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (!isStart)
            return;

        warningProduction.color = Color.Lerp(warningProduction.color, new Vector4(1, 0, 0, 0), 5.0f * Time.deltaTime);
        if (warningProduction.color.a < 0.05f)
        {
            warningProduction.color = warningColor;
        }
    }
    

    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(1.5f);

        SceneManager.LoadScene(1);
    }

    public void GameStart()
    {
        GameManager.sceneName = "GameScene";
        StartCoroutine("LoadScene");

        isStart = true;


        shipPosition = MainShip.mianShip.transform;
        meteoObj.transform.position =
            new Vector3(shipPosition.position.x + setMeteoPosition[0],
            shipPosition.position.y + setMeteoPosition[1],
            shipPosition.position.z + setMeteoPosition[2]);

       meteoObj.transform.LookAt(shipPosition);
       rigidbody.AddForce(meteoObj.transform.forward*800);


    }

}

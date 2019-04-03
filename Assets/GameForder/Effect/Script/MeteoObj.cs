using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoObj : MonoBehaviour {

    private IEnumerator OnCollisionEnter(Collision collision)
    {
        yield return new WaitForSeconds(0.5f);
//        GameManager.sceneName = "GameScene";
//        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour {

    public UISprite loadingBar;
    public UILabel loadTxt;

    // Use this for initialization


    void Start () {

        StartCoroutine("LoadScene");
    }

    IEnumerator LoadScene()
    {
        yield return null;

        AsyncOperation oper = SceneManager.LoadSceneAsync(GameManager.sceneName);

        oper.allowSceneActivation = false;
        loadingBar.fillAmount = 0f;
        float timer = 0f;

        while (!oper.isDone)
        {
            yield return null;

            timer += Time.deltaTime;
            loadTxt.text = ((int)(loadingBar.fillAmount * 100)).ToString() + "%";
//            loadingBar.fillAmount = oper.progress;

            if (oper.progress >= 0.9f)
            {
                loadingBar.fillAmount = Mathf.Lerp(loadingBar.fillAmount,1f,timer);

                if (loadingBar.fillAmount == 1.0f)
                    oper.allowSceneActivation = true;
            }
            else
            {

               loadingBar.fillAmount = Mathf.Lerp(loadingBar.fillAmount, oper.progress, timer);

                if (loadingBar.fillAmount >= oper.progress)
                {
                    timer = 0f;

                }
            }
        }
    }



}

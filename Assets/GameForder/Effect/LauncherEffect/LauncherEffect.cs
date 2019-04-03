using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LauncherEffect : MonoBehaviour {

    private void OnEnable()
    {
        StartCoroutine("PlayTime");
    }

    IEnumerator PlayTime()
    {
        GetComponent<ParticleSystem>().Play();

        yield return new WaitForSeconds(2.5f);

        ObjectPool.Instance.PushToPool(this.name, this.gameObject);
    }

}

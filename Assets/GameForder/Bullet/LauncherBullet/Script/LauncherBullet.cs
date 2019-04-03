using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LauncherBullet : MonoBehaviour {


    public string itemName = "LauncherBullet";
    public string effectName = "LauncherEffect";
    private float lifeTime = 3f;
    private float damage;

    public float bulletDamage { get{ return damage; } set{ damage = value; }}
    SphereCollider colliderHit;

    private void Awake()
    {
        colliderHit = transform.Find("HitRange").GetComponent<SphereCollider>();
    }
    // Use this for initialization
    private void OnEnable()
    {
        colliderHit.enabled = false;
        StartCoroutine("LifeTime");
    }

    private void OnDisable()
    {
        GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
    }

    IEnumerator LifeTime()
    {
        yield return new WaitForSeconds(lifeTime);
        ObjectPool.Instance.PushToPool(itemName, gameObject);

    }

    private IEnumerator OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag.Equals("Monster")|| other.gameObject.tag.Equals("Ground"))
        {
            colliderHit.enabled = true;
            GameObject effect = ObjectPool.Instance.PopFromPool(effectName);
            effect.transform.position = transform.position;
            effect.SetActive(true);

            yield return new WaitForSeconds(0.2f);
            ObjectPool.Instance.PushToPool(itemName, gameObject);
        }
    }
}

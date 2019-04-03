using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsectBullet : MonoBehaviour
{

    GameObject attacker;
    Transform AttackPosition ;
    public string itemName = "InsectBullet";
    public float lifeTime = 3f;
    float damage;
    float power = 1400;

    private void OnEnable()
    {
        if(attacker)
            StartCoroutine("ShootBullet");
    }


    IEnumerator ShootBullet()
    {
        damage = attacker.GetComponent<Insect>().monsterDamage;
        GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        GetComponent<Rigidbody>().AddForce(AttackPosition.forward * power);

        yield return new WaitForSeconds(lifeTime);
        ObjectPool.Instance.PushToPool(itemName, this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerManager>().GetDamage(damage);
            ObjectPool.Instance.PushToPool(itemName, this.gameObject);
        }
        else if (other.tag == "Ship")
        {
            other.GetComponentInParent<GameShip>().GetDamage(damage);
            ObjectPool.Instance.PushToPool(itemName, this.gameObject);
        }
    }

    public void SetAttackerPosition(Transform obj)
    {
        AttackPosition = obj;
    }

    public void SetAttacker(GameObject obj)
    {
        attacker = obj;
    }

}  

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HellfireEffect : MonoBehaviour {

    HellFire hellFire;
    public float fireRate;
    public float damage;
    public bool isDamage = false;

    private void Start()
    {
        hellFire = WeaponManager.weaponScript.GetComponentInChildren<HellFire>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Monster"))
        {
            isDamage = true;
            StartCoroutine("DmgDelay",other.GetComponent<Monster>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        isDamage = false;
    }

    IEnumerator DmgDelay(Monster obj)
    {
        while (isDamage)
        {
            hellFire.AttackHit(obj.gameObject);

            yield return new WaitForSeconds(fireRate);
        }
    }

}

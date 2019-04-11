using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HellfireEffect : MonoBehaviour {

    public float fireRate;
    public float damage;
    public bool isDamage = false;

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
            Debug.Log("Hit");
            obj.GetDamage(damage);
            PlayerHud.playerHudScript.AttackHit();
            yield return new WaitForSeconds(fireRate);
        }
    }

}

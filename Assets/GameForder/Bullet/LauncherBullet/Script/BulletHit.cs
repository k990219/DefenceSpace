using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHit : MonoBehaviour {

    float damage;

	// Use this for initialization
	void Start () {
        damage = GetComponentInParent<LauncherBullet>().bulletDamage;
//        launcher = GameObject.Find("Weapon/Launcher").GetComponent<Launcher>();
	}
	

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Monster"))
        {
            other.GetComponent<Monster>().GetDamage(damage);


            PlayerHud.playerHudScript.AttackHit();
        }
    }

}

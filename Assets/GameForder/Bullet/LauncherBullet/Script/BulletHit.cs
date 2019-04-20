using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHit : MonoBehaviour {

    Launcher launcher;

	// Use this for initialization
	void Start () {
        launcher = WeaponManager.weaponScript.GetComponentInChildren<Launcher>();
    }
	

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Monster"))
        {
            launcher.AttackHit(other.gameObject);
        }
    }

}

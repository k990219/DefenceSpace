using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPack : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {

        transform.Rotate(new Vector3(0,50,0)*Time.deltaTime);

	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            for (int i = 0; i < WeaponManager.weaponScript.weapon.Count; i++)
            {
                WeaponManager.weaponScript.weapon[i].GetComponent<Weapon>().RechargeAmmo();
            }
        }
    }


}

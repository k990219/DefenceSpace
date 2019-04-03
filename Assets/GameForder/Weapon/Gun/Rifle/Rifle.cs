using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : Weapon {

    protected override void StartSet()
    {
        type = WeaponType.rifle;
        weaponDmg = 10f;
        maxAmmo = 30;
        isReload = false;
        fireRate = 0.3f;
        reloadRate = 1.2f;
        maxMagagine = 120;
        magagine = maxMagagine;
        ammo = maxAmmo;
    }

    protected override void FireType()
    {
        muzzleFlash.Play();
        audio.PlayOneShot(fireClip);

        RaycastHit hit;

        if (Physics.Raycast(shootPoint.position, shootPoint.forward, out hit))
        {
            if (hit.transform.tag.Equals("Monster"))
            {
                hit.transform.GetComponent<Monster>().GetDamage(weaponDmg);
                PlayerHud.playerHudScript.AttackHit();

            }
        }
    }
}

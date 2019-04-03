using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGun : Weapon {

    int shotgunPellet;

    Quaternion fireRotation;

    protected override void StartSet()
    {
        type = WeaponType.shootgun;
        weaponDmg = 10f;
        maxAmmo = 7;
        isReload = false;
        fireRate = 1.3f;
        reloadRate = 1.2f;
        maxMagagine = 120;
        magagine = maxMagagine;
        ammo = maxAmmo;
        shotgunPellet = 12;
    }

    protected override void FireType()
    {
        muzzleFlash.Play();
        audio.PlayOneShot(fireClip);

        float shotgunLange = 5.0f;
        float shotgunSpread = 0.15f;
        for (int i = 0; i < shotgunPellet; i++)
        {
            
            RaycastHit hit;
            Vector3 ray = transform.forward;

            ray.x += Random.Range(-shotgunSpread, shotgunSpread);
            ray.y += Random.Range(-shotgunSpread, shotgunSpread);
            ray.z += Random.Range(-shotgunSpread, shotgunSpread);

            //Debug.DrawRay(shootPoint.position, ray*shotgunLange, Color.red,10.0f);

            if (Physics.Raycast(shootPoint.position, ray,out hit, shotgunLange))
            {
               
                if (hit.transform.tag.Equals("Monster"))
                {
                    hit.transform.GetComponent<Monster>().GetDamage(weaponDmg);
                    PlayerHud.playerHudScript.AttackHit();

                }
            }
        }
    }


}

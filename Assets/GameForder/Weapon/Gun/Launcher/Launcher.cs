using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : Weapon
{

    protected override void Awake()
    {
        base.Awake();
    }

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected override void StartSet()
    {
        type = WeaponType.launcher;
        weaponDmg = 35;
        maxAmmo = 5;
        isReload = false;
        fireRate = 1.3f;
        reloadRate = 2.3f;
        maxMagagine = 20;
        magagine = maxMagagine;
        ammo = maxAmmo;
    }

    protected override void FireType()
    {
        muzzleFlash.Play();
        audio.PlayOneShot(fireClip);

        string bulletName = "LauncherBullet";
        GameObject bullet = ObjectPool.Instance.PopFromPool(bulletName);

        bullet.GetComponent<LauncherBullet>().bulletDamage = weaponDmg;
        bullet.transform.position = muzzlePoint.position;
        bullet.transform.rotation = muzzlePoint.rotation;

        bullet.SetActive(true);
        bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.up * 1200.0f);
    }

}

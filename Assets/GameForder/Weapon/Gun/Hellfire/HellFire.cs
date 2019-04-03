using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HellFire : Weapon
{
    HellfireEffect weaponEffect;
    Collider attackRange;

    protected override void Awake()
    {
        base.Awake();
        weaponEffect = GetComponentInChildren<HellfireEffect>();
        attackRange = weaponEffect.GetComponent<Collider>();

    }

    protected override void Start()
    {
        base.Start();

        weaponEffect.damage = weaponDmg;
        weaponEffect.fireRate = fireRate;
        attackRange.enabled = false;
    }

    private void OnDisable()
    {
        audio.Stop();
        attackRange.enabled = false;
    }

    protected override void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            audio.Stop();
            attackRange.enabled = false;
        }

        base.Update();
    }


    protected override void StartSet()
    {
        type = WeaponType.hellfire;
        weaponDmg = 5f;
        maxAmmo = 120;
        isReload = false;
        fireRate = 0.1f;
        reloadRate = 3.8f;
        maxMagagine = 240;
        magagine = maxMagagine;
        ammo = maxAmmo;
    }

    protected override void FireType()
    {
        muzzleFlash.Play();
        if (!audio.isPlaying)
            audio.PlayOneShot(fireClip);

        attackRange.enabled = true;

    }

}

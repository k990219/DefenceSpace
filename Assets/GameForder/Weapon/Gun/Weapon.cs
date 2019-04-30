using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon: MonoBehaviour {
    
    public Transform muzzlePoint;    
    public ParticleSystem muzzleFlash;

    protected Transform shootPoint;

    public AudioClip fireClip;
    public AudioClip reloadClip;
    public AudioClip hitClip;
    protected AudioSource audio;

    public enum ShootMode { idle, sprint, reload, shoot }
    public ShootMode mode;

    public enum WeaponType { rifle, launcher, shootgun,hellfire }
    public WeaponType type;

    float fireTime;
    public float fireRate;
    public float reloadRate;
    public int maxMagagine;
    public int maxAmmo;
    public int magagine;
    public int ammo;
    public bool isReload;
    private float damage;
    public float weaponDmg { get { return damage; } set { damage = value; } }

    Animator ani;


    // Use this for initialization
    protected virtual void Awake()
    {
        shootPoint = GetComponentInParent<Transform>();
        audio = GetComponentInParent<AudioSource>();

    }

    protected virtual void Start () {
        StartSet();
        ani = PlayerManager.playerScript.GetComponent<Animator>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (PlayerManager.playerScript.isDead)
            return;

        if (Input.GetMouseButton(0) && (mode == ShootMode.idle)
            && ani.GetCurrentAnimatorStateInfo(0).IsName("Idle_Gun"))
        {
            if (isReload)
                return;
            if (ammo > 0)
            {
                mode = ShootMode.shoot;
                Fire();
            }
            else if (magagine > 0)
            {
                Reload();
            }
        }
        else
        {
            mode = ShootMode.idle;
        }

        if (Input.GetKeyDown(KeyCode.R) && mode == ShootMode.idle
            && magagine > 0)
        {
            if (!isReload)
                Reload();
        }

        if (fireTime < fireRate)
        {
            fireTime += Time.deltaTime;
        }

    }

    void Fire()
    {

        if (fireTime < fireRate || ammo <= 0)
            return;

        ammo--;

        FireType();
        
        fireTime = 0;
    }

    protected virtual void FireType() { }

    protected virtual void StartSet() {}

    IEnumerator CallReload()
    {
        isReload = true;
        PlayerHud.playerHudScript.reload.gameObject.SetActive(true);
        mode = ShootMode.reload;
        audio.PlayOneShot(reloadClip);
        yield return new WaitForSeconds(reloadRate);

        if (magagine + ammo > maxAmmo)
        {
            magagine -= (maxAmmo - ammo);
            ammo = maxAmmo;
        }
        else
        {
            ammo += magagine;
            magagine = 0;
        }

        mode = ShootMode.idle;
        isReload = false;

    }

    

    public void AttackHit(GameObject target)
    {
        Debug.Log(target.name);
        target.GetComponent<Monster>().GetDamage(weaponDmg);
        PlayerHud.playerHudScript.AttackHit();
        if(!audio.isPlaying && audio.clip!=hitClip)
            audio.PlayOneShot(hitClip);
    }

    void Reload()
    {
        if(ammo < maxAmmo)
            StartCoroutine("CallReload");
    }

    public void ChaingeSprint()
    {
        mode = ShootMode.sprint;
    }
    

    public void RechargeAmmo()
    {
        magagine = maxMagagine;
    }

}

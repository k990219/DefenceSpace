using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHud : MonoBehaviour
{

    private static PlayerHud instance;
    public static PlayerHud playerHudScript
    {
        get
        {
            if (!instance)
            {
                instance = FindObjectOfType(typeof(PlayerHud)) as PlayerHud;
            }
            return instance;
        }
    }

    public UISlider boostSlider;
    private UISprite shieldBar;
    private UISprite playerHealthBar;
    private UISprite hit;
    private UISprite Ammo;
    public UISprite reload;


    private UISprite shipHealthBar;
    private UISprite timerBar;
    private UILabel gameScore;


    private UILabel shieldLabel;
    private UILabel healthLabel;
    private UILabel magagine;

    float hitAlpha = 1.0f;

    UISprite[] boostSprites;

    Color hitColor = new Vector4(1.0f, 1.0f, 1.0f,0.5f);
    float reloadTimer = 0;

    private void Awake()
    {
        GameObject centerHUD = GameObject.Find("HUDWindows/Center");


        boostSlider = centerHUD.transform.Find("EnergyBar").GetComponent<UISlider>();
        shieldBar = centerHUD.transform.Find("ShieldBar/CurrentShield").GetComponent<UISprite>();
        playerHealthBar = centerHUD.transform.Find("HealthBar/CurrentHealth").GetComponent<UISprite>();
        hit = centerHUD.transform.Find("Hit").GetComponent<UISprite>();
        Ammo = GameObject.Find("HUDWindows/Center/Ammo/CurrentAmmo").transform.GetComponent<UISprite>();
        reload = GameObject.Find("HUDWindows/Center").transform.Find("Reload").GetComponent<UISprite>();

        shipHealthBar = GameObject.Find("HUDWindows/Top/GameStatus/ShipHealth").GetComponent<UISprite>();
        timerBar = GameObject.Find("HUDWindows/Top/GameStatus/GameTimer").GetComponent<UISprite>();
        gameScore = GameObject.Find("HUDWindows/Top/GameStatus/GameScore").GetComponent<UILabel>();

        shieldLabel = GameObject.Find("HUDWindows/Side/ShieldTxt").GetComponent<UILabel>();
        healthLabel = GameObject.Find("HUDWindows/Side/HealthTxt").GetComponent<UILabel>();
        magagine = GameObject.Find("HUDWindows/Side/AmmoTxt").GetComponent<UILabel>();

}


    // Use this for initialization
    void Start()
    {
        boostSprites = boostSlider.GetComponentsInChildren<UISprite>();

    }

    // Update is called once per frame
    void Update()
    {

        magagine.text = WeaponManager.weaponScript.handWeapon.ammo.ToString() + " / " + WeaponManager.weaponScript.handWeapon.magagine.ToString();

        Ammo.fillAmount = ((float)(WeaponManager.weaponScript.handWeapon.ammo / (float)WeaponManager.weaponScript.handWeapon.maxAmmo));

        healthLabel.text = ((int)PlayerManager.playerScript.playerHP).ToString();
        shieldLabel.text = ((int)PlayerManager.playerScript.playerShield).ToString();

        shipHealthBar.fillAmount = GameShip.shipScript.shipHP / GameShip.shipScript.shipMaxHP;
        playerHealthBar.fillAmount = (PlayerManager.playerScript.playerHP / PlayerManager.playerScript.playerMaxHP) * 0.33f;
        shieldBar.fillAmount = (PlayerManager.playerScript.playerShield / PlayerManager.playerScript.playerMaxShield) * 0.33f;

        timerBar.fillAmount = GameManager.gameManager.gameTimer / GameManager.gameManager.gameStartTime;

        ReloadFill();

        boostSlider.value = PlayerManager.playerScript.playerBoost / PlayerManager.playerScript.playerMaxBoost;


        gameScore.text = "Score: " + GameManager.gameManager.gameScore.ToString();

        //        hit.color = Color.Lerp(hit.color, Color.clear, Time.deltaTime * 3.0f);
        hit.alpha = Mathf.Lerp(hit.alpha, 0.0f, Time.deltaTime * 5.0f);
    }


    void ReloadFill()
    {
        if (reload.gameObject.activeInHierarchy)
        {
            reloadTimer += Time.deltaTime;
            reload.fillAmount = reloadTimer / WeaponManager.weaponScript.handWeapon.reloadRate;

            if (reloadTimer > WeaponManager.weaponScript.handWeapon.reloadRate)
            {
                reload.gameObject.SetActive(false);
                reloadTimer = 0;
            }
        }
    }

    void Timer()
    {

    }

    public void AttackHit()
    {
        hit.alpha = hitAlpha;
//        hit.color = hitColor;
    }

    public void BoostBarEnable(bool useBoost)
    {

        for (int i = 0; i < boostSprites.Length; i++)
        {

            boostSprites[i].enabled = useBoost;
        }
    }
}

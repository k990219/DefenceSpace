using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour {
    
    private static PlayerManager instance;
    public static PlayerManager playerScript
    {
        get
        {
            if (!instance)
            {
                instance = FindObjectOfType(typeof(PlayerManager)) as PlayerManager;
            }
            return instance;
        }
    }

    public float playerMaxHP;
    public float playerMaxShield;
    public float playerMaxBoost;
    public float playerHP;
    public float playerShield;
    public float playerBoost;

    bool OnShield;
    bool shieldCharging;
    float shieldChargeTime;
    float shieldChargeCheck;
    float shieldChargeValue;
    float shieldChargeDelay;

    bool boostCharging;
    public bool boostUse;
    float boostChargeTime;
    float boostChargeCheck;
    float boostChargeValue;
    float boostChargeDelay;


    public bool isDead = false;

    private void Awake()
    {
    }

    void Start () {
        PlayerSet();
	}
	
	// Update is called once per frame
	void Update () {

        if (!isDead)
        {
            ShieldCheck();
            BoostCheck();

            if (playerHP <= 0)
                isDead = true;
        }

	}

    void PlayerSet()
    {
        isDead = false;
        shieldCharging = false;
        boostCharging = false;

        playerMaxHP = 150f;
        playerMaxShield = 250f;
        playerMaxBoost = 200f;

        shieldChargeTime = 0;
        shieldChargeCheck = 0.1f;
        shieldChargeValue = 10.0f;
        shieldChargeDelay = 3.0f;


        boostChargeTime = 0;
        boostChargeCheck = 0.1f;
        boostChargeValue = 15.0f;
        boostUse = false;

        playerHP = playerMaxHP;
        playerShield = playerMaxShield;
        playerBoost = playerMaxBoost;
    }



    void ShieldCheck()
    {
        OnShield = playerShield > 0 ? true : false;

        if (shieldCharging)
        {
            shieldChargeTime += Time.deltaTime;

            if (playerShield > playerMaxShield)
                playerShield = playerMaxShield;

            if (playerShield < playerMaxShield)
            {
                if (shieldChargeTime > shieldChargeCheck)
                {
                    shieldChargeTime = 0;
                    playerShield += shieldChargeValue * Time.deltaTime;
                }

            }
        }
    }

    void BoostCheck()
    {
        if (playerBoost < playerMaxBoost)
            boostCharging = true;
        else
        {
            boostCharging = false;
            playerBoost = playerMaxBoost;
        }

        if (boostCharging)
        {
            boostChargeTime += Time.deltaTime;

            if (playerBoost < playerMaxBoost)
            {

                if (boostChargeTime > boostChargeCheck)
                {
                    boostChargeTime = 0;
                    if (boostUse)
                        playerBoost += boostChargeValue * 0.3f * Time.deltaTime;
                    else
                        playerBoost += boostChargeValue * Time.deltaTime;
                }
            }
        }

        PlayerHud.playerHudScript.BoostBarEnable(boostCharging);
    }

    public void GetDamage(float damage)
    {
        shieldCharging = false;
        StartCoroutine("ShieldReCharge");

        if (OnShield)
        {
            playerShield -= damage;
            if (playerShield <= 0)
            {
                playerShield = 0;
            }
        }
        else
        {
            playerHP -= damage;
            if (playerHP < 0)
            {
                playerHP = 0;
                GameManager.gameManager.GameOverMsg();
            }
        }

    }

    IEnumerator  ShieldReCharge()
    {
        yield return new WaitForSeconds(shieldChargeDelay);
        shieldChargeTime = 0;
        shieldCharging = true;

    }

}

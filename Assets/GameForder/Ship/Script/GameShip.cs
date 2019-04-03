using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameShip : MonoBehaviour {

    private static GameShip instance;
    public static GameShip shipScript
    {
        get
        {
            if (!instance)
            {
                instance = FindObjectOfType(typeof(GameShip)) as GameShip;
            }
            return instance;
        }
    }

    public float shipHP { get; set; }
    public float shipMaxHP { get; set; } 
    

    public GameObject []ShiptPoint;

    private void Awake()
    {
    }

    // Use this for initialization
    void Start () {
        StartSet();
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void GetDamage(float damage)
    {
        shipHP -= damage;
        if (shipHP < 0)
        {
            shipHP = 0;
            GameManager.gameManager.GameOverMsg();
        }
    }

    void StartSet()
    {
        shipMaxHP = 1000;
        shipHP = shipMaxHP;
    }

}

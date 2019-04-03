
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttack : MonoBehaviour {

    Monster monster;

    private void Awake()
    {

    }

    // Use this for initialization
    void Start () {
        monster = transform.parent.GetComponentInParent<Monster>();
    }
	

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Player")
        {
            PlayerManager.playerScript.SendMessage("GetDamage",monster.monsterDamage);
            
        }

        else if(other.gameObject.tag == "Ship")
        {
            GameShip.shipScript.SendMessage("GetDamage", monster.monsterDamage);

        }

    }

    private void OnTriggerExit(Collider other)
    {
        
    }



}

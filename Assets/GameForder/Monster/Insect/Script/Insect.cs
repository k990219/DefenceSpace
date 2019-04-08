using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Insect : ShootMonster{

    private float mapYPosition = 1.0f;

    protected override void OnEnable()
    {
        float rand = Random.Range(mapYPosition + 5f, mapYPosition + 15f);
        transform.position = new Vector3(transform.position.x, rand, transform.position.z);
    }


    protected override void MonsterSet()
    {
        monsterName = "Insect";
        bulletName = "InsectBullet";
        monsterHP = 80f;
        monsterSpeed = 4.5f;
        monsterRun = 6f;
        monsterDamage = 15f;
        idleAttack = 3.5f;
        moveAttack = 1.5f;
        playerAttackRange = 12f;
        shipAttackRange = playerAttackRange + 4f;
        runRange = 7f;
        traceRange = 15f;
        turnSpeed = 2.5f;
        randAttackMax = 1;
        monsterPoint = 35;

    }


    protected void ShootAttack()
    {
        GameObject bullet = ObjectPool.Instance.PopFromPool(bulletName);

        bullet.transform.position = attackPosition.position;
        bullet.GetComponent<InsectBullet>().SetAttackerPosition(attackPosition);
        bullet.GetComponent<InsectBullet>().SetAttacker(this.gameObject);

        bullet.SetActive(true);

    }

}

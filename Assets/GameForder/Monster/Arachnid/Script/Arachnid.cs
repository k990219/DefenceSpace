using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arachnid : MeleeMonster
{

    private ParticleSystem rangeAttack;

    protected override void Awake()
    {
        base.Awake();
        rangeAttack = gameObject.GetComponentInChildren<ParticleSystem>();
    }

    protected override void MonsterSet()
    {
        monsterName = "Arachnid";
        monsterHP = 120f;
        monsterSpeed = 4.8f;
        monsterDamage = 20f;
        idleAttack = 3.5f;
        moveAttack = 1.0f;
        playerAttackRange = 4f;
        shipAttackRange = playerAttackRange + 1;
        traceRange = 13f;
        turnSpeed = 2.5f;
        randAttackMax = 1;
        monsterPoint = 25;
    }


    public override void MeleeAttackOn()
    {
        base.MeleeAttackOn();
        rangeAttack.Play();

    }

    public override void MeleeAttackOff()
    {
        base.MeleeAttackOff();
        rangeAttack.Stop();
    }
}

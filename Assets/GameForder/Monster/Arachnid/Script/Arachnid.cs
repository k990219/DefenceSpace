﻿using System.Collections;
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
        monsterHP = 60f;
        monsterSpeed = 2.5f;
        monsterDamage = 30f;
        idleAttack = 3.5f;
        moveAttack = 1.0f;
        playerAttackRange = 6f;
        shipAttackRange = playerAttackRange + 1;
        traceRange = 20f;
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

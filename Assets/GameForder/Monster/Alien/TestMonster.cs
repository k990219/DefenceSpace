using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMonster : Monster {

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        MonsterSet();
        base.Start();

    }

    protected override void MonsterSet()
    {
        monsterHP = 100f;
        monsterSpeed = 4f;
        monsterRun = 6f;
        monsterDamage = 100f;
        idleAttack = 3.5f;
        playerAttackRange = 2.5f;
        shipAttackRange = playerAttackRange + 2f;
        runRange = 7f;
        traceRange = 15f;
        turnSpeed = 2.5f;
        randAttackMax = 1;
    }
}

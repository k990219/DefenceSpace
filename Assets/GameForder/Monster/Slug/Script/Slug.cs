using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slug : MeleeMonster
{

    protected override void MonsterSet()
    {
        monsterName = "Slug";
        monsterHP = 50f;
        monsterSpeed = 4.5f;
        monsterRun = 7f;
        monsterDamage = 15f;
        idleAttack = 3.0f;
        moveAttack = 0.5f;
        playerAttackRange = 3f;
        shipAttackRange = playerAttackRange + 1.3f;
        runRange = 7f;
        traceRange = 18;
        turnSpeed = 2.5f;
        randAttackMax = 3;
        monsterPoint = 15;
    }
}

﻿
public class Juggernaut : MeleeMonster{

    
    protected override void MonsterSet()
    {
        monsterName = "Juggernaut";
        monsterHP = 25f;
        monsterDamage = 30f;
        idleAttack = 4.0f;
        moveAttack = 1.0f;
        playerAttackRange = 4.5f;
        shipAttackRange = playerAttackRange + 1.8f;
        traceRange = 12f;
        turnSpeed = 3f;
        monsterSpeed = 0.2f;
        randAttackMax =3;
        rewpawnTime = 30f;
        monsterPoint = 100;
    }
}

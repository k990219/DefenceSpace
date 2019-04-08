
public class Juggernaut : MeleeMonster{

    
    protected override void MonsterSet()
    {
        monsterName = "Juggernaut";
        monsterHP = 200f;
        monsterDamage = 30f;
        idleAttack = 4.0f;
        moveAttack = 1.0f;
        playerAttackRange = 4.5f;
        shipAttackRange = playerAttackRange + 1.8f;
        traceRange = 12f;
        turnSpeed = 3f;
        monsterSpeed = 6.0f;
        randAttackMax =3;
        rewpawnTime = 30f;
        monsterPoint = 100;
    }
}

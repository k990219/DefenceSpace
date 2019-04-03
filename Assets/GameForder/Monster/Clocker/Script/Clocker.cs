
public class Clocker : MeleeMonster{
   
    protected override void MonsterSet()
    {
        monsterName = "Clocker";
        monsterHP = 15f;
        monsterSpeed = 0.3f;
        monsterRun = 5f;
        monsterDamage = 15f;
        idleAttack = 3.5f;
        moveAttack = 0.5f;
        playerAttackRange = 3.5f;
        shipAttackRange = playerAttackRange + 0.8f;
        runRange = 7f;
        traceRange = 10f;
        turnSpeed = 3.5f;
        randAttackMax = 2;
        monsterPoint = 35;
    }
}

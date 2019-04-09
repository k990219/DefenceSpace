
public class Clocker : MeleeMonster{
   
    protected override void MonsterSet()
    {
        monsterName = "Clocker";
        monsterHP = 100f;
        monsterSpeed = 7.5f;
        monsterRun = 5f;
        monsterDamage = 25f;
        idleAttack = 3.5f;
        moveAttack = 0.5f;
        playerAttackRange = 3.5f;
        shipAttackRange = playerAttackRange + 0.8f;
        runRange = 7f;
        traceRange = 18f;
        turnSpeed = 3.5f;
        randAttackMax = 2;
        monsterPoint = 35;
    }
}

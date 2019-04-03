using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootMonster : Monster
{
    protected Transform attackPosition;

    protected string bulletName;

    protected override void Awake()
    {
        base.Awake();
        attackPosition = transform.Find("AttackPosition");
    }

    protected override void MonsterAttackType()
    {
        targetPosition = targetSelect.transform.position;
        targetPosition.y += 1;
        attackPosition.LookAt(targetPosition);
        thisPosition = transform.position;
    }


}

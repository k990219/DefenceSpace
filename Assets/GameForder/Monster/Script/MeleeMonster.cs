using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeMonster : Monster
{

    protected override void Awake()
    {
        base.Awake();
        attackCollider = transform.Find("HitBox").GetComponent<Collider>();
        attackCollider.enabled = false;
    }


    public virtual void MeleeAttackOn()
    {
        attackCollider.enabled = true;
    }

    public virtual void MeleeAttackOff()
    {
        attackCollider.enabled = false;
    }

    protected  override void MonsterAttackType()
    {
        targetPosition = new Vector3(targetSelect.transform.position.x, 0, targetSelect.transform.position.z);
        thisPosition = new Vector3(transform.position.x, 0, transform.position.z);
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        GetComponent<CharacterController>().Move(Vector3.down * Time.deltaTime);
    }
}

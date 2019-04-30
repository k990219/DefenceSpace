using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Monster : MonoBehaviour
{

    protected enum MonsterStates { idle, attack, walk, die }
    protected MonsterStates state = MonsterStates.idle;


    Animator animator { get { return GetComponent<Animator>(); } }

    protected Vector3 targetDir { get { return targetPosition - thisPosition; } set { targetDir = value; } }
    protected GameObject shipObject { get { return GameShip.shipScript.gameObject; } }
    protected GameObject playerObject { get { return PlayerManager.playerScript.gameObject; } }
    protected GameObject targetSelect
    {
        get
        {
            playerDist = Vector3.Distance(playerObject.transform.position, transform.position);
            GameObject returnObj = null;

            if (playerDist < traceRange&&!returnObj)
                return playerObject;

            else
            {
                float farDist = Mathf.Infinity;

                foreach (GameObject obj in GameShip.shipScript.ShiptPoint)
                {
                    float currentDist = Vector3.Distance(obj.transform.position, transform.position);

                    if (farDist > currentDist)
                    {
                        returnObj = obj;
                        farDist = currentDist;
                    }
                }

                return returnObj;
            }

        }
    }



    protected float monsterAttackRange
    {
        get
        {
            return targetSelect.tag == "Player" ? playerAttackRange : shipAttackRange;
        }
    }
    protected float defaultAttackRange
    {
        get
        {
            if (targetSelect == GameShip.shipScript.ShiptPoint[2])
            {
                return shipAttackRange * 1.3f;
            }
            else if (targetSelect == GameShip.shipScript.ShiptPoint[0])
            {
                return shipAttackRange * 0.7f;
            }
            else
            {
                return shipAttackRange;
            }
        }
    }
    protected float monsterAttackDelay
    {
        get
        {
            return firstAttack ? moveAttack : idleAttack;
        }
    }
    protected float targetDist { get { return Vector3.Distance(targetPosition, thisPosition); } }

    public float monsterDamage;
    public float attackDelay;
    protected string monsterName;
    protected float monsterHP;
    protected float monsterMaxHP;
    protected float monsterSpeed;
    protected float monsterRun;
    protected float shipAttackRange;
    protected float playerAttackRange;
    protected float runRange;
    protected float traceRange;
    protected float turnSpeed = 3f;
    protected float moveAttack;
    protected float idleAttack;
    protected int randAttackMin;
    protected int randAttackMax;
    protected float rewpawnTime;
    protected int monsterPoint;
    protected float shipDist;
    protected float playerDist;
    protected bool firstAttack = true;
    protected float attackTime = 0f;
    protected bool isDead;

    protected Vector3 targetPosition;
    protected Vector3 thisPosition;


    protected Collider attackCollider;

    protected virtual void Awake(){
    }

    protected virtual void OnEnable()
    {
        monsterHP = monsterMaxHP;
        isDead = false;
    }

    protected virtual void Start()
    {
        MonsterSet();        
    }

    // Update is called once per frame
    protected virtual void FixedUpdate()
    {
        if (isDead)
            return;

        MonsterAttackType();

        if (targetDist < monsterAttackRange)
        {
            Attack();
        }

        else
        {
            TraceTarget();
        }
        
    }
    
    public void Attack()
    {
        animator.SetBool("isMoving", false);
        attackTime += Time.deltaTime;

        if (attackTime > monsterAttackDelay)
        {
            int aRand = Random.Range(0, randAttackMax);
            animator.SetInteger("randAttack", aRand);

            animator.SetTrigger("isAttack");
            attackTime = 0;
            firstAttack = false;
        }
        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation,
                Quaternion.LookRotation(targetDir), turnSpeed * Time.deltaTime);
        }
    }

    protected virtual void MonsterSet() {}

    protected virtual void MonsterAttackType() { }

    void TraceTarget()
    {
        animator.SetBool("isMoving", true);

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Move"))
        {
            firstAttack = true;
            attackTime = 0;

            targetDir.Normalize();
        
            GetComponent<CharacterController>().Move(targetDir * monsterSpeed *Time.deltaTime*0.1f);
        }
        transform.rotation = Quaternion.Slerp(transform.rotation,
            Quaternion.LookRotation(targetDir), turnSpeed * Time.deltaTime);
    }

    IEnumerator PushMonster()
    {
        yield return new WaitForSeconds(2.5f);

        ObjectPool.Instance.PushToPool(monsterName, gameObject);
        GameManager.gameManager.DeadMonsterPush(gameObject);
    }

    public void GetDamage(float damage)
    {
        if (isDead)
            return;

        monsterHP -= damage;
//        Debug.Log(transform.name + "HP: " + monsterHP);
        if (monsterHP <= 0)
            Dead();
    }

    public void Dead()
    {
        GameManager.gameManager.gameScore += monsterPoint;
        animator.SetTrigger("isDead");
        isDead = true;

        StartCoroutine("PushMonster");
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : EnemyMelleFSM
{
    public GameObject BossBolt;
    public Transform AttackPoint;

    WaitForSeconds Delay50 = new WaitForSeconds(0.05f);
    WaitForSeconds Delay500 = new WaitForSeconds(0.5f);


    public LayerMask meleeLayerMask;
    public GameObject DangerMarker;

    private Vector3 endPoint;
    public GameObject hitEffect;
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, playerRealizeRagne);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
     
    void Start()
    { 
        base.Start();
        attackCoolTime = 1.5f;
        attackCoolTimeCacl = attackCoolTime;

        playerRealizeRagne = 30 ;
        attackRange = 20;
        moveSpeed = 1;
        nvAgent.stoppingDistance = 4;
    }

    protected override void InitMonster()
    {
        maxHp = 5000;
        currentHp = maxHp;
        damage = 10;
    }
    protected override IEnumerator Attack()
    {
        yield return null;
        nvAgent.isStopped = true;
        transform.LookAt(Player.transform.position);
        if(Random.value < 0.6)
        {
            if(Random.value<0.5)
            {
                if(!Anim.GetCurrentAnimatorStateInfo(0).IsName("Attack01"))
                {
                    Anim.SetTrigger("Attack01");
                }
            }
            else
            {
                if (!Anim.GetCurrentAnimatorStateInfo(0).IsName("Attack02"))
                {
                    Anim.SetTrigger("Attack02");
                }
            }
            yield return Delay50;
        }
        // 돌진 
        else
        {
            Vector3 NewPosition = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
            Physics.Raycast(NewPosition, transform.forward, out RaycastHit hit, 100f, meleeLayerMask);

            if (hit.transform.CompareTag("Wall"))
            {
                GameObject DangerMarkerClone = Instantiate(DangerMarker, NewPosition, transform.rotation);
                DangerMarkerClone.GetComponent<DangerLine>().EndPosition = hit.point;
                endPoint = DangerMarkerClone.GetComponent<DangerLine>().EndPosition;
            }

            if (!Anim.GetCurrentAnimatorStateInfo(0).IsName("GetHit"))
            {
                Anim.SetTrigger("GetHit");
            }

            yield return Delay500;
            nvAgent.stoppingDistance = 0;
            //nvAgent.SetDestination(Player.transform.position);
            nvAgent.SetDestination(endPoint);
            nvAgent.isStopped = false;
            nvAgent.speed = 200f;
             

            while (Vector3.Distance(nvAgent.destination,transform.position) > 1)
            {
                if(!Anim.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
                {
                    Anim.SetTrigger("Walk");
                }
                yield return null;
            }
             
            nvAgent.speed = moveSpeed;
            nvAgent.stoppingDistance = attackRange;
        }
        canAtk = false;
        currentState = State.Idle;
    }

    public void Attack01()
    {
        Instantiate(BossBolt, AttackPoint.position, Quaternion.Euler(transform.eulerAngles + new Vector3(0, -35f, 0)));
        Instantiate(BossBolt, AttackPoint.position, Quaternion.Euler(transform.eulerAngles + new Vector3(0, 0, 0)));
        Instantiate(BossBolt, AttackPoint.position, Quaternion.Euler(transform.eulerAngles + new Vector3(0, 35f, 0)));
    }
    public void Attack02()
    {
        Instantiate(BossBolt, AttackPoint.position, Quaternion.Euler(transform.eulerAngles + new Vector3(0, -25f, 0)));
        Instantiate(BossBolt, AttackPoint.position, Quaternion.Euler(transform.eulerAngles + new Vector3(0, -10f, 0)));
        Instantiate(BossBolt, AttackPoint.position, Quaternion.Euler(transform.eulerAngles + new Vector3(0, 10f, 0)));
        Instantiate(BossBolt, AttackPoint.position, Quaternion.Euler(transform.eulerAngles + new Vector3(0, 25f, 0)));

    }

    // 몬스터 맞을경우
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.CompareTag("Bullet"))
        {
            currentHp = currentHp - other.gameObject.GetComponent<ControlBullet>().damage; 
            Instantiate(hitEffect, transform.position, transform.rotation);
            UIManager.Instance.BossCurrentHp = currentHp;

            if (currentHp <= 0)
            {
                //GetComponent<MonsterAI>().state = MonsterAI.State.DIE;
                this.gameObject.GetComponent<BoxCollider>().enabled = false;
                PlayerTargeting.Instance.EnemyList.Remove(this.gameObject);
                CheckRoom.Instance.EnemyListInRoom.Remove(this.gameObject);
                Destroy(gameObject, 2f);
                // Destroy(hpBar, 1.5f);
                //animator.SetTrigger(hashDie);
                UIManager.Instance.isBossRoom = false;
            }
        }
        
    }
     
}

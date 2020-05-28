using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBase : MonoBehaviour
{
    public float maxHp = 1000;
    public float currentHp = 1000;

    public float damage = 100;

    protected float playerRealizeRagne = 10;
    protected float attackRange = 5;
    protected float attackCoolTime = 3;
    protected float attackCoolTimeCacl = 3;
    protected bool canAtk = true;

    protected float moveSpeed = 2;

    protected GameObject Player;
    protected NavMeshAgent nvAgent;
    protected float distance;

    protected GameObject parentRoom;
    protected Animator Anim;
    protected Rigidbody rb;

    public LayerMask layerMask;

    protected void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");

        nvAgent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        Anim = GetComponent<Animator>();

        //parentRoom = transform.parent.transform.parent.gameObject;

        UIManager.Instance.BossCurrentHp = currentHp;
        UIManager.Instance.BossMaxHp = maxHp;

        StartCoroutine(CalcCoolTime());
    }

    protected bool CanAtkStateFun()
    {
        Vector3 targetDir = new Vector3(Player.transform.position.x - transform.position.x, 0f, Player.transform.position.z - transform.position.z);
        Physics.Raycast(new Vector3(transform.position.x, 3f, transform.position.z), targetDir, out RaycastHit hit, 100, layerMask);
        distance = Vector3.Distance(Player.transform.position, transform.position);
        Debug.DrawRay(new Vector3(transform.position.x, 3f, transform.position.z), targetDir ,Color.red);
        //if (hit.transform == null)
        //{
        //    Debug.Log("null...");
        //    return false;
        //}
        //if (hit.transform.CompareTag("Player") && distance <= attackRange)
        Debug.Log("탐지중");
        if (Physics.Raycast(transform.position, targetDir, out hit, 30) && UIManager.Instance.isBossRoom)
        { 
            Debug.Log("탐지완료");
            return true;
        }
        else return false; 
    }
    protected virtual IEnumerator CalcCoolTime()
    {
        while(true)
        {
            yield return null;
            if(!canAtk)
            {
                attackCoolTimeCacl -= Time.deltaTime;
                if(attackCoolTimeCacl <=0)
                {
                    attackCoolTimeCacl = attackCoolTime;
                    canAtk = true;
                }
            }
        }
    }
}

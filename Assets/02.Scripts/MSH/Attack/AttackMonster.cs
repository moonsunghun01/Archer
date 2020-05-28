using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class AttackMonster : MonoBehaviour
{
    protected Animator animator;
    protected Transform playerTr;
    protected Transform monsterTr;

    protected readonly int hashAttack = Animator.StringToHash("Attack");

    public float nextAttack = 0;
    public float attackRate = 2f;
    protected readonly float damping = 10;

    public bool isAttack = false;

    protected NavMeshAgent navMonster;

    void Start()
    {
        playerTr = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        monsterTr = GetComponent<Transform>();
        animator = GetComponent<Animator>();
        //audio = GetComponent<AudioSource>();
        navMonster = GetComponent<NavMeshAgent>();
    }
    public virtual void Update()
    { 
        if (isAttack)
        { 
            if (Time.time >= nextAttack)
            {
                Attack();
                nextAttack = Time.time + attackRate;
            }
            Quaternion rot = Quaternion.LookRotation(playerTr.position - monsterTr.position);
            monsterTr.rotation = Quaternion.Slerp(monsterTr.rotation, rot, Time.deltaTime * damping);
        }
    }

    public virtual void Attack() { }

}

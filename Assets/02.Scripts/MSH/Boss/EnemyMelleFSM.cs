using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelleFSM : EnemyBase
{
    public enum State
    {
        Idle,
        Move,
        Attack
    }

    public State currentState = State.Idle;
    WaitForSeconds Delay500 = new WaitForSeconds(0.5f);
    WaitForSeconds Delay250 = new WaitForSeconds(0.25f);
    protected void Start()
    {
        base.Start();
        //parentRoom = transform.parent.transform.parent.gameObject;

        StartCoroutine(FSM());
    }

    protected virtual void InitMonster() { } 
    protected virtual IEnumerator FSM()
    {
        yield return null;

        InitMonster();
        //while (!parentRoom.GetComponent<RoomCondition>().playerInThisRoom) yield return Delay500;
        while (true) yield return StartCoroutine(currentState.ToString());
    }

    protected virtual IEnumerator Idle()
    {
        yield return null;
        if(!Anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            Anim.SetTrigger("Idle");
        }

        if(CanAtkStateFun ())
        {
            if(canAtk)
            {
                currentState = State.Attack;
            }
            else
            {
                currentState = State.Idle;
                transform.LookAt(Player.transform.position);
            }
        }
        else
        {
            currentState = State.Idle;
        }
    }
    protected virtual void AtkEffect() { }
    protected virtual IEnumerator Attack()
    {
        yield return null;

        nvAgent.stoppingDistance = 0;
        nvAgent.isStopped = true;
        nvAgent.SetDestination(Player.transform.position);
        yield return Delay500;

        nvAgent.isStopped = false;
        nvAgent.speed = 30;
        canAtk = false;

        if(!Anim.GetCurrentAnimatorStateInfo(0).IsName("stun"))
        {
            Anim.SetTrigger("Attack01");
        }
        AtkEffect();
        yield return Delay500;

        nvAgent.speed = moveSpeed;
        nvAgent.stoppingDistance = attackRange;
        currentState = State.Idle;
    }
    protected virtual IEnumerator Move()
    {
        yield return null;

        if(!Anim.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
        {
            Anim.SetTrigger("Walk");
        }
        if(CanAtkStateFun() && canAtk)
        {
            currentState = State.Attack;
        }
        else if(distance > playerRealizeRagne)
        {
            nvAgent.SetDestination(transform.parent.position - Vector3.forward * 5);
        }
        else
        {
            nvAgent.SetDestination(Player.transform.position);
        }
    }
}

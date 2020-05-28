using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatAI : MonoBehaviour
{
    // 몬스터 상태
    public enum State
    {
        IDLE, 
        ATTACK,
        HIT,
        DIE
    } 
    public State state = State.IDLE;
    // 플레이어 위치
    private Transform playerTr;
    // 몬스터 위치
    private Transform monsterTr;
    // 몬스터 애니매이션 설정
    private Animator animator;
    // 몬스터 공격 거리 배열로 초기화 0.Turtle / 1.Mage 
    public float attackDist = 30; 
    // 몬스터 공격 거리 인덱스 
    public int attackDistIdx;
    // 몬스터 공격 탐지 인덱스
    public int traceDistIdx;



    public bool isDie = false;
    private WaitForSeconds ws; 
    private AttackMonster attackMonster;

    private readonly int hashIsMove = Animator.StringToHash("IsMove"); 

    private void Awake()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        if (player != null) playerTr = player.GetComponent<Transform>();

        monsterTr = GetComponent<Transform>(); 
        animator = GetComponent<Animator>();
        attackMonster = GetComponent<AttackMonster>();
        ws = new WaitForSeconds(0.3f);
    }

    private void OnEnable()
    {
        StartCoroutine(CheckState());
        StartCoroutine(Action());
    }
    IEnumerator Action()
    {
        while (!isDie)
        {
            yield return ws;

            switch (state)
            {
                case State.IDLE: 
                    break; 
                case State.ATTACK:
                    animator.SetBool(hashIsMove, false);
                    if (attackMonster.isAttack == false) attackMonster.isAttack = true; 
                    break;
                case State.HIT:
                    animator.SetBool(hashIsMove, false); 
                    break;
                case State.DIE:
                    attackMonster.isAttack = false;
                    isDie = true;
                    animator.SetBool(hashIsMove, false); 
                    break;
            }
        }
    }
    IEnumerator CheckState()
    {
        while (!isDie)
        {
            if (state == State.DIE) yield break;

            float dist = Vector3.Distance(playerTr.position, monsterTr.position);

            if (dist <= attackDist)
            {
                state = State.ATTACK;
            } 
            else
            {
                state = State.IDLE;
            }
            yield return ws;
        }
    }

}

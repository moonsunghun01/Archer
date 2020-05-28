using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAI : MonoBehaviour
{ 
    // 몬스터 상태
    public enum State
    {
        IDLE,
        TRACE,
        ATTACK, 
        HIT,
        DIE
    }
    // 초기 몬스터 상태
    public State state = State.IDLE;
    // 플레이어 위치
    private Transform playerTr;
    // 몬스터 위치
    private Transform monsterTr;
    // 몬스터 애니매이션 설정
    private Animator animator;
    // 몬스터 공격 거리 배열로 초기화 0.Turtle / 1.Mage 
    public float[] attackDist = new float[3] {2,8,2 }; 
    // 몬스터 탐지 거리 배열로 초기화
    public float[] traceDist = new float[3] {30,15,0 }; 
    // 몬스터 공격 거리 인덱스 
    public int attackDistIdx;
    // 몬스터 공격 탐지 인덱스
    public int traceDistIdx;



    public bool isDie = false;
    private WaitForSeconds ws;
    private MoveMonster moveMonster;
    private AttackMonster attackMonster;
     
    private readonly int hashIsMove = Animator.StringToHash("IsMove");
    private readonly int hashAttack = Animator.StringToHash("Attack");

    private void Awake()
    { 
        var player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)playerTr =  player.GetComponent<Transform>();

        monsterTr = GetComponent<Transform>();
        moveMonster = GetComponent<MoveMonster>();
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
        while(!isDie)
        {
            yield return ws;

            switch (state)
            {
                case State.IDLE:

                    break;
                case State.TRACE:
                    animator.SetBool(hashIsMove, true);
                    moveMonster.traceTarget = playerTr.position;
                    attackMonster.isAttack = false;
                    break;
                case State.ATTACK:
                    animator.SetBool(hashIsMove, false);
                    if(attackMonster.isAttack == false)attackMonster.isAttack = true;
                    moveMonster.Stop();
                    break;
                case State.HIT:
                    animator.SetBool(hashIsMove, false);
                    moveMonster.Stop();
                    break;
                case State.DIE:
                    attackMonster.isAttack = false;
                    isDie = true;
                    animator.SetBool(hashIsMove, false);
                    moveMonster.Stop();
                    break;
            }
        }
    }
    IEnumerator CheckState()
    {
        while(!isDie)
        {
            if (state == State.DIE) yield break;

            float dist = Vector3.Distance(playerTr.position, monsterTr.position);

            if(dist<=attackDist[attackDistIdx])
            {
                state = State.ATTACK;
            }
            else if(dist <= traceDist[traceDistIdx])
            {
                state = State.TRACE;
            }
            else
            {
                state = State.IDLE;
            }
            yield return ws;
        }
    }

}

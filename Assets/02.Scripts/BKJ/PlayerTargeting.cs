using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTargeting : MonoBehaviour
{
    public static PlayerTargeting Instance // singlton     
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<PlayerTargeting>();
                if (instance == null)
                {
                    var instanceContainer = new GameObject("PlayerTargeting");
                    instance = instanceContainer.AddComponent<PlayerTargeting>();
                }
            }
            return instance;
        }
    }
    private static PlayerTargeting instance;

    public Animator playerAnimator;

    public bool getATarget = false;
    float currentDist = 0;      //현재 거리
    float closetDist = 100f;    //가까운 거리
    float TargetDist = 100f;   //타겟 거리
    int closeDistIndex = 0;    //가장 가까운 인덱스
    public int TargetIndex = -1;      //타겟팅 할 인덱스
    int prevTargetIndex = 0;
    public LayerMask layerMask;

    public float attackSpeed = 1f;
    public int attackSpeedLv;
    public List<GameObject> EnemyList = new List<GameObject>();
    //Enemy를 담는 List 

    //public GameObject PlayerBolt;  //발사체
    //public Transform AttackPoint;

    private void Start()
    {
        playerAnimator = GetComponent<Animator>();
        attackSpeedLv = PlayerData.Instance.playerSkill[6];
        attackSpeed = 1 + PlayerData.Instance.playerSkill[6] * 0.5f;
        playerAnimator.SetFloat("ATTACKSPEED", attackSpeed);
    }

    void OnDrawGizmos()
    {
        if (getATarget)
        {
            for (int i = 0; i < EnemyList.Count; i++)
            {
                if (EnemyList[i] == null)
                {
                    return;
                }

                RaycastHit hit; //	

                bool isHit = Physics.Raycast(transform.position, EnemyList[i].transform.position - transform.position,//변경 
                                            out hit, 20f, layerMask);

                if (isHit && hit.transform.CompareTag("Enemy"))
                {
                    Gizmos.color = Color.green;
                }
                else
                {
                    Gizmos.color = Color.red;
                }
                Gizmos.DrawRay(transform.position, EnemyList[i].transform.position - transform.position);//변경 
            }
        }
    }

    void Update()
    {
        SetTarget();
        AtkTarget();
        if(attackSpeedLv != PlayerData.Instance.playerSkill[6] && attackSpeedLv < PlayerData.Instance.playerSkill[6])
        {
            AttackSpeed();
            attackSpeedLv = PlayerData.Instance.playerSkill[6];
        }

        if (PlayerData.Instance.currentHp <= 0)
        {
            PlayerData.Instance.currentHp = 0;

            playerAnimator.SetBool("IDLE", false);
            playerAnimator.SetBool("MOVE", false);
            playerAnimator.SetBool("ATTACK", false);
            playerAnimator.SetBool("DIE", true);
        }
    }

    void AttackSpeed()
    {
        attackSpeed = 1 + PlayerData.Instance.playerSkill[6] * 0.5f;
        playerAnimator.SetFloat("ATTACKSPEED", attackSpeed);
    }

    void SetTarget()
    {
        if (EnemyList.Count != 0)
        {
            prevTargetIndex = TargetIndex;
            currentDist = 0f;
            closeDistIndex = 0;
            TargetIndex = -1;

            for (int i = 0; i < EnemyList.Count; i++)
            {
                if (EnemyList[i] == null)
                {
                    return;
                }

                currentDist = Vector3.Distance(transform.position, EnemyList[i].transform.position);//변경 

                RaycastHit hit;
                bool isHit = Physics.Raycast(transform.position, EnemyList[i].transform.position - transform.position,//변경 
                                            out hit, 20f, layerMask);

                if (isHit && hit.transform.CompareTag("Enemy"))
                {
                    if (TargetDist >= currentDist)
                    {
                        TargetIndex = i;

                        TargetDist = currentDist;

                        if (!MoveJoystick.Instance.isPlayerMoving && prevTargetIndex != TargetIndex)  // 추// 추가
                        {
                            TargetIndex = prevTargetIndex;
                        }
                    }
                }

                if (closetDist >= currentDist)
                {
                    closeDistIndex = i;
                    closetDist = currentDist;
                }
            }

            if (TargetIndex == -1)
            {
                TargetIndex = closeDistIndex;
            }
            closetDist = 100f;
            TargetDist = 100f;
            getATarget = true;
        }

    }

    void AtkTarget()
    {
        if (TargetIndex == -1 || EnemyList.Count == 0)  // 추가 
        {
            playerAnimator.SetBool("ATTACK", false);
            return;
        }
        if (getATarget && !MoveJoystick.Instance.isPlayerMoving && EnemyList.Count != 0)
        {
            //Debug.Log ( "lookat : " + EnemyList[TargetIndex].transform.GetChild ( 0 ) );  // 변경
            transform.LookAt(EnemyList[TargetIndex].transform);     // 변경

            if (playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("IDLE"))
            {
                playerAnimator.SetBool("IDLE", false);
                playerAnimator.SetBool("MOVE", false);
                playerAnimator.SetBool ("ATTACK", true );
            }
        }
        else if (MoveJoystick.Instance.isPlayerMoving)
        {
            if (!playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("MOVE"))
            {
                playerAnimator.SetBool("ATTACK", false);
                playerAnimator.SetBool("IDLE", false);
                playerAnimator.SetBool("MOVE", true);
            }
        }
        else
        {
            playerAnimator.SetBool("ATTACK", false);
            playerAnimator.SetBool("IDLE", true);
            playerAnimator.SetBool("MOVE", false);
        }
    }
}
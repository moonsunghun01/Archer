using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageMonster : MonoBehaviour
{
    private const string bulletTag = "Bullet";
    public Transform monsterTr;

    public GameObject hitEffect;
    public MoveHpBar hpBar;
    private Animator animator;

    private readonly int hashHit = Animator.StringToHash("Hit");
    private readonly int hashDie = Animator.StringToHash("Die");
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    { 
        if (other.CompareTag(bulletTag)) 
        {
            hpBar.currentHp = hpBar.currentHp - other.gameObject.GetComponent<ControlBullet>().damage ;
            Instantiate(hitEffect, monsterTr.position, monsterTr.rotation);
            
            if (hpBar.currentHp <= 0)
            {
                GetComponent<MonsterAI>().state = MonsterAI.State.DIE;
                this.gameObject.GetComponent<BoxCollider>().enabled = false;
                PlayerTargeting.Instance.EnemyList.Remove(this.gameObject);
                CheckRoom.Instance.EnemyListInRoom.Remove(this.gameObject);
                Destroy(gameObject, 2f);
                Destroy(hpBar, 1.5f);
                animator.SetTrigger(hashDie);

                Vector3 currentPosition = new Vector3(transform.position.x , 3, transform.position.z);
                // 코인 생성
                for(int i = 0; i<Random.Range(1,5);i++)
                {
                    GameObject coinClone = Instantiate(PlayerData.Instance.itemCoin, currentPosition, transform.rotation);
                    coinClone.transform.parent = gameObject.transform.parent.parent;
                } 
            }
        }
    }

    void Update()
    {
        
    }
}

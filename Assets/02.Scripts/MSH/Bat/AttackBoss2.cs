using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class AttackBoss2 : AttackMonster
{

    GameObject player;
    public GameObject monster;
    public LayerMask layerMask;
    public GameObject DangerMarker;
    
    public bool isMove;
    public float damage = 10;

    float nextAtk = 0;
    public override void Update()
    {
        if (isAttack && !isMove)
        {
            if (Time.time >= nextAttack)
            { 
                Attack();
                nextAttack = Time.time + attackRate;
            }
        } 
        else if(isMove)
        {
            moveBat();
        }

    }

    public override void Attack()
    { 
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(BatAttack());
    }

    IEnumerator BatAttack()
    {
        yield return null;

        yield return new WaitForSeconds(0.5f); 
        transform.LookAt(player.transform.position); 
        DangerMarkerShoot(); 
        yield return new WaitForSeconds(2f);
        isMove = true;
    }

    void DangerMarkerShoot()
    { 
        Vector3 NewPosition = new Vector3(transform.position.x, transform.position.y+1, transform.position.z);
        Physics.Raycast(NewPosition, transform.forward, out RaycastHit hit, 100f, layerMask);  
        if (hit.transform.CompareTag("Wall"))
        { 
            GameObject DangerMarkerClone = Instantiate(DangerMarker, NewPosition, transform.rotation);
            DangerMarkerClone.GetComponent<DangerLine>().EndPosition = hit.point; 
        }
    }

    void moveBat()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * 100); 
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag("Player") || other.transform.CompareTag("Wall"))
        {
            this.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            isAttack = false;
            isMove = false;
            if (other.transform.CompareTag("Player")) PlayerData.Instance.currentHp -= damage;
        }
        
    }
    private void OnCollisionExit(Collision other)
    {
        if (other.transform.CompareTag("Player") || other.transform.CompareTag("Wall"))
        {
            this.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        }
    }
}

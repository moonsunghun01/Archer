using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackMage : AttackMonster
{ 
    public GameObject obj;
    public Transform atkPos;
      
    public override void Attack()
    {
        animator.SetTrigger(hashAttack);
        GameObject _obj = Instantiate(obj, atkPos.position, atkPos.rotation);
        Destroy(_obj, 1); 
    }
}

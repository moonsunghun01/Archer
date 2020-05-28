using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTurtle : AttackMonster
{  
    public GameObject obj;
    public override void Attack()
    {
        animator.SetTrigger(hashAttack);
        //공격
        obj.SetActive(true);
    }
}

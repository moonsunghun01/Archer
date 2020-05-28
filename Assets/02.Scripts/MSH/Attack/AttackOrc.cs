using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackOrc : AttackMonster
{
    public GameObject obj;
    public override void Attack()
    {
        animator.SetTrigger(hashAttack);
        obj.SetActive(true);
    }
}

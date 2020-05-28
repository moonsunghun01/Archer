using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBat : AttackMonster
{
    public float damage = 10.0f;

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && !PlayerData.Instance.isAbs)
        {
            PlayerData.Instance.isAbs = true;
            PlayerData.Instance.currentHp -= damage; 
        }
    }
}

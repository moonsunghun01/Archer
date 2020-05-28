using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllAttackObj : MonoBehaviour
{
    public float damage = 100.0f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && !PlayerData.Instance.isAbs)
        {
            PlayerData.Instance.isAbs = true;
            PlayerData.Instance.currentHp -= damage;
            gameObject.SetActive(false);
        }
    }
}

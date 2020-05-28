using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllMeleeAtk : MonoBehaviour
{
    public float damage = 50;
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            PlayerData.Instance.currentHp -= damage;
        }
    } 
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllFireBall : MonoBehaviour
{
    public float damage = 50.0f;
    public float speed = 1.0f;
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") )
        {
            if (!PlayerData.Instance.isAbs)
            {
                PlayerData.Instance.isAbs = true;
                PlayerData.Instance.currentHp -= damage;
            }
            gameObject.SetActive(false); 
        }

        if (collision.gameObject.CompareTag("Wall") )
        {
            gameObject.SetActive(false);
        }
    }
}

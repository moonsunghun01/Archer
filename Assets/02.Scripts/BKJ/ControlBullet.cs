using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlBullet : MonoBehaviour
{
    public float damage = 1.0f;
    public float speed = 1.0f;
    public int bonceCount = 1;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        { 
            gameObject.SetActive(false);
        }
        if(collision.gameObject.CompareTag("Wall"))
        {
                gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        { 
            gameObject.SetActive(false);
        }
        if (other.CompareTag("Wall"))
        {
            //gameObject.SetActive(false);
            //Debug.Log("벽 충돌");
        }
    }

    private void OnEnable()
    {
        GetComponent<Rigidbody>().AddForce(transform.right * speed);
        bonceCount = 1;
        damage = damage + PlayerData.Instance.playerDamage * (PlayerData.Instance.playerSkill[4] + 1);
    }

    private void OnDisable()
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        bonceCount = 0;
        damage = 1.0f;
    }
}

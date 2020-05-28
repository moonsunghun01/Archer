using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBolt : MonoBehaviour
{
    Rigidbody rb;
    public float damage = 30;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * 10;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag("Wall"))
        {
            Destroy(gameObject,0.1f);
        }
        if (collision.transform.CompareTag("Player"))
        { 
            PlayerData.Instance.currentHp -= damage;
            Destroy(gameObject);
        }
    }
}

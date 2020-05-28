using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoulletController : MonoBehaviour
{
    public GameObject slotMachine;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            slotMachine.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameObject.SetActive(false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsVictory : MonoBehaviour
{
    public GameObject joystickUI;
    public GameObject victoryUI;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            joystickUI.SetActive(false); 
            victoryUI.SetActive(true);
        }
    }
}

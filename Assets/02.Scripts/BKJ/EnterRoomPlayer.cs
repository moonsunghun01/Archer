using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterRoomPlayer : MonoBehaviour
{
    public Collider[] colliders;
    public GameObject[] npc;
    public GameObject roulette;
    public int checkCount = 0;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);

        if(other.name == "Map1-1")
        {
            colliders[0].gameObject.SetActive(true);
            if (checkCount == 0)
            {
                roulette.SetActive(true);
                checkCount++;
            }
        }
        if (other.name == "Map1-2")
        {
            colliders[1].gameObject.SetActive(true);
            //npc[0].gameObject.SetActive(true);
        }
        if (other.name == "Map1-3")
        {
            colliders[2].gameObject.SetActive(true);
            //npc[1].gameObject.SetActive(true);
        }
        if (other.name == "Map1-4")
        {
            colliders[3].gameObject.SetActive(true);
        }
        if (other.name == "Map1-5")
        {
            colliders[4].gameObject.SetActive(true);
        }
        if (other.name == "Map1-6")
        {
            colliders[5].gameObject.SetActive(true);
        }
        if (other.name == "Map1-7")
        {
            colliders[6].gameObject.SetActive(true);
        }
        if (other.name == "Map1-8")
        {
            colliders[7].gameObject.SetActive(true);
        }
        if (other.name == "Map1-9")
        {
            colliders[8].gameObject.SetActive(true);
        }
        if (other.name == "Map1-10")
        {
            colliders[9].gameObject.SetActive(true);
        }
        if (other.name == "Map1-11")
        {
            colliders[10].gameObject.SetActive(true);
        }
        if (other.name == "Map1-12")
        {
            colliders[11].gameObject.SetActive(true);
        }
        if (other.name == "Map1-13")
        {
            colliders[12].gameObject.SetActive(true);
            UIManager.Instance.isBossRoom = true;
        }
    }
}

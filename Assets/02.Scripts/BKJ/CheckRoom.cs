using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckRoom : MonoBehaviour
{
    public List<GameObject> EnemyListInRoom = new List<GameObject>();

    public bool isPlayerInThisRoom = false;
    public bool isClearRoom = false;

    public GameObject gate;
    public GameObject npc;

    public static CheckRoom Instance // singlton     
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<CheckRoom>();
                if (instance == null)
                {
                    var instanceContainer = new GameObject("CheckRoom");
                    instance = instanceContainer.AddComponent<CheckRoom>();
                }
            }
            return instance;
        }
    }
    private static CheckRoom instance;

    void Update()
    {
        if(isPlayerInThisRoom)
        {
            if(EnemyListInRoom.Count <= 0 && !isClearRoom)
            {
                isClearRoom = true;
            }
            for (int i = 0; i < EnemyListInRoom.Count; i++)
            {
                if (EnemyListInRoom[i] == null)
                {
                    EnemyListInRoom.RemoveAt(i);
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (isClearRoom && gate != null)
        {
            if(gate.transform.localPosition.y <= 3.0f)
            {
                gate.transform.Translate(0, 0.1f, 0);
            }
        }
        if(isClearRoom && npc != null)
        {
            if (npc.transform.localPosition.y >= 2.0f)
            {
                npc.transform.Translate(0, -1.0f, 0);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInThisRoom = true;
        }

        if (other.CompareTag("Enemy"))
        {
            EnemyListInRoom.Add(other.gameObject);
            PlayerTargeting.Instance.EnemyList = new List<GameObject>(EnemyListInRoom);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            isPlayerInThisRoom = false;
        }
        if(other.CompareTag("Enemy"))
        {
            EnemyListInRoom.Remove(other.gameObject);
        }
    }
}

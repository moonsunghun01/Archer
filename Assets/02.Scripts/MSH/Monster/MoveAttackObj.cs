using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAttackObj : MonoBehaviour
{
    public Transform monsterTr;
    private Transform attackObjTr;

    private void Start()
    {
        attackObjTr = GetComponent<Transform>(); 
    }
     
    void Update()
    {
        attackObjTr.transform.position = monsterTr.transform.position;
    }
}

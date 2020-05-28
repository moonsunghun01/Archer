using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveMonster : MonoBehaviour
{ 
    private NavMeshAgent agent;
     
    public float traceSpeed = 2; 
     
    private Vector3 _traceTarget;
    public Vector3 traceTarget
    {
        get { return _traceTarget; }
        set
        {
            _traceTarget = value;
            agent.speed = traceSpeed;
            TraceTarget(_traceTarget);
        }
    }
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;
        
    } 

    void TraceTarget(Vector3 pos)
    {
        if (agent.isPathStale) return;
        agent.destination = pos; 
        agent.isStopped = false;
    }

    public void Stop()
    {
        agent.isStopped = true;
        agent.velocity = Vector3.zero; 
    }  
}

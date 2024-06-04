using System;
using UnityEngine;
using UnityEngine.AI;


public class TestNavMesh : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform target;
    
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        agent.SetDestination(target.position);
    }
}
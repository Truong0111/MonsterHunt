using System;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    public Collider[] barriers;
    public Collider[] barriersToOpens;
    private void Awake()
    {
        foreach (var barrier in barriers)
        {
            barrier.isTrigger = false;
        }
        
        foreach (var barrier in barriersToOpens)
        {
            barrier.isTrigger = false;
        }
    }

    public void OpenBarrier(bool isOpen)
    {
        foreach (var barrier in barriersToOpens)
        {
            barrier.gameObject.SetActive(isOpen);
        }
    }
}
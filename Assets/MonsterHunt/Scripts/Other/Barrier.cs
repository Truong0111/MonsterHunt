using System;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    public Collider[] boxBarriers;

    private void Awake()
    {
        foreach (var barrier in boxBarriers)
        {
            barrier.isTrigger = false;
        }
    }

    public void OpenBarrier(bool isOpen)
    {
        foreach (var barrier in boxBarriers)
        {
            barrier.isTrigger = isOpen;
        }
    }
}
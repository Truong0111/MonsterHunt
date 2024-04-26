using System;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    public Transform playerCameraTransform;
    public Transform playerTransform;

    public LayerMask maskRaycast;
    
    private void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == maskRaycast)
        {
            Collect();
        }
    }

    private void Collect()
    {
        //TODO: Collect object
    }
}
using UnityEngine;
using UnityEngine.Serialization;

public class LookAtCamera : MonoBehaviour
{
    public Transform cameraTransform;
    
    private void Start()
    {
        var cameraMain = Camera.main;

        if (cameraTransform != null) return;
        
        if (cameraMain != null)
        {
            cameraTransform = cameraMain.transform;
        }
        else
        {
            Debug.LogError("No camera can get in scene");
            enabled = false;
        }
    }

    private void Update()
    {
        transform.forward = cameraTransform.forward;
    }
}
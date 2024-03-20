using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    public Transform camera;
    
    private void Start()
    {
        var cameraMain = Camera.main;

        if (camera == null)
        {
            if (cameraMain != null)
            {
                camera = cameraMain.transform;
            }
            else
            {
                Debug.LogError("No camera can get in scene");
                enabled = false;
            }
        }
    }

    private void Update()
    {
        transform.forward = camera.forward;
    }
}
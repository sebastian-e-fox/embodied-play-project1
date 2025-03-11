using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    private Camera cam;
    public float zoomInFOV = 10f;  // Target FOV when zooming in
    public float zoomOutFOV = 60f; // Default FOV when zooming out
    public float zoomInSpeed = 10f; // Slower zoom-in
    public float zoomOutSpeed = 20f; // Faster zoom-out

    private bool shouldZoomIn = false;
    private bool isZooming = false;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    public void SetZoomState(bool zoomIn)
    {
        if (zoomIn != shouldZoomIn) // Only start zoom if state changes
        {
            shouldZoomIn = zoomIn;
            if (!isZooming) StartCoroutine(Zoom()); 
        }
    }

    private System.Collections.IEnumerator Zoom()
    {
        isZooming = true;
        float targetFOV = shouldZoomIn ? zoomInFOV : zoomOutFOV;
        float speed = shouldZoomIn ? zoomInSpeed : zoomOutSpeed;

        while (!Mathf.Approximately(cam.fieldOfView, targetFOV))
        {
            cam.fieldOfView = Mathf.MoveTowards(cam.fieldOfView, targetFOV, speed * Time.deltaTime);
            yield return null;
        }

        isZooming = false;
    }
}
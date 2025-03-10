using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LipsDetection : MonoBehaviour
{
    public Transform playerCamera;
    public float dotThreshold = 0.98f;
    public float maxDistance = 5f;

    public bool IsLookingAtObject;

    public ShowPanel panel;

    void FixedUpdate()
    {
        if (IsLookingAtObject)
        {
            FindObjectOfType<ShowPanel>().SetFadeState(true);  // To fade in
            FindObjectOfType<CameraZoom>().SetZoomState(true);  // Zoom in

        }
        else
        {
            FindObjectOfType<ShowPanel>().SetFadeState(false); // To fade out
            FindObjectOfType<CameraZoom>().SetZoomState(false); // Zoom out
        }

            Vector3 directionToTarget = (transform.position - playerCamera.position);
        float distanceToTarget = directionToTarget.magnitude;

        if (distanceToTarget <= maxDistance) // Check if within distance
        {
            directionToTarget.Normalize();
            float dotProduct = Vector3.Dot(playerCamera.forward, directionToTarget);

            IsLookingAtObject = dotProduct >= dotThreshold;
        }
        else
        {
            IsLookingAtObject = false;
        }
    }
}

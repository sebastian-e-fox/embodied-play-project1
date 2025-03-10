using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LipsDetection : MonoBehaviour
{
    public Transform playerCamera;
    public float dotThreshold = 0.98f;
    public float maxDistance = 5f;

    public bool IsLookingAtObject;

    void Update()
    {
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

    // slowly slide opacity up until 150 when looking at target

    // slowly slide opacity down to 0 / hide gameobject when not

}

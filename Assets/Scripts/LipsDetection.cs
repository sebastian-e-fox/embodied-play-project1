using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;


namespace Tobii.Gaming.SimpleGazeSelection
{
    public class LipsDetection : MonoBehaviour
    {
        public Transform playerCamera;

        //public float dotThreshold = 0.98f;
        //public float maxDistance = 5f;

        public bool isLookingAtObject;

        public GazeAware _gazeAwareComponent;
        private void Start()
        {
            _gazeAwareComponent = GetComponent<GazeAware>();
            if (_gazeAwareComponent == null)
            {
                Debug.LogError("GazeAware component is missing from this object!", this);
            }
            if (TobiiAPI.IsConnected == false)
            {
                Debug.LogError("Tobii Eye Tracker is not connected!");
            }
        }
        void Update()
        {
            if (_gazeAwareComponent.HasGazeFocus) // Gaze Detection
            {
                isLookingAtObject = true;
            }
            else
            {
                isLookingAtObject = false;
            }

            //Vector3 directionToTarget = (transform.position - playerCamera.position);
            //float distanceToTarget = directionToTarget.magnitude;

            //if (distanceToTarget <= maxDistance) // Check if within distance
            //{
            //    isLookingAtObject = true;
            //}
            //else
            //{
            //    isLookingAtObject = false;
            //}
        }
    }
}
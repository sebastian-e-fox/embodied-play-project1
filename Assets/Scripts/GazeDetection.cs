using System.Collections;
using UnityEngine;
using Tobii.Gaming;

namespace Tobii.Gaming.SimpleGazeSelection
{
    public class GazeDetection : MonoBehaviour
    {
        public Transform playerCamera;
        public float maxDistance = 20f;
        public float gazeTimeRequired = 2f; // Time required to turn fully red and start chasing
        public float chaseSpeed = 5f; // Speed at which the object chases the player

        private bool isLookingAtObject;
        private Renderer _renderer;
        private GazeAware _gazeAwareComponent;
        private float gazeTime = 0f; // Timer for how long the player has looked at the object
        public bool isChasing = false;
        private float distanceToTarget;

        private void Start()
        {
            _gazeAwareComponent = GetComponent<GazeAware>();
            _renderer = GetComponent<Renderer>();

            // Debugging Checks
            if (_gazeAwareComponent == null)
            {
                Debug.LogError("GazeAware component is missing from this object!", this);
            }

            if (TobiiAPI.IsConnected == false)
            {
                Debug.LogError("Tobii Eye Tracker is not connected!");
            }

            if (_renderer == null)
            {
                Debug.LogError("Renderer component is missing!", this);
            }
        }

        void Update()
        {

            Vector3 directionToTarget = (transform.position - playerCamera.position);
           distanceToTarget = directionToTarget.magnitude;

            if (isChasing) // If already chasing, follow the player
            {
                ChasePlayer();
                return;
            }

            if (distanceToTarget <= maxDistance) // Check if within detection range
            {
                if (_gazeAwareComponent.HasGazeFocus) // Gaze Detection
                {
                    isLookingAtObject = true;
                    gazeTime += Time.deltaTime; // Increment the gaze timer

                    // Calculate color intensity based on how long the player is looking
                    float t = Mathf.Clamp01(gazeTime / gazeTimeRequired);
                    _renderer.material.color = Color.Lerp(Color.white, Color.red, t);

                    // If player has looked long enough, trigger chase mode
                    if (gazeTime >= gazeTimeRequired)
                    {
                        isChasing = true;
                    }
                }
                else // Player looked away before timer finished
                {
                    isLookingAtObject = false;
                    gazeTime = 0f; // Reset gaze timer
                    _renderer.material.color = Color.white; // Reset color
                }
            }
            else // Out of range
            {
                isLookingAtObject = false;
                gazeTime = 0f; // Reset gaze timer
                _renderer.material.color = Color.white; // Reset color
            }
        }

        private void ChasePlayer()
        {
            if (distanceToTarget < 30f)
            {
                transform.position = Vector3.MoveTowards(transform.position, playerCamera.position, chaseSpeed * Time.deltaTime);
                _renderer.material.color = Color.red; // Fully red while chasing
            }
            else
            {
                // Gradually fade back to white over time
                _renderer.material.color = Color.Lerp(_renderer.material.color, Color.white, Time.deltaTime * 0.75f);

                // If the color is close to white, stop chasing
                if (_renderer.material.color.r > 0.95f && _renderer.material.color.g > 0.95f && _renderer.material.color.b > 0.95f)
                {
                    isChasing = false;
                }
            }
        }
    }
}
using UnityEngine;
using UnityEngine.UI;
using Tobii.Gaming;
using System.Runtime.CompilerServices;

namespace Tobii.Gaming.SimpleGazeSelection
{
    public class GazeDetection : MonoBehaviour
    {
        public Transform playerCamera;
        public float maxDistance = 20f;
        public float gazeTimeRequired = 2f; // Time required to fill the bar and trigger chase
        public float chaseSpeed = 5f; // Speed at which the object chases the player

        private BoxCollider _boxCollider;
        private GazeAware _gazeAwareComponent;
        private float gazeTime = 0f;
        private float distanceToTarget;
        public bool isChasing = false;

        public GameObject ExclamationCanvas; // The entire exclamation UI
        public RectTransform fillRectTransform; // The red fill bar RectTransform

        private float endY = 0;
        private float startY = -0.8f;

        private void Start()
        {
            _gazeAwareComponent = GetComponent<GazeAware>();

            _boxCollider = GetComponent<BoxCollider>();
            _boxCollider.enabled = false; //disables NPC's collisions detection

            //CONSOLE MESSAGES FOR MEASURE
            if (_gazeAwareComponent == null)
            {
                Debug.LogError("GazeAware component is missing!", this);
            }

            if (TobiiAPI.IsConnected == false)
            {
                Debug.LogError("Tobii Eye Tracker is not connected!");
            }

            if (ExclamationCanvas == null || fillRectTransform == null)
            {
                Debug.LogError("Assign all UI elements (ExclamationCanvas, backgroundImage, fillRectTransform)!", this);
            }

            ExclamationCanvas.SetActive(false);
            ResetFill();
        }

        void Update()
        {
            distanceToTarget = Vector3.Distance(transform.position, playerCamera.position);

            if (isChasing)
            {
                ChasePlayer();
                return;
            }

            if (distanceToTarget <= maxDistance)
            {
                if (_gazeAwareComponent.HasGazeFocus)
                {
                    ExclamationCanvas.SetActive(true);

                    gazeTime += Time.deltaTime;
                    float t = Mathf.Clamp01(gazeTime / gazeTimeRequired);

                    // Move the red fill rectangle gradually from startY to endY
                    Vector2 anchoredPos = fillRectTransform.anchoredPosition;
                    anchoredPos.y = Mathf.Lerp(startY, endY, t);
                    fillRectTransform.anchoredPosition = anchoredPos;

                    if (gazeTime >= gazeTimeRequired)
                    {
                        isChasing = true;
                    }
                }
                else
                {
                    ResetDetection();
                }
            }
            else
            {
                ResetDetection();
            }
        }

        private void ResetDetection()
        {
            gazeTime = 0f;
            ResetFill();
            ExclamationCanvas.SetActive(false);
        }

        private void ResetFill()
        {
            Vector2 anchoredPos = fillRectTransform.anchoredPosition;
            anchoredPos.y = startY;
            fillRectTransform.anchoredPosition = anchoredPos;
        }

        private void ChasePlayer()
        {
            if (distanceToTarget < 30f)
            {
                transform.position = Vector3.MoveTowards(transform.position, playerCamera.position, chaseSpeed * Time.deltaTime);

                // Fully fill the exclamation
                Vector2 anchoredPos = fillRectTransform.anchoredPosition;
                anchoredPos.y = endY;
                fillRectTransform.anchoredPosition = anchoredPos;

                _boxCollider.enabled = true; //enables the enemy "catching you"
            }
            else
            {
                Vector2 anchoredPos = fillRectTransform.anchoredPosition;
                anchoredPos.y = Mathf.Lerp(anchoredPos.y, startY, Time.deltaTime * 0.75f);
                fillRectTransform.anchoredPosition = anchoredPos;

                if (Mathf.Abs(anchoredPos.y - startY) < 0.01f)
                {
                    isChasing = false;
                    ExclamationCanvas.SetActive(false);
                    _boxCollider.enabled = false; //disables the enemy "catching you"
                }
            }
        }
    }
}

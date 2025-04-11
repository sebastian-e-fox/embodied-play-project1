using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private MoveSettings _settings = null;
    [SerializeField] private HeadBobController _headBobController;
    
    [Header("Footsteps")]
    public AudioClip [] footstepsSound;
    public AudioSource sprintSound;
    public AudioSource footstepsSource;
    public string currentSurfaceTag = "Concrete";
    public string lastSurfaceTag = "Concrete";

    private Vector3 _moveDirection;
    private CharacterController _controller;

    [Header("Stamina Settings")]
    public float maxStamina = 100f;
    public float staminaDrainRate = 10f; // Stamina loss per second when moving
    public float staminaRegenRate = 5f; // Stamina gain per second when idle
    public float sprintMultiplier = 1.5f; // Move speed multiplier when sprinting
    private float currentStamina;
    private bool isSprinting;

    [Header("UI")]
    public Slider staminaBar; // Assign in Inspector

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        currentStamina = maxStamina;

        if (staminaBar != null)
        {
            staminaBar.maxValue = maxStamina;
            staminaBar.value = currentStamina;
        }
    }

    private void Update()
    {
        HandleSprint();
        DefaultMovement();
        UpdateStaminaUI();
        UpdateFootstepSound();

        if (_headBobController != null)
        {
            _headBobController.IsSprinting = isSprinting;
        }

        if (isSprinting && currentStamina <= 60)
        {
            sprintSound.enabled = true;
        }
        else
        {
            sprintSound.enabled = false;
        }
    }


    private void FixedUpdate()
    {
        _controller.Move(_moveDirection * Time.deltaTime);
    }

    private void DefaultMovement()
    {
        if (_controller.isGrounded)
        {
            Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

            if (input.x != 0 && input.y != 0)
            {
                input *= 0.777f;
            }

            float speed = _settings.speed;

            if (isSprinting && currentStamina > 0)
            {
                speed *= sprintMultiplier;
                currentStamina -= staminaDrainRate * Time.deltaTime;
                currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);
            }
            else if (!isSprinting && currentStamina < maxStamina)
            {
                currentStamina += staminaRegenRate * Time.deltaTime;
                currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);
            }

            // Disable sprinting when out of stamina
            if (currentStamina <= 0)
            {
                isSprinting = false;
            }

            _moveDirection.x = input.x * speed;
            _moveDirection.z = input.y * speed;
            _moveDirection.y = -_settings.antiBump;

            _moveDirection = transform.TransformDirection(_moveDirection);
            
            // FOOTSTEPS
            if (input.x != 0 || input.y != 0)
            {
                Debug.Log("Movement!");
                footstepsSource.enabled = true;

                if (currentSurfaceTag != lastSurfaceTag)
                {
                    // Update clip based on new surface
                    if (currentSurfaceTag == "Snow")
                    {
                        footstepsSource.clip = footstepsSound[0];
                    }
                    else if (currentSurfaceTag == "Concrete")
                    {
                        footstepsSource.clip = footstepsSound[1];
                    }

                    footstepsSource.Stop(); // Force restart
                    footstepsSource.Play();

                    lastSurfaceTag = currentSurfaceTag;
                }

                if (isSprinting)
                {
                    footstepsSource.pitch = 1.9f;
                }
                else
                {
                    footstepsSource.pitch = 0.7f;
                }
            }
            else
            {
                footstepsSource.enabled = false;

            }
        }
        else
        {
            _moveDirection.y -= _settings.gravity * Time.deltaTime;
        }
    }

    private void HandleSprint()
    {
        // Enable sprinting if Left Shift is held and stamina is available
        isSprinting = Input.GetKey(KeyCode.LeftShift) && currentStamina > 0f;
    }

    //private void Jump()
    //{
    //    _moveDirection.y += _settings.jumpForce;
    //}

    private void UpdateFootstepSound()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 2f))
        {
            if (hit.collider.CompareTag("Snow"))
            {
                currentSurfaceTag = "Snow";
            }
            else if (hit.collider.CompareTag("Concrete"))
            {
                currentSurfaceTag = "Concrete";
            }
        }
    }

    private void UpdateStaminaUI()
    {
        if (staminaBar != null)
        {
            staminaBar.value = currentStamina;
        }
    }
}

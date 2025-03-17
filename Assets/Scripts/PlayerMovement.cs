using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public Transform orientation;
    public Rigidbody rb;

    public float horizInput;
    public float vertInput;
    private Vector3 moveDirection;

    [Header("Stamina Settings")]
    public float maxStamina = 100f;
    public float staminaDrainRate = 10f; // Stamina loss per second when moving
    public float staminaRegenRate = 5f; // Stamina gain per second when idle
    public float sprintMultiplier = 1.5f; // Move speed multiplier when sprinting
    private float currentStamina;
    private bool isSprinting;

    [Header("UI")]
    public Slider staminaBar; // Assign in Inspector

    private void Start()
    {
        rb.GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        currentStamina = maxStamina;
        staminaBar.maxValue = maxStamina;
        staminaBar.value = currentStamina;
    }

    private void Update()
    {
        MyInput();
        HandleStamina();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizInput = Input.GetAxisRaw("Horizontal");
        vertInput = Input.GetAxisRaw("Vertical");

        isSprinting = Input.GetKey(KeyCode.LeftShift) && currentStamina > 0;
    }

    private void MovePlayer()
    {
        float speedMultiplier = isSprinting ? sprintMultiplier : 1f;
        moveDirection = (orientation.forward * vertInput + orientation.right * horizInput).normalized;

        rb.AddForce(moveDirection * moveSpeed * speedMultiplier * 10f, ForceMode.Force);
    }

    private void HandleStamina()
    {
        if (isSprinting && (horizInput != 0 || vertInput != 0))
        {
            currentStamina -= staminaDrainRate * Time.deltaTime;
        }
        else
        {
            currentStamina += staminaRegenRate * Time.deltaTime;
        }

        currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);
        staminaBar.value = currentStamina;
    }
}

using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FirstPersonMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float runSpeed = 7f;

    [Header("Stamina")]
    public bool canRun = true;
    [SerializeField] private float maxStamina = 5f;
    [SerializeField] private float staminaDrainPerSecond = 1f;
    [SerializeField] private float staminaRegenPerSecond = 0.75f;
    [SerializeField] private float staminaRequiredToRun = 0.5f;
    [SerializeField] private float startingStamina = -1f;

    private float staminaRegenDelay = 3f;

    [Header("Controls")]
    public KeyCode runningKey = KeyCode.LeftShift;

    public bool IsRunning { get; private set; }
    public float currentStamina;

    private Rigidbody rb;
    private Vector2 moveInput;
    private float currentRegenDelayTimer;

    public List<System.Func<float>> speedOverrides = new List<System.Func<float>>();

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        currentStamina = (startingStamina < 0f) ? maxStamina : Mathf.Clamp(startingStamina, 0f, maxStamina);
    }

    void Update()
    {

        HandleInputAndStamina();
    }

    void FixedUpdate()
    {
        ApplyMovement();
    }

    private void HandleInputAndStamina()
    {
        moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        bool isMoving = moveInput.sqrMagnitude > 0.01f;
        bool inputRun = Input.GetKey(runningKey);

        if (IsRunning)
        {
            if (!inputRun || currentStamina <= 0 || !isMoving)
            {
                IsRunning = false;
            }
        }
        else
        {
            if (canRun && inputRun && currentStamina >= staminaRequiredToRun && isMoving)
            {
                IsRunning = true;
            }
        }

        if (IsRunning)
        {
            currentStamina -= staminaDrainPerSecond * Time.deltaTime;
            currentRegenDelayTimer = staminaRegenDelay; 
        }
        else
        {
            if (currentRegenDelayTimer > 0)
            {
                currentRegenDelayTimer -= Time.deltaTime;
            }
            else
            {
                currentStamina += staminaRegenPerSecond * Time.deltaTime;
            }
        }
        currentStamina = Mathf.Clamp(currentStamina, 0f, maxStamina);
    }

    private void ApplyMovement()
    {
        float targetSpeed = IsRunning ? runSpeed : walkSpeed;

        if (speedOverrides.Count > 0)
        {
            targetSpeed = speedOverrides[speedOverrides.Count - 1]();
        }
        Vector3 moveDirection = (transform.right * moveInput.x + transform.forward * moveInput.y).normalized;
        Vector3 targetVelocity = moveDirection * targetSpeed;

        rb.linearVelocity = new Vector3(targetVelocity.x, rb.linearVelocity.y, targetVelocity.z);
    }
}
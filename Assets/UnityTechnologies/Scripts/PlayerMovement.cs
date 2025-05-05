using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [Header("Velocidade")]
    public float turnSpeed = 20f;
    public float walkSpeedMultiplier = 1f;
    public float runSpeedMultiplier = 2f;

    [Header("Stamina")]
    public Slider staminaSlider;
    public float maxStamina = 5f;
    public float staminaRegenRate = 1f;
    public float staminaDrainRate = 1f;


    [Header("Vida")]
    public Slider healthSlider;
    public float maxHealth = 100f;
    public float currentHealth;


    private float currentStamina;
    private bool isRunning;


    [Header("Handler")]
    public StaminaHandler sliderHandler;
    public GameEnding gameEnding;
    Animator m_Animator;
    Rigidbody m_Rigidbody;
    AudioSource m_AudioSource;
    Vector3 m_Movement;
    Quaternion m_Rotation = Quaternion.identity;

    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
        m_AudioSource = GetComponent<AudioSource>();

        currentStamina = 0.5f;

        if (staminaSlider != null)
        {
            staminaSlider.maxValue = maxStamina;
            staminaSlider.value = staminaSlider.value;
        }

        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }
    }

    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        m_Movement.Set(horizontal, 0f, vertical);
        bool hasMovementInput = m_Movement.sqrMagnitude > 0.01f;
        m_Movement.Normalize();

        isRunning = Input.GetKey(KeyCode.Space) && currentStamina > 0f && hasMovementInput;

        if (isRunning)
        {
            currentStamina -= staminaDrainRate * Time.fixedDeltaTime;
            currentStamina = Mathf.Max(currentStamina, 0f);
        }
        else if (!hasMovementInput)
        {
            currentStamina += staminaRegenRate * Time.fixedDeltaTime;
            currentStamina = Mathf.Min(currentStamina, maxStamina);
        }

        if (staminaSlider != null)
        {
            staminaSlider.value = currentStamina;
        }

        float speedMultiplier = isRunning ? runSpeedMultiplier : walkSpeedMultiplier;

        m_Animator.SetBool("IsWalking", hasMovementInput);

        if (hasMovementInput)
        {
            if (!m_AudioSource.isPlaying)
            {
                m_AudioSource.Play();
            }
        }
        else
        {
            m_AudioSource.Stop();
        }

        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);
        m_Rotation = Quaternion.LookRotation(desiredForward);

        m_Movement *= speedMultiplier;
    }

    void OnAnimatorMove()
    {
        m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement * m_Animator.deltaPosition.magnitude);
        m_Rigidbody.MoveRotation(m_Rotation);
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Max(currentHealth, 0f);
        Debug.Log("Player health: " + currentHealth);

        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }

        if (currentHealth <= 0f)
        {
            gameEnding.CaughtPlayer();
        }
    }
}

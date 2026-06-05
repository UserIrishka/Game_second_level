using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 5f;
    public float jumpForce = 7f;

    [Header("Detection Settings")]
    public Transform groundCheck;
    public float checkRadius = 0.2f;
    public LayerMask whatIsGround;

    [Header("Stats")]
    public int health = 100;
    public int keys = 0;

    private Rigidbody2D rb;
    private Animator animator;        // <-- Добавлен Animator
    private bool isGrounded;
    private float moveInput;

    private HealthManager healthManager;
    private KeyManager keyManager;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();   // <-- Получаем компонент аниматора
        healthManager = Object.FindAnyObjectByType<HealthManager>();
        keyManager = Object.FindAnyObjectByType<KeyManager>();

        if (healthManager) healthManager.UpdateUI(health);
        if (keyManager) keyManager.UpdateUI(keys);
    }

    public void OnMove(InputValue value) => moveInput = value.Get<Vector2>().x;

    public void OnJump()
    {
        if (isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            // Опционально: триггер для анимации прыжка
            if (animator != null) animator.SetTrigger("Jump");
        }
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        UpdateAnimator(); // <-- Обновляем параметры аниматора каждый кадр
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y);
        Flip();
    }

    // Обновление параметров для Animator
    void UpdateAnimator()
    {
        if (animator == null) return;

        // Скорость по горизонтали (модуль для бега в любую сторону)
        float horizontalSpeed = Mathf.Abs(rb.linearVelocity.x);
        animator.SetFloat("Speed", horizontalSpeed);

        // На земле или в воздухе
        animator.SetBool("IsGrounded", isGrounded);

        // Вертикальная скорость (для анимаций прыжка/падения)
        animator.SetFloat("VerticalVelocity", rb.linearVelocity.y);
    }

    private void Flip()
    {
        if (moveInput > 0)
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        else if (moveInput < 0)
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            health -= 10;
            if (healthManager) healthManager.UpdateUI(health);
            if (health <= 0) healthManager.Die();
        }

        if (collision.CompareTag("Key"))
        {
            keys++;
            if (keyManager) keyManager.UpdateUI(keys);
            Destroy(collision.gameObject);
        }
    }
}
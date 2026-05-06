using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 5f;
    public float jumpForce = 10f;

    [Header("Detection Settings")]
    public Transform groundCheck;
    public float checkRadius = 0.2f;
    public LayerMask whatIsGround;

    [Header("Stats")]
    public int health = 100;
    public int keys = 0;

    private Rigidbody2D rb;
    private bool isGrounded;
    private float moveInput;

    // Ссылки на менеджеры для обновления интерфейса
    private HealthManager healthManager;
    private KeyManager keyManager;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        healthManager = Object.FindAnyObjectByType<HealthManager>();
        keyManager = Object.FindAnyObjectByType<KeyManager>();

        // Синхронизируем UI при старте
        if (healthManager) healthManager.UpdateUI(health);
        if (keyManager) keyManager.UpdateUI(keys);
    }

    public void OnMove(InputValue value) => moveInput = value.Get<Vector2>().x;

    public void OnJump()
    {
        if (isGrounded) rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }

    void Update() => isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

    void FixedUpdate() => rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y);

    // Центр управления событиями
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Получение урона
        if (collision.CompareTag("Enemy"))
        {
            health -= 10;
            if (healthManager) healthManager.UpdateUI(health);
            if (health <= 0) healthManager.Die();
        }

        // Сбор ключей
        if (collision.CompareTag("Key"))
        {
            keys++;
            if (keyManager) keyManager.UpdateUI(keys);
            Destroy(collision.gameObject); // Удаляем ключ
        }
    }
}
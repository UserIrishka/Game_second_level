using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Движение")]
    public float speed = 5f;
    public float jumpForce = 7f;
    private bool hasDoubleJump = false;

    [Header("Стрельба")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 0.3f;

    [Header("Проверка земли")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.1f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private bool isGrounded = false;
    private float nextFireTime = 0f;
    private float facingDirection = 1f; // 1 = вправо, -1 = влево
    private Animator animator;
    private float baseScaleX;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        baseScaleX = Mathf.Abs(transform.localScale.x);
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        HandleMovement();
        HandleJump();
        HandleShoot();
        UpdateAnimations();
    }

    void HandleMovement()
    {
        float moveX = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(moveX * speed, rb.linearVelocity.y);

        if (moveX != 0f)
        {
            facingDirection = moveX > 0 ? 1f : -1f;
            transform.localScale = new Vector3(
                facingDirection * baseScaleX,
                transform.localScale.y,
                transform.localScale.z
            );
        }
    }

    void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                hasDoubleJump = true; // разрешаем двойной прыжок
            }
            else if (hasDoubleJump)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce * 1.2f);
                hasDoubleJump = false; // использован, больше нельзя
            }
        }

        // Сбрасываем при приземлении
        if (isGrounded)
            hasDoubleJump = true;
    }

    void HandleShoot()
    {
        bool firePressed = Input.GetKeyDown(KeyCode.F) || Input.GetMouseButtonDown(1);

        if (firePressed && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        if (bulletPrefab == null || firePoint == null)
        {
            if (bulletPrefab == null) Debug.LogWarning("Bullet prefab не назначен!");
            if (firePoint == null) Debug.LogWarning("Fire point не назначен!");
            return;
        }


        GameObject bulletObj = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Bullet bullet = bulletObj.GetComponent<Bullet>();
        if (bullet != null)
            bullet.direction = facingDirection;
    }

    void UpdateAnimations()
    {
        if (animator == null) return;

        // Получаем горизонтальное движение
        float moveX = Mathf.Abs(Input.GetAxis("Horizontal"));

        // Передаём параметры в Animator
        animator.SetFloat("Speed", moveX);
        animator.SetBool("IsGrounded", isGrounded);
        animator.SetFloat("VelocityY", rb.linearVelocity.y);
    }
}
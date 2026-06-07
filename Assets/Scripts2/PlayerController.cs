using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{       
    [SerializeField] private AudioSource gameOverMusicSource;
    [SerializeField] private GameObject gameOverImage;

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

    [Header("Settings Стрельбы")]
    public GameObject bulletPrefab; 
    public Transform firePoint;   
    public float fireRate = 0.3f;   
    private float nextFireTime = 0f;
    private float facingDirection = 1f;

    private Rigidbody2D rb;
    private Animator animator;        
    private bool isGrounded;
    private float moveInput;

    private HealthManager healthManager;
    private KeyManager keyManager;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();   
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
            if (animator != null) animator.SetTrigger("Jump");
        }
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        UpdateAnimator();
        HandleShoot();
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y);
        Flip();
    }

    void UpdateAnimator()
    {
        if (animator == null) return;

        float horizontalSpeed = Mathf.Abs(rb.linearVelocity.x);
        animator.SetFloat("Speed", horizontalSpeed);

        animator.SetBool("IsGrounded", isGrounded);

        animator.SetFloat("VerticalVelocity", rb.linearVelocity.y);
    }


    private void Flip()
    {
        if (moveInput > 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            facingDirection = 1f;
        }
        else if (moveInput < 0)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            facingDirection = -1f;
        }
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

    void HandleShoot()
    {
        bool firePressed = Input.GetKeyDown(KeyCode.F) || Input.GetMouseButtonDown(0);

        if (firePressed && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        if (bulletPrefab == null || firePoint == null) return;

        GameObject bulletObj = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        LightningBullet bullet = bulletObj.GetComponent<LightningBullet>();

        if (bullet != null)
        {
            bullet.direction = facingDirection;
        }
    }

    private AudioSource[] allAudioSources;
    private void ShowGameOver()
    {
        // Отображаем Game Over Image
        if (gameOverImage != null)
        {
            gameOverImage.SetActive(true);
        }

        allAudioSources = UnityEngine.Object.FindObjectsByType<AudioSource>(UnityEngine.FindObjectsSortMode.None);

        // Отключаем все звуки кроме музыки GameOver
        foreach (AudioSource source in allAudioSources)
        {
            if (source != gameOverMusicSource)
            {
                source.Pause();
            }
        }

        // Запускаем музыку GameOver
        if (gameOverMusicSource != null)
        {
            gameOverMusicSource.Play();
        }


        // Остановка игры
        Time.timeScale = 0;
    }



    public void ReturnToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }
}
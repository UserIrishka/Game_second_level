using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    [Header("«доровье")]
    public int maxHealth = 5;
    public int currentHealth;

    [Header("UI Ч сердечки")]
    public HeartHealthBar heartHealthBar;

    [Header("Ёффекты")]
    public float invincibilityTime = 1.5f;

    private bool isInvincible = false;
    private SpriteRenderer spriteRenderer;
    private PlayerMovement movement;
    private Rigidbody2D rb;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        movement = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody2D>();

        currentHealth = maxHealth;

        if (heartHealthBar == null)
            heartHealthBar = FindObjectOfType<HeartHealthBar>();

        UpdateHealthBar();
    }

    public void TakeDamage(int damage)
    {
        if (isInvincible) return;

        currentHealth = Mathf.Max(0, currentHealth - damage);
        UpdateHealthBar();
        StartCoroutine(InvincibilityRoutine());

        if (currentHealth <= 0)
            StartCoroutine(DieAndRestartLevel());
    }

    public void Heal(int amount)
    {
        currentHealth = Mathf.Min(maxHealth, currentHealth + amount);
        UpdateHealthBar();
    }

    void UpdateHealthBar()
    {
        if (heartHealthBar != null)
            heartHealthBar.UpdateHearts(currentHealth);
    }

    IEnumerator InvincibilityRoutine()
    {
        isInvincible = true;

        if (spriteRenderer != null)
        {
            float elapsed = 0f;
            while (elapsed < invincibilityTime)
            {
                spriteRenderer.enabled = !spriteRenderer.enabled;
                yield return new WaitForSeconds(0.1f);
                elapsed += 0.1f;
            }
            spriteRenderer.enabled = true;
        }
        else
        {
            yield return new WaitForSeconds(invincibilityTime);
        }

        isInvincible = false;
    }

    IEnumerator DieAndRestartLevel()
    {
        if (spriteRenderer != null) spriteRenderer.enabled = false;
        if (movement != null) movement.enabled = false;
        if (rb != null) rb.linearVelocity = Vector2.zero;

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
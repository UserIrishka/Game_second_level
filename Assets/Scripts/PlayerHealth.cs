using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    [Header("Здоровье")]
    public int maxHealth = 100;
    public int currentHealth;

    [Header("UI")]
    public HealthTextUI healthTextUI;
    public GameObject gameOverPanel; // перетащи сюда панель Game Over

    [Header("Эффекты")]
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

        if (healthTextUI == null)
            healthTextUI = FindObjectOfType<HealthTextUI>();

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        UpdateHealthUI();
    }

    public void TakeDamage(int damage)
    {
        if (isInvincible) return;

        currentHealth = Mathf.Max(0, currentHealth - damage);
        UpdateHealthUI();
        StartCoroutine(InvincibilityRoutine());

        if (currentHealth <= 0)
            StartCoroutine(Die());
    }

    public void Heal(int amount)
    {
        currentHealth = Mathf.Min(maxHealth, currentHealth + amount);
        UpdateHealthUI();
    }

    void UpdateHealthUI()
    {
        if (healthTextUI != null)
            healthTextUI.UpdateHealth(currentHealth);
    }

    IEnumerator Die()
    {
        if (spriteRenderer != null) spriteRenderer.enabled = false;
        if (movement != null) movement.enabled = false;
        if (rb != null) rb.linearVelocity = Vector2.zero;

        yield return new WaitForSeconds(1f);

        // Показываем Game Over
        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        Time.timeScale = 0f; // останавливаем игру
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
}
using UnityEngine;

// Базовый компонент врага: здоровье, урон при контакте, смерть и дроп ключа.
// EnemyAI.cs управляет только движением и атакой на расстоянии.
public class Enemy : MonoBehaviour
{
    [Header("Здоровье")]
    public int maxHealth = 3;
    private int currentHealth;

    [Header("Контактный урон")]
    public int contactDamage = 1;

    [Header("Дроп ключа")]
    public GameObject keyPrefab;        // Префаб ключа (назначь в Inspector)
    [Range(0f, 1f)]
    public float keyDropChance = 1f;    // 1.0 = всегда, 0.5 = 50%

    void Start()
    {
        currentHealth = maxHealth;
        if (LevelManager.Instance != null)
            LevelManager.Instance.RegisterEnemy();
    }

    // Вызывается из Bullet при попадании
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
            Die();
    }

    void Die()
    {
        TryDropKey();
        if (LevelManager.Instance != null)
            LevelManager.Instance.EnemyKilled();

        Destroy(gameObject);
    }

    void TryDropKey()
    {
        if (keyPrefab != null && Random.value <= keyDropChance)
            Instantiate(keyPrefab, transform.position, Quaternion.identity);
    }

    // Контактный урон игроку
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth ph = collision.gameObject.GetComponent<PlayerHealth>();
            if (ph != null) ph.TakeDamage(contactDamage);
        }
    }
}
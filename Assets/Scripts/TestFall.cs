using UnityEngine;

// Считает высоту падения и наносит урон игроку при жёстком приземлении.
[RequireComponent(typeof(PlayerHealth))]
[RequireComponent(typeof(Rigidbody2D))]
public class TestFall : MonoBehaviour
{
    [Header("Настройки урона от падения")]
    public float minFallHeight = 3f;        // Минимальная высота для урона
    public int baseDamage = 1;              // Базовый урон при падении с minFallHeight
    public float extraDamagePerMeter = 5f;  // Доп. урон за каждый метр сверх минимума

    private Rigidbody2D rb;
    private PlayerHealth playerHealth;
    private float fallStartY;
    private bool isFalling = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerHealth = GetComponent<PlayerHealth>();
    }

    void Update()
    {
        // Начало падения
        if (!isFalling && rb.linearVelocity.y < -0.5f)
        {
            isFalling = true;
            fallStartY = transform.position.y;
        }

        // Приземление (скорость по Y ~ 0)
        if (isFalling && Mathf.Abs(rb.linearVelocity.y) < 0.05f)
        {
            float fallDistance = fallStartY - transform.position.y;
            isFalling = false;

            if (fallDistance >= minFallHeight)
            {
                int damage = baseDamage + Mathf.RoundToInt((fallDistance - minFallHeight) * extraDamagePerMeter);
                playerHealth.TakeDamage(damage);
            }
        }
    }
}
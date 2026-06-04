using System.Collections;
using UnityEngine;

public class BouncingTrap : Trap
{
    [SerializeField] private float bounceForce = 10f; // Сила, с которой будет отталкиваться игрок

    private void OnCollisionEnter2D(Collision2D collision)
    {
        StartCoroutine(ActivateTrap(collision.collider)); 
    }

    protected override IEnumerator ActivateTrap(Collider2D collision)
    {
        if (collision.TryGetComponent(out Rigidbody2D playerRigidbody) && !IsTrapActivated)
        {
            IsTrapActivated = true; // Запрет на повторное срабатывание ловушки
            playerRigidbody.AddForce(Vector2.up * bounceForce, ForceMode2D.Impulse); // Применяем силу вверх
            yield return new WaitForSeconds(0.5f); // Задержка перед повторным срабатыванием
            IsTrapActivated = false; 
        }
        yield return null;
    }
}


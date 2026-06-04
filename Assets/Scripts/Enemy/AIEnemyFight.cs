using Pathfinding;
using System.Collections;
using UnityEngine;

public class AIEnemyFight : EnemyStats
{
    [SerializeField] private int _damageAmount = 10; // Количество урона, наносимого игроку
    [SerializeField] private float _moveSpeed = 2f; // Скорость движения врага

    private SpriteRenderer sprite;
    [SerializeField] private AIPath aiPath;

    private void Awake()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        sprite.flipX = aiPath.desiredVelocity.x >= 0.01f;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Проверяем, есть ли у объекта компонент PlayerStats
        if (collision.TryGetComponent(out PlayerStats player))
        {
            // Наносим урон игроку
            player.GetDamage(_damageAmount);
        }
    }
}

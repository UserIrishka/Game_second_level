using System.Collections;
using UnityEngine;

public class WalkingEnemy : EnemyStats
{
    public float patrolSpeed = 2f; // Скорость патрулирования

    [SerializeField] private int _damageAmount = 10; // Количество урона, наносимого игроку
    public float attackCooldown = 1f; // Время между атаками
    [SerializeField] private Transform _pointToMove;


    private void Update()
    {
        Patrol();
    }

    public void SwapPosition(Transform nextPositionToMove)
    {
        _pointToMove = nextPositionToMove;
    }

    private void Patrol()
    {
        if (_pointToMove == null) return;

        // Перемещение к текущей точке патрулирования
        transform.position = Vector3.MoveTowards(transform.position, _pointToMove.position, patrolSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Проверяем, есть ли у объекта компонент PlayerStats
        if (collision.TryGetComponent(out IDamageAble player) && collision.gameObject.layer == 6)
        {
            Debug.Log("Атака");
            StartCoroutine(Attack(player));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerStats player))
        {
            StopAllCoroutines();
        }
    }

    private IEnumerator Attack(IDamageAble player)
    {
        while (true) 
        {
            Debug.Log("Атака1");
            player.GetDamage(_damageAmount); // Наносим урон игроку
            yield return new WaitForSeconds(attackCooldown);
        }
    }
}



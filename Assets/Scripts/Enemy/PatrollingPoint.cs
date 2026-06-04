using UnityEngine;

public class PatrollingPoint: MonoBehaviour
{
    [SerializeField] private Transform _pointToMove;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out WalkingEnemy enemy))
        {
            enemy.SwapPosition(_pointToMove);
        }
    }
} 



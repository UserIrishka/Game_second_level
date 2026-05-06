using UnityEngine;

public class GhostAI : MonoBehaviour
{
    [Header("Настройки движения")]
    public float patrolSpeed = 2f;
    public float chaseSpeed = 3.5f;
    public float patrolDistance = 3f; 
    public float noticeRange = 1f;    

    private Vector2 startPos;
    private bool movingRight = true;
    private bool isChasing = false;

    private Transform player;
    private Animator anim;
    private SpriteRenderer sprite;

    void Start()
    {
        startPos = transform.position;
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null) player = playerObj.transform;
    }

    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= noticeRange)
        {
            isChasing = true;
        }
        else if (distanceToPlayer > noticeRange + 2f)
        {
            isChasing = false;
        }

        if (isChasing) ChasePlayer();
        else Patrol();

        anim.SetBool("isAlert", isChasing);
    }

    void Patrol()
    {
        float leftEdge = startPos.x - patrolDistance;
        float rightEdge = startPos.x + patrolDistance;
        float targetX = movingRight ? rightEdge : leftEdge;

        // Двигаемся к цели плавно
        transform.position = Vector2.MoveTowards(transform.position,
            new Vector2(targetX, transform.position.y), patrolSpeed * Time.deltaTime);

        // Если дошли до края - разворот
        if (Mathf.Abs(transform.position.x - targetX) < 0.1f)
        {
            Flip();
        }
    }

    void ChasePlayer()
    {
        // Преследуем только по X, сохраняя текущий Y призрака
        Vector2 targetPos = new Vector2(player.position.x, transform.position.y);
        transform.position = Vector2.MoveTowards(transform.position, targetPos, chaseSpeed * Time.deltaTime);

        // Поворот спрайта за игроком
        if (player.position.x > transform.position.x && !movingRight) Flip();
        else if (player.position.x < transform.position.x && movingRight) Flip();
    }

    void Flip()
    {
        movingRight = !movingRight;
        sprite.flipX = !sprite.flipX;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(new Vector2(startPos.x - patrolDistance, startPos.y), new Vector2(startPos.x + patrolDistance, startPos.y));

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, noticeRange);
    }
}
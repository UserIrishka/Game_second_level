using UnityEngine;

// Управляет поведением врага: патруль, обнаружение, преследование, атака.
// Урон и здоровье — в Enemy.cs.
[RequireComponent(typeof(Enemy))]
public class EnemyAI : MonoBehaviour
{
    [Header("Патруль")]
    public Transform pointA;
    public Transform pointB;
    public float patrolSpeed = 2f;

    [Header("Обнаружение")]
    public float detectionRange = 5f;
    public float loseRange = 8f;
    public float chaseDelay = 0.5f;    // Задержка перед началом преследования

    [Header("Преследование")]
    public float chaseSpeed = 3.5f;

    [Header("Территория")]
    public bool useTerritory = true;
    public float territoryLeft = -10f;
    public float territoryRight = 10f;

    [Header("Проверка препятствий (LOS)")]
    public LayerMask obstacleLayer;
    public bool checkLineOfSight = true;
    public float raycastHeight = 0.5f;

    [Header("Атака (дальняя / контактная)")]
    public int meleeDamage = 1;
    public float attackRange = 0.9f;
    public float attackCooldown = 1f;

    // ── Состояние ──────────────────────────────────────────────
    private enum State { Patrol, Alert, Chase }
    private State state = State.Patrol;

    private Transform player;
    private SpriteRenderer spriteRenderer;
    private Transform currentPatrolTarget;
    private float chaseStartTime;
    private float lastAttackTime;
    private Vector3 lastKnownPlayerPos;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (pointA != null && pointB != null)
            currentPatrolTarget = pointB;
    }

    void Update()
    {
        if (player == null) return;

        UpdateState();
        ActOnState();
    }

    // ── Логика переходов между состояниями ─────────────────────
    void UpdateState()
    {
        float dist = Vector2.Distance(transform.position, player.position);
        bool canSee = CanSeePlayer();
        bool inTerritory = IsPlayerInTerritory();

        switch (state)
        {
            case State.Patrol:
                if (dist <= detectionRange && canSee && inTerritory)
                {
                    state = State.Alert;
                    chaseStartTime = Time.time;
                    lastKnownPlayerPos = player.position;
                }
                break;

            case State.Alert:
                if (!canSee || !inTerritory || dist > loseRange)
                {
                    state = State.Patrol;
                    break;
                }
                lastKnownPlayerPos = player.position;
                if (Time.time >= chaseStartTime + chaseDelay)
                    state = State.Chase;
                break;

            case State.Chase:
                if (canSee && inTerritory)
                    lastKnownPlayerPos = player.position;

                // Теряем игрока
                if (dist > loseRange || !inTerritory)
                {
                    state = State.Patrol;
                }
                break;
        }
    }

    // ── Действия в текущем состоянии ───────────────────────────
    void ActOnState()
    {
        switch (state)
        {
            case State.Patrol:
                Patrol();
                break;

            case State.Alert:
                // Стоим, смотрим на игрока
                FaceTarget(player.position.x);
                break;

            case State.Chase:
                ChasePlayer();
                break;
        }
    }

    // ── Патруль ────────────────────────────────────────────────
    void Patrol()
    {
        if (pointA == null || pointB == null || currentPatrolTarget == null) return;

        MoveTowards(currentPatrolTarget.position, patrolSpeed);

        if (Vector2.Distance(transform.position, currentPatrolTarget.position) < 0.2f)
            currentPatrolTarget = (currentPatrolTarget == pointA) ? pointB : pointA;
    }

    // ── Преследование ──────────────────────────────────────────
    void ChasePlayer()
    {
        bool canSee = CanSeePlayer();
        Vector3 target = canSee ? player.position : lastKnownPlayerPos;

        float targetX = useTerritory
            ? Mathf.Clamp(target.x, territoryLeft, territoryRight)
            : target.x;

        Vector2 targetPos = new Vector2(targetX, transform.position.y);
        MoveTowards(targetPos, chaseSpeed);

        if (!canSee && Vector2.Distance(transform.position, lastKnownPlayerPos) < 0.3f)
            state = State.Patrol;

        float distToPlayer = Vector2.Distance(transform.position, player.position);
        if (canSee && distToPlayer <= attackRange && Time.time > lastAttackTime + attackCooldown)
            Attack();
    }

    // ── Атака ──────────────────────────────────────────────────
    void Attack()
    {
        lastAttackTime = Time.time;
        PlayerHealth ph = player.GetComponent<PlayerHealth>();
        if (ph != null) ph.TakeDamage(meleeDamage);
    }

    // ── Утилиты ────────────────────────────────────────────────
    void MoveTowards(Vector2 target, float speed)
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        FaceTarget(target.x);
    }

    void FaceTarget(float targetX)
    {
        float dir = targetX - transform.position.x;
        if (Mathf.Abs(dir) > 0.05f)
            spriteRenderer.flipX = dir < 0;
    }

    bool CanSeePlayer()
    {
        if (player == null || !checkLineOfSight) return true;

        Vector2 from = new Vector2(transform.position.x, transform.position.y + raycastHeight);
        Vector2 to = new Vector2(player.position.x, player.position.y + raycastHeight);
        Vector2 dir = to - from;

        RaycastHit2D hit = Physics2D.Raycast(from, dir.normalized, dir.magnitude, obstacleLayer);

        Debug.DrawRay(from, dir, hit.collider == null ? Color.green : Color.red);
        return hit.collider == null;
    }

    bool IsPlayerInTerritory()
    {
        if (!useTerritory || player == null) return true;
        return player.position.x >= territoryLeft && player.position.x <= territoryRight;
    }

    // ── Гизмо ──────────────────────────────────────────────────
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, loseRange);

        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        if (pointA != null && pointB != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(pointA.position, pointB.position);
            Gizmos.DrawSphere(pointA.position, 0.15f);
            Gizmos.DrawSphere(pointB.position, 0.15f);
        }

        if (useTerritory)
        {
            Gizmos.color = new Color(0f, 1f, 0f, 0.15f);
            float cx = (territoryLeft + territoryRight) / 2f;
            Gizmos.DrawWireCube(new Vector3(cx, transform.position.y, 0),
                                new Vector3(territoryRight - territoryLeft, 4f, 1f));
        }
    }
}   
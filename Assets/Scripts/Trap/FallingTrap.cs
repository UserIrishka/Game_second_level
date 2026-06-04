using UnityEngine;

public class FallingTrap : MonoBehaviour
{
    [Header("Настройки")]
    [SerializeField] private float fallDelay = 0.5f;  
    [SerializeField] private float destroyDelay = 2f;  
    [SerializeField] private bool respawnOnReset = false; 

    private Rigidbody2D rb;
    private Collider2D platformCollider;
    private Vector3 startPosition;
    private Quaternion startRotation;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        platformCollider = GetComponent<Collider2D>();
        startPosition = transform.position;
        startRotation = transform.rotation;


        if (rb != null) rb.bodyType = RigidbodyType2D.Static;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Invoke(nameof(StartFalling), fallDelay);
        }
    }

    private void StartFalling()
    {

        if (rb != null)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.gravityScale = 1f; 
        }


        Destroy(gameObject, destroyDelay);
    }
}

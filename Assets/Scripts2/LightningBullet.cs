using UnityEngine;

public class LightningBullet : MonoBehaviour
{
    public float speed = 12f;
    public float lifetime = 2f;
    public float direction = 1f;

    void Start()
    {
        Destroy(gameObject, lifetime);

        if (direction < 0)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }

    void Update()
    {
        transform.position += Vector3.right * direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        GreenGhostAI ghost = other.GetComponent<GreenGhostAI>();

        if (ghost == null)
        {
            ghost = other.GetComponentInParent<GreenGhostAI>();
        }

        // Проверяем, нашли ли призрака или объект с тегом Enemy
        if (ghost != null || other.CompareTag("Enemy") || (other.transform.parent != null && other.transform.parent.CompareTag("Enemy")))
        {
            if (ghost != null)
            {
                Destroy(ghost.gameObject);
            }
            else
            {
                Destroy(other.gameObject);
            }

            Destroy(gameObject);
            return;
        }

        if (!other.CompareTag("Player") && !other.CompareTag("Key"))
        {
            Destroy(gameObject);
        }
    }
}
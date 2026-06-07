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
        if (other.CompareTag("Enemy") || other.GetComponent<GhostAI>() != null)
        {
            Destroy(other.gameObject); 
            Destroy(gameObject);       
            return;
        }

        if (!other.CompareTag("Player") && !other.CompareTag("Key"))
        {
            Destroy(gameObject);
        }
    }
}
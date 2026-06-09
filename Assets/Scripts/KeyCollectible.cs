using UnityEngine;

public class KeyCollectible : MonoBehaviour
{
    [Header("Анимация подбора")]
    public float bobAmplitude = 0.15f;
    public float bobFrequency = 2f;
    public float rotateSpeed = 90f;

    [Header("Звук")]
    public AudioClip collectSound; // перетащи b146...

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float newY = startPos.y + Mathf.Sin(Time.time * bobFrequency) * bobAmplitude;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        transform.Rotate(0f, 0f, rotateSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        if (collectSound != null)
            AudioSource.PlayClipAtPoint(collectSound, transform.position);

        KeyCounter counter = FindObjectOfType<KeyCounter>();
        if (counter != null) counter.AddKey();

        if (LevelManager.Instance != null)
            LevelManager.Instance.KeyCollected();

        Destroy(gameObject);
    }
}
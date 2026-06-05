using UnityEngine;

// Вешается на префаб ключа. При подборе игроком — добавляет ключ в счётчик.
public class KeyCollectible : MonoBehaviour
{
    [Header("Анимация подбора")]
    public float bobAmplitude = 0.15f;    // Амплитуда покачивания
    public float bobFrequency = 2f;       // Частота покачивания
    public float rotateSpeed = 90f;       // Скорость вращения (градусов/сек)

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        // Покачивание вверх-вниз
        float newY = startPos.y + Mathf.Sin(Time.time * bobFrequency) * bobAmplitude;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);

        // Вращение
        transform.Rotate(0f, 0f, rotateSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        KeyCounter counter = FindObjectOfType<KeyCounter>();
        if (counter != null)
            counter.AddKey();

        if (LevelManager.Instance != null)
            LevelManager.Instance.KeyCollected();

        Destroy(gameObject);
    }
}
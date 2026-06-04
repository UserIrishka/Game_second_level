using UnityEngine;

public class PlayerMovement2D : MonoBehaviour
{
    [Header("Настройки")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private Sprite upSprite;
    [SerializeField] private Sprite downSprite;
    [SerializeField] private Sprite leftSprite;
    [SerializeField] private Sprite rightSprite;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        rb.gravityScale = 0;
    }

    private void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        // Движение по осям X/Y (не по диагонали!)
        rb.linearVelocity = new Vector2(x, y).normalized * speed;

        // Меняем спрайты в зависимости от направления
        if (y > 0) SetSprite(upSprite);        // W - вверх
        else if (y < 0) SetSprite(downSprite); // S - вниз
        else if (x > 0) SetSprite(rightSprite); // D - вправо
        else if (x < 0) SetSprite(leftSprite); // A - влево
    }

    // Затычка для смены спрайта
    private void SetSprite(Sprite newSprite)
    {
        if (spriteRenderer != null && newSprite != null)
            spriteRenderer.sprite = newSprite;
    }
}
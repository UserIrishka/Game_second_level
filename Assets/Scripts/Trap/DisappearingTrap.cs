using UnityEngine;
using System.Collections;

public class DisappearingTrap : MonoBehaviour
{
    [Header("Настройки")]
    [SerializeField] private float disappearDelay = 2f;  
    [SerializeField] private float reappearDelay = 3f;    
    [SerializeField] private bool disappearOnce = false;  

    private Collider2D platformCollider;
    private SpriteRenderer spriteRenderer;
    private bool isHidden = false;

    private void Awake()
    {
        platformCollider = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isHidden) return;
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(DisappearRoutine());
        }
    }

    private IEnumerator DisappearRoutine()
    {

        yield return new WaitForSeconds(disappearDelay);


        if (spriteRenderer != null) spriteRenderer.enabled = false;
        if (platformCollider != null) platformCollider.enabled = false;
        isHidden = true;

        if (disappearOnce)
        {

            Destroy(gameObject);
        }
        else
        {

            yield return new WaitForSeconds(reappearDelay);
            if (spriteRenderer != null) spriteRenderer.enabled = true;
            if (platformCollider != null) platformCollider.enabled = true;
            isHidden = false;
        }
    }
}
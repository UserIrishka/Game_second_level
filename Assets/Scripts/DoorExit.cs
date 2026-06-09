using UnityEngine;

public class DoorExit : MonoBehaviour
{
    [Header("Визуал подсказки")]
    public GameObject hint; // необязательно — текст "Выход открыт!"

    void Update()
    {
        // Показываем подсказку если все условия выполнены
        if (hint != null && LevelManager.Instance != null)
            hint.SetActive(LevelManager.Instance.AllConditionsMet());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        if (LevelManager.Instance != null && LevelManager.Instance.AllConditionsMet())
            LevelManager.Instance.PlayerReachedDoor();
        // Если условия не выполнены — ничего не происходит
    }
}
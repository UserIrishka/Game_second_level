using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthTextUI : MonoBehaviour
{
    [Header("Текст здоровья")]
    public TMP_Text healthText; // или Text если не используешь TextMeshPro

    public void UpdateHealth(int currentHealth)
    {
        if (healthText != null)
            healthText.text = "Жизни: " + currentHealth;
    }
}
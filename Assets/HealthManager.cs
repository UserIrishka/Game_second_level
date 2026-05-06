using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class HealthManager : MonoBehaviour
{
    public TextMeshPro healthText;

    public void UpdateUI(int currentHealth)
    {
        if (healthText != null)
            healthText.text = "Жизни: " + currentHealth;
    }

    public void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
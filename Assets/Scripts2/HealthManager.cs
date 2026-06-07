using UnityEngine;
using TMPro;

public class HealthManager : MonoBehaviour
{
    public TextMeshPro healthText;

    [Header("UI Ёлементы")]
    public GameObject gameOverPanel;

    public void UpdateUI(int currentHealth)
    {
        if (healthText != null)
            healthText.text = "∆изни: " + currentHealth;
    }

    public void Die()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);

            Time.timeScale = 0f;
        }
        else
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(
                UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
            );
        }
    }
}
using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    public int totalKeys = 0;
    public int collectedKeys = 0;
    public int enemiesAlive = 0;

    public GameObject winPanel;
    public TMP_Text winText;

    void Awake()
    {
        Instance = this;
        if (winPanel != null)
            winPanel.SetActive(false);
    }

    public void RegisterEnemy()
    {
        enemiesAlive++;
    }

    public void EnemyKilled()
    {
        enemiesAlive = Mathf.Max(0, enemiesAlive - 1);
        CheckWin();
    }

    public void KeyCollected()
    {
        collectedKeys++;
        CheckWin();
    }

    void CheckWin()
    {
        if (collectedKeys >= totalKeys && enemiesAlive <= 0)
        {
            if (winPanel != null)
                winPanel.SetActive(true);

            if (winText != null)
                winText.text = "Вы прошли уровень!";
        }
    }
}
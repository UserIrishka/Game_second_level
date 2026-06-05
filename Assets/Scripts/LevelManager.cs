using UnityEngine;
using UnityEngine.SceneManagement;   // Required for LoadScene

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    public int totalKeys = 0;
    public int collectedKeys = 0;
    public int enemiesAlive = 0;

    void Awake()
    {
        Instance = this;
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
            // Load the next scene – make sure it's added to Build Settings
            SceneManager.LoadScene("Second_level");
        }
    }
}
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    public int totalKeys = 0;
    public int collectedKeys = 0;
    public int enemiesAlive = 0;

    private bool doorReached = false; // игрок дошёл до двери

    [Header("Следующий уровень")]
    public string nextSceneName = "Second_level";

    void Awake()
    {
        Instance = this;
    }

    public void RegisterEnemy() { enemiesAlive++; }
    public void EnemyKilled() { enemiesAlive = Mathf.Max(0, enemiesAlive - 1); CheckWin(); }
    public void KeyCollected() { collectedKeys++; CheckWin(); }

    public void PlayerReachedDoor()
    {
        doorReached = true;
        CheckWin();
    }

    public bool AllConditionsMet()
    {
        return collectedKeys >= totalKeys && enemiesAlive <= 0;
    }

    void CheckWin()
    {
        if (collectedKeys >= totalKeys && enemiesAlive <= 0 && doorReached)
            SceneManager.LoadScene(nextSceneName);
    }
}
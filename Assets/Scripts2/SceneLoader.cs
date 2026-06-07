using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private string sceneToLoad = "Platformer";

    public void LoadPlatformerLevel()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene(sceneToLoad);
    }

    public void LoadLevelByName(string levelName)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(levelName);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            LoadPlatformerLevel();
        }
    }
}
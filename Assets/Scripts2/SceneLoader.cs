using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Название сцены, которую нужно загрузить
    [SerializeField] private string sceneToLoad = "Platformer";

    // Загрузка по имени (можно вызвать из любого места)
    public void LoadPlatformerLevel()
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    // Загрузка при нажатии на кнопку (назначить в UI)
    public void LoadLevelByName(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    // Загрузка при входе в триггер (если нужно)
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            LoadPlatformerLevel();
        }
    }
}
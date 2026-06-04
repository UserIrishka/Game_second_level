using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void QuitGame()
    {
        Application.Quit();
    }
    public void ContinueGame()
    {
        SceneManager.LoadScene(1);
    }
}

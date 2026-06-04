using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    [SerializeField] private Button pauseButton;
    [SerializeField] private GameObject pausePanel; 
    private bool isPaused = false;

    private void Start()
    {
        pauseButton.onClick.AddListener(TogglePause);
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        Time.timeScale = isPaused ? 0 : 1; 

        if (pausePanel != null)
            pausePanel.SetActive(isPaused); 

        AudioListener.pause = isPaused;
    }
}
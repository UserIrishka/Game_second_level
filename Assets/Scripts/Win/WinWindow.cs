using UnityEngine;

public class WinWindow : MonoBehaviour
{
    public GameObject winPanel;           
    public AudioSource winMusicSource;   

    private AudioSource[] allAudioSources;

    void Start()
    {
        allAudioSources = UnityEngine.Object.FindObjectsByType<AudioSource>(UnityEngine.FindObjectsSortMode.None);
    }

    public void ShowWinPanel()
    {
        Time.timeScale = 0;
        winPanel.SetActive(true);

        foreach (AudioSource source in allAudioSources)
        {
            if (source != winMusicSource)
            {
                source.Pause();  
            }
        }

        if (winMusicSource != null)
        {
            winMusicSource.Play();
        }
    }
}


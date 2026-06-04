using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour, IDamageAble
{
    public int Health;

    [SerializeField] public Slider _slider;

    public float JumpForce;
    public float SpeedMovement;
    public float Damage;
    public int PlayerDamage;
    [SerializeField] private AudioSource gameOverMusicSource;


    // Новая переменная для монет
    public int CoinCount { get; private set; } = 0; 

    [SerializeField] private GameObject gameOverImage;

    // Ссылка на текстовый элемент для отображения счётчика монет
    [SerializeField] private TMPro.TextMeshProUGUI coinText; 

    // Метод для добавления монет
    public void AddCoins(int amount)
    {
        CoinCount += amount;
        UpdateCoinText(); // Обновляем текст при добавлении монет
        Debug.Log("Coins: " + CoinCount); // Для проверки в консоли
    }

    private void UpdateCoinText()
    {
        if (coinText != null)
        {
            coinText.text = "Coins: " + CoinCount; // Обновляем текст
        }
    }

    public void GetDamage(int damageValue)
    {
        if (Health - damageValue <= 0)
        {
            Health = 0;
            ShowGameOver();
        }
        else
        {
            Health -= damageValue;
        }
        _slider.value = Health;
        Debug.Log("Здоровье: " + Health);
    }

    private AudioSource[] allAudioSources;

    private void ShowGameOver()
    {
        // Отображаем Game Over Image
        if (gameOverImage != null)
        {
            gameOverImage.SetActive(true);
        }

        allAudioSources = UnityEngine.Object.FindObjectsByType<AudioSource>(UnityEngine.FindObjectsSortMode.None);

        // Отключаем все звуки кроме музыки GameOver
        foreach (AudioSource source in allAudioSources)
        {
            if (source != gameOverMusicSource)
            {
                source.Pause(); 
            }
        }

        // Запускаем музыку GameOver
        if (gameOverMusicSource != null)
        {
            gameOverMusicSource.Play();
        }


        // Остановка игры
        Time.timeScale = 0;
    }


    public void ReturnToMainMenu()
    {
        Time.timeScale = 1; 
        SceneManager.LoadScene("Menu"); 
    }
}

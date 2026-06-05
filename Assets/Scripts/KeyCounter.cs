using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Глобальный счётчик ключей. Положи этот скрипт на пустой GameObject «KeyManager» в сцене.
// В UI создай Text (или TMP_Text) и назначь его в Inspector.
public class KeyCounter : MonoBehaviour
{
    [Header("Счётчик")]
    public int keysCollected = 0;
    public int keysRequiredToWin = 3;   // Сколько ключей нужно для победы (0 = без условия)

    [Header("UI")]
    public TextMeshProUGUI keyText;                // Обычный UnityEngine.UI.Text
    // Если используешь TextMeshPro — замени на TMPro.TMP_Text и добавь using TMPro;

    [Header("Событие победы")]
    public GameObject winPanel;         // Панель «Уровень пройден» (опционально)

    void Start()
    {
        keysCollected = 0;
        UpdateUI();
    }

    public void AddKey(int amount = 1)
    {
        keysCollected += amount;
        UpdateUI();

        if (keysRequiredToWin > 0 && keysCollected >= keysRequiredToWin)
            OnLevelComplete();
    }

    void UpdateUI()
    {
        if (keyText != null)
            keyText.text = $"Ключи: {keysCollected}" +
                           (keysRequiredToWin > 0 ? $" / {keysRequiredToWin}" : "");
    }

    void OnLevelComplete()
    {
        Debug.Log("🏆 Уровень пройден!");

        if (winPanel != null)
            winPanel.SetActive(true);

        // Здесь можно загрузить следующий уровень:
        // UnityEngine.SceneManagement.SceneManager.LoadScene("NextLevel");
    }

    // Публичный геттер для других скриптов
    public int GetKeys() => keysCollected;
}
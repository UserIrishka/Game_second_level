using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KeyCounter : MonoBehaviour
{
    [Header("Счётчик")]
    public int keysCollected = 0;
    public int keysRequiredToWin = 3;

    [Header("UI")]
    public TextMeshProUGUI keyText;

    [Header("Событие победы")]
    public GameObject winPanel;

    void Start()
    {
        keysCollected = 0;
        UpdateUI();

        // Устанавливаем цвет текста #FEFE3F
        if (keyText != null)
            keyText.color = new Color32(0xFE, 0xFE, 0x3F, 0xFF);
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
    }

    public int GetKeys() => keysCollected;
}
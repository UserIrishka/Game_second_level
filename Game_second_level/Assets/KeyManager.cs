using UnityEngine;
using TMPro;

public class KeyManager : MonoBehaviour
{
    public TextMeshPro keyText;

    public void UpdateUI(int currentKeys)
    {
        if (keyText != null)
            keyText.text = "Ключи: " + currentKeys;
    }
}
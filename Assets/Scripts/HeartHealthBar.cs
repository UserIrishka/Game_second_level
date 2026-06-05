using UnityEngine;
using UnityEngine.UI;

// Отображает здоровье игрока в виде сердечек.
// Положи на GameObject «HeartsContainer» и назначь все Image-сердечки в массив.
public class HeartHealthBar : MonoBehaviour
{
    [Header("Сердечки")]
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public int maxHearts = 5;

    public void UpdateHearts(int currentHealth)
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].gameObject.SetActive(i < maxHearts);
            if (i < maxHearts)
                hearts[i].sprite = i < currentHealth ? fullHeart : emptyHeart;
        }
    }
}
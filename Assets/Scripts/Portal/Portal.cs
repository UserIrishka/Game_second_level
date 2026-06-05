using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    // Имя сцены, на которую нужно перейти
    public string nextSceneName = "Menu";
    [SerializeField] private int totalCoins; // Общее количество монет

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Проверяем, что объект, который вошел в триггер, - это игрок
        if (collision.CompareTag("Player"))
        {
            PlayerStats playerStats = collision.GetComponent<PlayerStats>();
            if (playerStats != null)
            {
                if (playerStats.CoinCount >= totalCoins)
                {
                    // Загружаем следующую сцену, если собраны все монеты
                    SceneManager.LoadScene(nextSceneName);
                }
                else
                {
                    //Debug.Log("Соберите все монеты, чтобы открыть портал!"); // Сообщение для игрока
                }
            }
        }
    }
}

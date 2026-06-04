using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player; // Ссылка на игрока

    void Update()
    {
        // Проверяем, есть ли ссылка на игрока
        if (player != null)
        {
            // Сравниваем позиции игрока и бабочки по оси X
            if (player.position.x > transform.position.x)
            {
                // Игрок справа, поворачиваем бабочку вправо
                transform.localScale = new Vector3(1, 1, 1); // Смотрит вправо
            }
            else
            {
                // Игрок слева, поворачиваем бабочку влево
                transform.localScale = new Vector3(-1, 1, 1); // Смотрит влево
            }
        }
    }
}

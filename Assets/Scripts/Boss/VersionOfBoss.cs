using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class VersionOfBoss : MonoBehaviour
{
    public float toggleInterval = 5f; // Периодичность включения/выключения объекта

    private void Start()
    {
        // Начинаем корутину для включения/выключения объекта
        StartCoroutine(ToggleObject());
    }

    private IEnumerator ToggleObject()
    {
        while (true)
        {
            // Включаем объект
            gameObject.SetActive(false);
            yield return new WaitForSeconds(toggleInterval);

            // Выключаем объект
            gameObject.SetActive(true);
            yield return new WaitForSeconds(toggleInterval);
        }
    }
}

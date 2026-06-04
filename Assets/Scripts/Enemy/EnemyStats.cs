using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStats : MonoBehaviour, IDamageAble
{
    public int Health;
    [SerializeField] private Slider _slider;
    private Coroutine _coroutine;

    private void Awake()
    {
        _slider.maxValue = Health;
        _slider.value = Health;
    }

    public void GetDamage(int damageValue)
    {
        _slider.gameObject.SetActive(true);
        if (_coroutine != null ) 
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(WaitToDisableSlider());
        if (Health - damageValue < 0)
        {
            Health = 0;
            Destroy(gameObject);
        }
        else
        {
            Health = Health - damageValue;
        }
        _slider.value = Health;
        Debug.Log("Çהמנמגüו: " + Health);
    }
    private IEnumerator WaitToDisableSlider()
    {
        yield return new WaitForSeconds(2);
        _slider.gameObject.SetActive(false);
    }
}

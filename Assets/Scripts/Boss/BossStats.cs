using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class BossStats : MonoBehaviour, IDamageAble
{
    public int Health;
    [SerializeField] private Slider _slider;
    private Coroutine _coroutine;
    private BossStateMachine _stateMachine;
    private float _teleporationHealth;
    [SerializeField] private BoxCollider2D _boxCollider2D;

    [Inject] private void Constract(BossStateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    private void Awake()
    {
        _slider.maxValue = Health;
        _slider.value = Health;
        _teleporationHealth = Health - (0.2f * _slider.maxValue);
    }

    public void GetDamage(int damageValue)
    {
        _slider.gameObject.SetActive(true);
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(WaitToDisableSlider());
        if (Health - damageValue <= 0)
        {

            Health = 0;
            _boxCollider2D.enabled = false;
            _stateMachine.ChangeState(BossStates.IDLE);
        }
        else
        {
            Health = Health - damageValue;
        }
        if (Health <= _teleporationHealth)
        {
            _stateMachine.ChangeState(BossStates.Teleport);
            _teleporationHealth = Health - (0.2f * _slider.maxValue);
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

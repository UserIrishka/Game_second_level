using UnityEngine;
using Zenject;

public class PlayerFight : MonoBehaviour
{
    [SerializeField] private PlayerStats _playerStats;
    [SerializeField] private Transform _atackPoint;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private AudioClip _attackSound;
    private AudioSource _audioSource; 
    private bool _isDialogActivated;

    private void DialogStarted() => _isDialogActivated = true;
    private void DialogEnded() => _isDialogActivated = false;

    [Inject]
    void Constract(EventBus eventBus)
    {
        eventBus.OnDialogStarted += DialogStarted;
        eventBus.OnDialogEnded += DialogEnded;
    }

    private void Start()
    {
        _audioSource = gameObject.AddComponent<AudioSource>();
        _audioSource.clip = _attackSound;
    }

    private void Update()
    {
        if (_isDialogActivated) 
            return;
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.C))
        {
            _audioSource.Play();
            if (Physics2D.OverlapBox(_atackPoint.position, new Vector2(1, 1), 0, _layerMask))
            {
                Collider2D[] enemy = Physics2D.OverlapBoxAll(_atackPoint.position, new Vector2(1, 1), 0, _layerMask);
                foreach (var hitObject in enemy)
                {
                    Debug.Log(hitObject.name);
                    if (hitObject.TryGetComponent(out IDamageAble iDamagable))
                    {
                        iDamagable.GetDamage(_playerStats.PlayerDamage);
                        //_audioSource.Play();
                    }
                }
            }
        }
    }
}

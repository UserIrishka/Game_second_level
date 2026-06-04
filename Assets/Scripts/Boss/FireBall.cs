using DG.Tweening;
using UnityEngine;

public class FireBall : MonoBehaviour, IBossSpell
{
    private Vector3 _positionToMove;
    [SerializeField] private int _flyingTime;
    [SerializeField] private float _detonationRange;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private int _damageValue;
    public Vector3 PositionToMove { get => _positionToMove; set => _positionToMove = value; }

    public void InicializeFireBall(Vector3 positionToMove)
    {
        _positionToMove = positionToMove;
        MoveToPoint();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IDamageAble player) && collision.gameObject.layer == 6)
        {
            player.GetDamage(_damageValue);
            Destroy(gameObject);
        }
    }
    public void MoveToPoint()
    {
        transform.DOMove(_positionToMove, _flyingTime).SetEase(Ease.Linear).OnComplete(SpellExploats);
    }

    private void SpellExploats()
    {
        Collider2D player = Physics2D.OverlapCircle(transform.position, _detonationRange, _layerMask);
        if (player != null) 
        {
            if (player.TryGetComponent(out IDamageAble stats))
            {
                stats.GetDamage(_damageValue);
            }
        }
        Destroy(gameObject);
    }
}

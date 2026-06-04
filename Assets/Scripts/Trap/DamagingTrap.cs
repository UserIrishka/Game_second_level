using System.Collections;
using UnityEngine;

public class DamagingTrap : Trap
{
    [SerializeField] private int _damageValue;
    protected override IEnumerator ActivateTrap(Collider2D collision)
    {
        if (collision.transform.TryGetComponent(out IDamageAble idamagable) && collision.gameObject.layer == 6)
        {
            idamagable.GetDamage(_damageValue);
        }
        yield return null;
    }
}
